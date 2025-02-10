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
        public static Monster CreateMonster(int stageLvl) //스테이지 레벨을 매개변수로 받아 몬스터 객체를 생성하는 메서드
        {
            if (stageLvl == 5)
            {
                return new HellChangSub();//5스테이지 진입시 헬창섭 소환
            }
            else
            {
                int randomDice = rand.Next(1, 101);// 스테이지레벨에 따라 더 강력한 몬스터 생성될 확률 증가
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
}
