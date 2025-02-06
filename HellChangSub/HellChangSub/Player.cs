using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Player
    {
        public string Name { get; set; }
        public int JobCode { get; set; }        //직업 1번 전사, 2번 도적, 3번 마법사
        public int Level { get; set; }
        public int Exp { get; set; }
        public int CurrentHealth { get; set; }
        public int MaximumHealth { get; set; }
        public int Atk { get; set; }
        public int EquipAtk { get; set; }
        public int Def { get; set; }
        public int EquipDef { get; set; }
        public int Gold { get; set; }

        public Player(string name, int Job)
        {
            Name = name;
            JobCode = Job;
            Level = 1;
            Exp = 0;
            Gold = 0;
            EquipAtk = 0;
            EquipDef = 0;
            switch (JobCode)
            {
                case 1:
                    Atk = 10;
                    Def = 5;
                    break;

                case 2:
                    Atk = 12;
                    Def = 3;
                    break;

                case 3:
                    Atk = 15;
                    Def = 0;
                    break;
            }
        }

    }
}
