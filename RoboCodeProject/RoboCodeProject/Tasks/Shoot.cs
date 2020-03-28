using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCodeProject
{
    class Shoot:BTNode
    {
        int damage;
        public Shoot(BlackBoard blackBoard, int damage)
        {
            this.blackBoard = blackBoard;
            this.damage = damage;
        }
        public override BTNodeStatus Tick()
        {
                blackBoard.robot.Fire(damage);
                return BTNodeStatus.succes;
        }


    }
}
