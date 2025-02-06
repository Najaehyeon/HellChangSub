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
        public float Atk { get; set; }
        public float EquipAtk { get; set; }
        public int Def { get; set; }
        public int EquipDef { get; set; }
        public int Gold { get; set; }
        public float Crit { get; set; }
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
                    Atk = 10.0f;
                    Def = 5;
                    CurrentHealth = 100;
                    MaximumHealth = 100;
                    CurrentMana = 30;
                    MaximumMana = 30;
                    Crit = 10;
                    CritDamage = 1.6f;
                    Evasion = 10;
                    break;

                case 2:         // 도적 - 중간 공격력, 치명타율 회피율 높음, 방어 체력 낮음
                    Atk = 12.0f;
                    Def = 3;
                    CurrentHealth = 80;
                    MaximumHealth = 80;
                    CurrentMana = 30;
                    MaximumMana = 30;
                    Crit = 30;
                    CritDamage = 2.0f;
                    Evasion = 20;
                    break;

                case 3:         // 마법사 - 높은 공격력과 마나, 방어력과 체력이 낮고 회피 불가
                    Atk = 15.0f;
                    Def = 0;
                    CurrentHealth = 70;
                    MaximumHealth = 70;
                    CurrentMana = 60;
                    MaximumMana = 60;
                    Crit = 10;
                    CritDamage = 1.6f;
                    Evasion = 0;
                    break;
            }
        }
        public void LevelUp()       //도전기능에서 요구하는 경험치량 10, 35, 65, 100
        {
            if (Exp >= Level * 100)
            {
                Exp -= Level * 100;
                Level++;
                CurrentHealth = MaximumHealth;
                Atk += 0.5f;
                Def += 1;
                Console.WriteLine("레벨업을 하였습니다.");
            }
        }
        public void TakeDamage(float damage, int crit)      //기본적인 데미지 공식 but 스킬데미지 및 치명타, 회피를 구현하려면????
        {
            CurrentHealth -= (int)damage - Def - EquipDef;      //데미지값 소수 첫째자리 버림
            if (crit == 1)
                Console.Write("치명타! ");
            Console.Write($"{Name}이(가) {damage - Def - EquipDef}의 데미지를 받았습니다.");
            if (IsDead)
                Console.WriteLine($"{Name}이(가) 죽었습니다.");
            else
                Console.WriteLine($"남은 체력: {CurrentHealth}");
        }
        //랜덤을 통해 치명타와 회피를 구현해야 함
        public int IsOccur(float prob)
        {
            int isOccur = new Random().Next(0, 100);
            if (isOccur < prob) return 1;
            else return 0;
        }
        //스킬을 기반으로 한 데미지일 경우 회피 계산식이 작동하지 않게 해야함 - isskill 부울값으로 할까?
        //스킬 습득 여부는 어떻게할까? - islearn 부울값을 배정해서 레벨업시 직업과 레벨 충족하면 해당 부울값을 true로 하고 true인 스킬은 전투화면에서 보여지고 사용도 가능하도록


    }
}
