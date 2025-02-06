using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class MonsterFactory
    {
        private static Random rand = new Random();
        public static Monster CreateMonster(int stageLvl)
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 3;
            }
            else if (randomDice < stageLvl * 10)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 1;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    return new Slime(stageLvl);
                case 1:
                    return new Skeleton(stageLvl);
                case 2:
                    return new Orge(stageLvl);
                case 3:
                    return new Dragon(stageLvl);
                default:
                    return new Slime(stageLvl);
            }
        }
    }
}
