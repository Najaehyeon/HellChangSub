using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class Player
    {
        public string Name { get; set; }
        public int JobCode { get; set; }        //직업 1번 전사, 2번 도적, 3번 마법사
        public string JobName { get; set; }
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

        public Player() { }

        public Player(SaveData saveData)
        {
            Name = saveData.Name;
            JobCode = saveData.JobCode;
            JobName = saveData.JobName;
            Level = saveData.Level;
            Exp = saveData.Exp;
            CurrentHealth = saveData.CurrentHealth;
            MaximumHealth = saveData.MaximumHealth;
            CurrentMana = saveData.CurrentMana;
            MaximumMana = saveData.MaximumMana;
            CurrentMana = saveData.CurrentMana;
            Atk = saveData.Atk;
            EquipAtk = saveData.EquipAtk;
            Def = saveData.Def;
            EquipDef = saveData.EquipDef;
            Gold = saveData.Gold;
            Crit = saveData.Crit;
            CritDamage = saveData.CritDamage;
            Evasion = saveData.Evasion;
        }

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
                    JobName = "전사";
                    Atk = 10.0f;
                    Def = 5;
                    CurrentHealth = 100;
                    MaximumHealth = 100;
                    CurrentMana = 50;
                    MaximumMana = 50;
                    Crit = 10;
                    CritDamage = 1.6f;
                    Evasion = 10;
                    break;

                case 2:         // 도적 - 중간 공격력, 치명타율 회피율 높음, 방어 체력 낮음
                    JobName = "도적";
                    Atk = 12.0f;
                    Def = 3;
                    CurrentHealth = 80;
                    MaximumHealth = 80;
                    CurrentMana = 50;
                    MaximumMana = 50;
                    Crit = 30;
                    CritDamage = 2.0f;
                    Evasion = 20;
                    break;

                case 3:         // 마법사 - 높은 공격력과 마나, 방어력과 체력이 낮고 회피 불가, 스킬 구현 후에는 기본 공격력을 낮추고 마법사 스킬들의 피해 계수를 높게 잡기
                    JobName = "마법사";
                    Atk = 15.0f;
                    Def = 0;
                    CurrentHealth = 70;
                    MaximumHealth = 70;
                    CurrentMana = 100;
                    MaximumMana = 100;
                    Crit = 10;
                    CritDamage = 1.6f;
                    Evasion = 0;
                    break;
            }
        }
        public void LevelUp()       //도전기능에서 요구하는 경험치량 10, 35, 65, 100
        {
            if (Level == 1 && Exp >= 10)
            {
                Exp -= 10;
                Level++;
                CurrentHealth = MaximumHealth;
                CurrentMana = MaximumMana;
                Atk += 0.5f;
                Def += 1;
                Console.WriteLine("레벨업을 하였습니다.");
            }
            else if (Level == 2 && Exp >= 35) 
            {
                Exp -= 35;
                Level++;
                CurrentHealth = MaximumHealth;
                CurrentMana = MaximumMana;
                Atk += 0.5f;
                Def += 1;
                Console.WriteLine("레벨업을 하였습니다.");
            }
            else if (Level == 3 && Exp >= 65)
            {
                Exp -= 65;
                Level++;
                CurrentHealth = MaximumHealth;
                CurrentMana = MaximumMana;
                Atk += 0.5f;
                Def += 1;
                Console.WriteLine("레벨업을 하였습니다.");
            }
            else if (Level == 4 && Exp >= 100)
            {
                Exp = 0;
                Level++;
                CurrentHealth = MaximumHealth;
                CurrentMana = MaximumMana;
                Atk += 0.5f;
                Def += 1;
                Console.WriteLine("레벨업을 하였습니다.");
            }
        }
        
        //스킬 습득 여부는 어떻게할까? - islearn 부울값을 배정해서 레벨업시 직업과 레벨 충족하면 해당 부울값을 true로 하고 true인 스킬은 전투화면에서 보여지고 사용도 가능하도록


        public void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine($"{"Name",-12} {Name}");
            Console.WriteLine($"{"Level",-12} {Level}");
            Console.WriteLine($"{"Exp",-12} {Exp}");
            Console.WriteLine($"{"Gold",-12} {Gold}");
            Console.WriteLine($"{"HP",-12} {CurrentHealth}/{MaximumHealth}");
            Console.WriteLine($"{"MP",-12} {CurrentMana}/{MaximumMana}");
            Console.WriteLine($"{"Atk",-12} {Atk} {(EquipAtk == 0 ? "" : $"(+ {EquipAtk})")}");
            Console.WriteLine($"{"Def",-12} {Def} {(EquipDef == 0 ? "" : $"(+ {EquipDef})")}");
            Console.WriteLine($"{"Crit",-12} {Crit}");
            Console.WriteLine($"{"CritDmg",-12} {CritDamage}");
            Console.WriteLine($"{"Evasion",-12} {Evasion}");
            Console.WriteLine("\n0. 나가기");
            int choice = Utility.Select(0, 0);
            GameManager.Instance.ShowMainScreen();
        }
    }
}
