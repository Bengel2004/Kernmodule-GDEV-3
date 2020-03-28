using Robocode;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoboCodeProject
{
    class CircleMove : BTNode
    {
        int speed;
        public CircleMove(BlackBoard blackBoard, int speed)
        {
            this.blackBoard = blackBoard;
            this.speed = speed;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.robot.SetTurnRight(blackBoard.lastScannedRobotEvent.Bearing + 90);
            blackBoard.robot.SetAhead(speed * blackBoard.moveDirection);
            return BTNodeStatus.succes;
        }


    }
}
