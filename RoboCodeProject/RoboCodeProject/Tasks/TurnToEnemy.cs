using Robocode;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoboCodeProject
{
    class TurnToEnemy : Rotator
    {
        public TurnToEnemy(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
            
        }


        public double TargetAngle()
        {
            ScannedRobotEvent _target = blackBoard.lastScannedRobotEvent;
            if(_target == null)
            {
                return 0;
            }
            double _angle = blackBoard.robot.Heading + _target.Bearing % 360;


            Vector2 robot = new Vector2(Convert.ToInt32(blackBoard.robot.X), Convert.ToInt32(blackBoard.robot.Y));
            Vector2 enemyBot = new Vector2(Convert.ToInt32(blackBoard.robot.X + Math.Sin(_angle) * _target.Distance), 
                Convert.ToInt32(blackBoard.robot.Y + Math.Cos(_angle) * _target.Distance));

            Vector2 result = robot * enemyBot;
            
            double dotResult1 = (robot.X * robot.Y) + (enemyBot.X * enemyBot.Y);
            double mag1 = Math.Sqrt(Math.Pow(robot.X, 2) + Math.Pow(robot.Y, 2));
            double mag2 = Math.Sqrt(Math.Pow(enemyBot.X, 2) + Math.Pow(enemyBot.Y, 2));

            double finalResult = Math.Cos(dotResult1 / (mag1 * mag2));


            if(_angle < -180)
            {
                _angle += 360;
            }
            
            blackBoard.robot.Out.WriteLine("I scanned robot: " + enemyBot.X + "X " + enemyBot.Y + " Y");

            return _angle;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.robot.SetTurnRight(blackBoard.robot.Heading - blackBoard.robot.GunHeading + blackBoard.lastScannedRobotEvent.Bearing);
            return BTNodeStatus.succes;
        }

        
    }
}
