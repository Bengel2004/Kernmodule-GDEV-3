using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Robocode;


namespace RoboCodeProject
{
    public class BanzaiBot : AdvancedRobot
    {
        public BTNode FleeBehaviour;
        public BTNode RangedBehaviour;
        public BTNode KamikazeBehaviour;
        public BlackBoard blackBoard = new BlackBoard();

        public override void Run()
        {
            blackBoard.robot = this;

            FleeBehaviour = new Sequence(blackBoard, 
                new ScanRobot(blackBoard, 360), 
                new CircleMove(blackBoard, 1000),
                new TurnToEnemy(blackBoard),
                //new Shoot(blackBoard, 15),
                new TurnGunTowardsScannedTank(blackBoard),
                new UpdateColour(blackBoard, Color.Green)
                );

            KamikazeBehaviour = new Sequence(blackBoard,
                new ScanRobot(blackBoard, 360),
                new Kamikaze(blackBoard, 1000, 15),
                new TurnToEnemy(blackBoard),
                new TurnGunTowardsScannedTank(blackBoard),
                new UpdateColour(blackBoard, Color.Red)
                );

            RangedBehaviour = new Sequence(blackBoard,
                new ScanRobot(blackBoard, 360),
                new MoveAhead(blackBoard, 1000),
                new TurnToEnemy(blackBoard),
                new Shoot(blackBoard, 15),
                new TurnGunTowardsScannedTank(blackBoard),
                new UpdateColour(blackBoard, Color.Yellow)
                );

            IsAdjustGunForRobotTurn = true;
            IsAdjustRadarForGunTurn = true;

            Random rnd = new Random();
            blackBoard.crazyMode = rnd.Next(0, 2);

            while (true)
            {
                if (blackBoard.robot.Energy < 30)
                {
                    FleeBehaviour.Tick();
                }
                else if(blackBoard.robot.Energy > 90)
                {
                    RangedBehaviour.Tick();
                }
                else
                {
                    KamikazeBehaviour.Tick();
                }
            }
            
        }
        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            blackBoard.lastScannedRobotEvent = evnt;
        }

        public override void OnHitWall(HitWallEvent e)
        {
            blackBoard.moveDirection = -blackBoard.moveDirection;
            blackBoard.robot.Out.WriteLine("HIT WALL");
        }
    }
}
