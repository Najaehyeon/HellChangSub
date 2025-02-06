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
        public int CurrentMana { get; set; }
        public int MaximumMana { get; set; }
        public int Atk { get; set; }
        public int EquipAtk { get; set; }
        public int Def { get; set; }
        public int EquipDef { get; set; }
        public int Gold { get; set; }
        public float CritChance { get; set; }
        public float CritDamage { get; set; }
        public float Evasion { get; set; }

        public bool IsDead => CurrentHealth <= 0;

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
                case 1:         // 전사 - 방어력 체력이 높음
                    Atk = 10;
                    Def = 5;
                    CurrentHealth = 100;
                    MaximumHealth = 100;
                    CurrentMana = 30;
                    MaximumMana = 30;
                    CritChance = 10;
                    CritDamage = 1.6f;
                    break;

                case 2:         // 도적 - 중간 공격력, 치명타율 회피율 높음, 방어 체력 낮음
                    Atk = 12;
                    Def = 3;
                    CurrentHealth = 80;
                    MaximumHealth = 80;
                    CurrentMana = 30;
                    MaximumMana = 30;
                    CritChance = 30;
                    CritDamage = 2.0f;
                    break;

                case 3:         // 마법사 - 높은 공격력과 마나, 방어력과 체력이 낮음
                    Atk = 15;
                    Def = 0;
                    CurrentHealth = 70;
                    MaximumHealth = 70;
                    CurrentMana = 60;
                    MaximumMana = 60;
                    CritChance = 10;
                    CritDamage = 1.6f;
                    break;
            }
        }
        public void TakeDamage(int damage)      //기본적인 데미지 공식 but 스킬데미지 및 치명타, 회피를 구현하려면????
        {
            CurrentHealth -= damage - Def - EquipDef;
            if (IsDead)
                Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else
                Console.WriteLine($"{Name}이(가) {damage - Def - EquipDef}의 데미지를 받았습니다. 남은 체력: {CurrentHealth}");
        }
        //랜덤을 통해 치명타와 회피를 구현해야 함
        //스킬을 기반으로 한 데미지일 경우 회피 계산식이 작동하지 않게 해야함 - isskill 부울값으로 할까?
    }
}
