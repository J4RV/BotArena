﻿using System;
using System.Collections.Generic;
using UnityEngine;


namespace BotArena
{
    public class RobotController : MonoBehaviour
    {
        [SerializeField]
        private string dllPath;

        public IRobot robot;
        public GameObject gun;
        public Dictionary<Command, ICommand> commands;


        //              UNITY METHODS

        void Start()
        {
            commands = new Dictionary<Command, ICommand>();
            robot = DLLLoader.LoadRobotFromDLL(dllPath, this);

            //Base commands, all robots can execute them
            RotateCommand rotateCmd = new RotateCommand(this);
            RotateGunCommand rotateGunCmd = new RotateGunCommand(this);
            commands.Add(Command.ROTATE, rotateCmd);
            commands.Add(Command.ROTATEGUN, rotateGunCmd);
        }

        void FixedUpdate()
        {
            if (TurnController.IsTurnUpdate())
            {
                UpdateRobot();
                robot.Think();
                //CheckEnemyAhead();
            }
        }


        //              ROBOT METHODS

        public void UpdateRobot()
        {
            Vector3 pos = transform.position;
            Vector3 rot = transform.rotation.eulerAngles;
            Vector3 gunRot = gun.transform.rotation.eulerAngles;
        }

        public HashSet<IRobot> FindEnemies()
        {
            HashSet<IRobot> res = new HashSet<IRobot>();
            GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");

            foreach (GameObject robot in robots)
            {
                res.Add(robot.GetComponent<IRobot>());
            }

            return res;
        }


        //              COMMAND METHODS

        public void Execute(Command cmd, object[] args)
        {
            switch (cmd)
            {
                case Command.ROTATE:
                    { 
                        RotateCommand rotate = (RotateCommand)commands[cmd];
                        float speed = (float) Convert.ToDouble(args[0]);
                        rotate.SetSpeed(speed);
                        rotate.Execute();

                        break;
                    }

                case Command.ROTATEGUN:
                    { 
                        RotateGunCommand rotateGun = (RotateGunCommand)commands[cmd];
                        float speed = (float)Convert.ToDouble(args[0]);
                        rotateGun.SetSpeed(speed * 2);
                        rotateGun.Execute();

                        break;
                    }
            }
        }

        public bool CanExecute(Command cmd, object[] args)
        {
            bool res = false;

            switch (cmd)
            {
                case Command.ROTATE:
                    { 
                        RotateCommand rotate = (RotateCommand)commands[cmd];
                        float speed = (float)Convert.ToDouble(args[0]);

                        rotate.SetSpeed(speed);
                        res = rotate.CanExecute();

                        break;
                    }

                case Command.ROTATEGUN:
                    { 
                        RotateGunCommand rotateGun = (RotateGunCommand)commands[cmd];
                        float speed = (float)Convert.ToDouble(args[0]);

                        rotateGun.SetSpeed(speed);
                        res = rotateGun.CanExecute();

                        break;
                    }
            }

            return res;
        }

        public void CheckEnemyAhead()
        {
            //check if there's an enemy ahead, if there is, execute robot.OnEnemyAhead()
            robot.OnEnemyAhead();
        }
    }
}