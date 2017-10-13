﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BotArena
{
    internal static class DllUtil
    {
        static System.Random rng = new System.Random();

        private static readonly string RANDOM = "(RANDOM)";

        public static IRobot CreateRobotFromDll(string dllPath, string robotToLoad) {
            try { 
                if(robotToLoad == null || robotToLoad == "") {
                    return CreateFirstRobotFromDll(dllPath);
                }
                if(robotToLoad == RANDOM) {
                    return CreateRandomRobotFromDll(dllPath);
                }
                return CreateSpecificRobotFromDll(dllPath, robotToLoad);
            } catch (Exception e) {
                Debug.Log(e.Message);
                return null;
            }
        }

        private static IRobot CreateSpecificRobotFromDll(string dllPath, string robotClassName) {
            IList<Type> robots = FindRobotsInDll(dllPath);
            foreach (Type type in robots) {
                if (type.Name == robotClassName) {
                    return (IRobot) Activator.CreateInstance(type, new object[] { });
                }
            }
            throw new ArgumentException("Cannot find any robot classes named \"" + dllPath + "\" at \"" + dllPath + "\" library.");
        }

        private static IRobot CreateFirstRobotFromDll(string dllPath) {
            IList<Type> robots = FindRobotsInDll(dllPath);
            
            return (IRobot) Activator.CreateInstance(robots[0], new object[] { });
        }

        private static IRobot CreateRandomRobotFromDll(string dllPath) {
            IList<Type> robots = GetTypes<IRobot>(dllPath);
            int randomIndex = (int) (rng.NextDouble() * robots.Count);
            
            return (IRobot) Activator.CreateInstance(robots[randomIndex], new object[] { });
        }

        private static IList<Type> FindRobotsInDll(string dllPath) {
            return GetTypes<IRobot>(dllPath);
        }


        ///Learning some Linq!
        ///http://stackoverflow.com/questions/3353699/using-reflection-to-get-all-classes-of-certain-base-type-in-dll
        private static IList<Type> GetTypes<T>(String dllPath) {
            Assembly dll = Assembly.LoadFile(@dllPath);
            return (from t in dll.GetTypes()
                    where t.IsSubclassOf(typeof(T))
                    select t).ToList();
        }
    }

}