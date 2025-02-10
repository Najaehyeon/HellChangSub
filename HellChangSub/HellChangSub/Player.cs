using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class Character  // 나중에 Character.cs로 따로 만들어줄거임
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentHealth { get; set; }
        public int MaximumHealth { get; set; }
        public float Atk { get; set; }
        public float EquipAtk { get; set; } = 0;
        public float CritDamage { get; set; }
        public int Def { get; set; }
        public int EquipDef { get; set; } = 0;
        public float Crit { get; set; }
        public float Evasion { get; set; } = 10.0f;
        public bool IsDead => CurrentHealth <= 0;

        public Character(string name, int level, int maxHealth, float atk, float critDmg, int def)
        {
            Name = name;
            Level = level;
            MaximumHealth = maxHealth;
            CurrentHealth = maxHealth;
            Atk = atk;
            CritDamage = critDmg;
            Def = def;
        }

        public void TakeDamage(Character attacker, float damageMultiplier, bool crit)       // 공격당하는객체.TakeDamage(공격하는개체, 스킬별로 설정된 계수(평타 = 1.0f), IsOccur(crit))
        {
            Random rand = new Random(); // 기본기능 - 전투 - 공격 항의 공격력은 10%의 오차를 가지게 됩니다 구현
            double randomMultiplier = rand.NextDouble() * 0.2 + 0.9;    // 0.9 ~ 1.1 사이의 값 생성
            float baseDamage = (attacker.Atk + attacker.EquipAtk) * (crit ? attacker.CritDamage : 1);   // 기본 데미지 계산
            double adjustedDamage = baseDamage * randomMultiplier * damageMultiplier;   // 랜덤 90% ~ 110% 적용

            int finalDamage = (int)Math.Ceiling(adjustedDamage) - Def - EquipDef;   // 올림 처리 후 방어력 적용
            if (finalDamage < 0) finalDamage = 0;   // 방어력이 과도하게 높을경우 맞았는데 체력이 회복되는거 방지

            CurrentHealth -= finalDamage;
            if (CurrentHealth < 0) CurrentHealth = 0;   // 체력이 음수가 되지 않도록 설정(추후 체력 음수상태에서 메인화면 회복 등 버그발생 방지)

            Console.WriteLine($"{Name} 을(를) 맞췄습니다. [데미지 : {finalDamage}]\n");
        }
    }

    public class Skill
    {
        public string Name { get; }
        public string Text { get; }
        public float DamageMultiplier { get; }
        public int ManaCost { get; }

        public Skill(string name, string text, float damageMultiplier, int manaCost)
        {
            Name = name;
            Text = text;
            DamageMultiplier = damageMultiplier;
            ManaCost = manaCost;
        }
    }

    public class Player : Character
    {
        public int JobCode { get; set; }
        public string JobName { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
        public int CurrentMana { get; set; }  // 마나 (Monster에는 없음)
        public int MaximumMana { get; set; }  // 최대마나 (직업별 변동값)
        public float Evasion { get; set; }  // 회피율 (직업별 변동값)
        public List<Skill> Skills { get; private set; }

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
        : base(name, 1, 100, 10.0f, 1.6f, 5) // `Character`의 기본 생성자 호출(이름, 레벨, 최대체력, 공격력, 치명타피해량, 방어력)
        {
            JobCode = Job;
            Exp = 0;
            Gold = 0;
            MaximumMana = 50;
            CurrentMana = 50;
            EquipAtk = 0;
            EquipDef = 0;
            switch (JobCode)
            {
                case 1:         // 전사 - 방어력 체력이 높음
                    JobName = "전사";
                    Atk += 0f;
                    Def += 2;
                    Crit = 10.0f;
                    Evasion = 10.0f;

                    Skills = new List<Skill>
                    {
                        new Skill("파워 슬래시", "단일 대상에게 공격력의 2배의 피해를 입힙니다.", 2.0f, 10),
                        new Skill("발도", "단일 대상에게 공격력의 3배의 피해를 입힙니다.", 3.0f, 15)
                    };
                    break;

                case 2:         // 도적 - 중간 공격력, 치명타율 회피율 높음, 방어 체력 낮음
                    JobName = "도적";
                    Atk += 2.0f;
                    Def -= 2;
                    MaximumHealth = 80;
                    CurrentHealth = 80;
                    Crit = 30.0f;
                    CritDamage = 2.0f;
                    Evasion = 20.0f;

                    Skills = new List<Skill>
                    {
                        new Skill("사악한 일격", "단일 대상에게 공격력의 2배의 피해를 입힙니다.", 2.0f, 10),
                        new Skill("절개", "단일 대상에게 공격력의 3배의 피해를 입힙니다.", 3.0f, 15)
                    };
                    break;

                case 3:         // 마법사 - 높은 공격력과 마나, 방어력과 체력이 낮고 회피 불가, 기본 공격력도 낮으므로 스킬 계수는 높게 잡을것
                    JobName = "마법사";
                    Atk += 0f;
                    Def -= 5;
                    CurrentHealth = 70;
                    MaximumHealth = 70;
                    MaximumMana = 100;
                    CurrentMana = 100;
                    Crit = 10.0f;
                    Evasion = 0f;

                    Skills = new List<Skill>
                    {
                        new Skill("파이어볼", "단일 대상에게 공격력의 4배의 피해를 입힙니다.", 4.0f, 10),
                        new Skill("콜드 빔", "단일 대상에게 공격력의 6배의 피해를 입힙니다.", 6.0f, 15)
                    };
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
