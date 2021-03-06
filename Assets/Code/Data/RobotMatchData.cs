﻿using System.Collections.Generic;

namespace BotArena {
    class RobotMatchData {

        public RobotController controller;
        public IRobot robot;

        readonly List<int> matchResults;

        public RobotMatchData(IRobot robot, RobotController controller) {
            this.robot = robot;
            this.controller = controller;
            matchResults = new List<int>();
        }

        public void AddMatch(int result) {
            matchResults.Add(result);
        }

        public int VictoriesCount() {
            int res = 0;

            foreach (int i in matchResults) {
                res += i == 1 ? 1 : 0;
            }

            return res;
        }

        public int LossesCount() {
            int res = 0;

            foreach (int i in matchResults) {
                res += i == -1 ? 1 : 0;
            }

            return res;
        }

        public int Points() {
            int res = 0;

            foreach (int i in matchResults) {
                res += i;
            }

            return res;
        }

    }
}