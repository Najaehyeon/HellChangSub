using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    
    public class Skill
    {
        public string Name { get; }
        public string? Text { get; }
        public float DamageMultiplier { get; }
        public int? ManaCost { get; }

        public Skill(string name, float damageMultiplier, int? manaCost = null, string? text = null)
        {
            Name = name;
            Text = text;
            DamageMultiplier = damageMultiplier;
            ManaCost = manaCost;
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int JobCode { get; set; }
        public string JobName { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int NeedExp { get; set; } = 10;
        public int Gold { get; set; }
        public int CurrentHealth { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentMana { get; set; }  // 마나 (Monster에는 없음)
        public int MaximumMana { get; set; }  // 최대마나 (직업별 변동값)
        public float Atk { get; set; }
        public float EquipAtk { get; set; }
        public int Def { get; set; }
        public int EquipDef { get; set; }
        public float Crit { get; set; }
        public float CritDamage { get; set; }

        public float Evasion { get; set; }  // 회피율 (직업별 변동값)
        public List<Skill> Skills { get; private set; } // 스킬 리스트

        public bool IsDead => CurrentHealth <= 0;

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

            // Skills 리스트 초기화
            Skills = saveData.Skills;

        }

        public Player(string name, int Job)
        {
            Name = name;
            JobCode = Job;
            Exp = 0;
            Gold = 1000000000;
            EquipAtk = 0;
            EquipDef = 0;
            switch (JobCode)
            {
                case 1:         // 전사 - 방어력 체력이 높음
                    JobName = "전사";
                    Atk = 10.0f;
                    Def = 5;
                    MaximumHealth = 100;
                    CurrentHealth = 100;
                    MaximumMana = 50;
                    CurrentMana = 50;
                    Crit = 10.0f;
                    CritDamage = 1.6f;
                    Evasion = 10.0f;

                    Skills = new List<Skill>
                    {
                        new Skill("파워 슬래시", 2.0f, 10, "단일 대상에게 공격력의 2배의 피해를 입힙니다."),
                        new Skill("발도", 3.0f, 15, "단일 대상에게 공격력의 3배의 피해를 입힙니다.")
                    };
                    break;

                case 2:         // 도적 - 높은 공격력, 치명타율 회피율 높음, 방어 체력 낮음
                    JobName = "도적";
                    Atk = 12.0f;
                    Def = 3;
                    MaximumHealth = 80;
                    CurrentHealth = 80;
                    Crit = 30.0f;
                    CritDamage = 2.0f;
                    Evasion = 20.0f;

                    Skills = new List<Skill>
                    {
                        new Skill("사악한 일격", 2.0f, 10, "단일 대상에게 공격력의 2배의 피해를 입힙니다."),
                        new Skill("절개", 3.0f, 15, "단일 대상에게 공격력의 3배의 피해를 입힙니다.")
                    };
                    break;

                case 3:         // 마법사 -  높은 마나, 방어력과 체력이 낮고 회피 불가,  스킬 계수는 높게 잡을것
                    JobName = "마법사";
                    Atk = 10.0f;
                    Def = 0;
                    CurrentHealth = 70;
                    MaximumHealth = 70;
                    MaximumMana = 100;
                    CurrentMana = 100;
                    Crit = 10.0f;
                    CritDamage = 1.6f;
                    Evasion = 0f;

                    Skills = new List<Skill>
                    {
                        new Skill("파이어볼", 3.0f, 15, "단일 대상에게 공격력의 3배의 피해를 입힙니다."),
                        new Skill("콜드 빔", 4.5f, 20, "단일 대상에게 공격력의 4.5배의 피해를 입힙니다.")
                    };
                    break;
            }
        }

        public void LevelUp2()       //도전기능에서 요구하는 경험치량 10, 35, 65, 100
        {
            int oldLevel = Level;

            while (true)
            {
                if (Level == 1 && Exp >= 10)
                {
                    Exp -= 10;
                    Level++;
                }
                else if (Level == 2 && Exp >= 35)
                {
                    Exp -= 35;
                    Level++;
                }
                else if (Level == 3 && Exp >= 65)
                {
                    Exp -= 65;
                    Level++;
                }
                else if (Level == 4 && Exp >= 100)
                {
                    Exp -= 100;
                    Level++;
                }
                else if (Exp >= 100 + (40 + 5 * (Level - 4)) * (Level - 3))
                {
                    Exp -= 100 + (40 + 5 * (Level - 4)) * (Level - 3);
                    Level++;
                }
                else
                    break;
                // 레벨업 안했으면 바로 위에서 break되서 StatUp, LearnSkill 실행 안됨
                StatUp();
                LearnSkill(Level); // 레벨업 후 스킬 습득 가능 시 스킬 습득
            }

            if (oldLevel != Level) // 레벨이 변했을 때만 실행
            {
                
                CurrentHealth = MaximumHealth;
                CurrentMana = MaximumMana;
                Console.WriteLine($"레벨이 상승했습니다. 현재 레벨: {Level}");
            }
        }
        
        public void LevelUp()
        {
            if (Exp >= NeedExp)
            {
                Level++;
                NeedExp = CalculateExpRequirement(Level);
                LearnSkill(Level);
                CurrentHealth = MaximumHealth;
                CurrentMana = MaximumMana;
                GameManager.Instance.quest.questDataList[2].Progressed = Level;
                Console.WriteLine($"레벨이 상승했습니다. 현재 레벨: {Level}");
                Utility.PressAnyKey();
                StatUp();
                StatUp();
            }
        }
        private int CalculateExpRequirement(int level)
        {
            int baseExp = 10;  // 최초 레벨업 필요 경험치
            double growthFactor = 1.5;  // 경험치 증가율  필요 경험치 = 10*레벨^1.5
            return (int)(baseExp * Math.Pow(level, growthFactor));
        }
        public void StatUp2()
        {
            switch (JobCode)        // 직업별 레벨업시 스탯증가 - 추후 다른방식으로 스탯이 오르게 할지도
            {
                case 1:
                    MaximumHealth += 50;
                    MaximumMana += 20;
                    Atk += 3.0f;
                    Def += 2;
                    break;
                case 2:
                    MaximumHealth += 30;
                    MaximumMana += 20;
                    Atk += 3.0f;
                    Def += 1;
                    break;
                case 3:
                    MaximumHealth += 20;
                    MaximumMana += 50;
                    Atk += 2.0f;
                    Def += 0;
                    break;
            }
        }

        public void StatUp()
        {
            Console.Clear();
            Console.WriteLine("상승 시킬 스탯을 선택해주세요.");
            Console.WriteLine($"1. HP : {MaximumHealth} (+100)");
            Console.WriteLine($"2. MP : {MaximumMana} (+30)");
            Console.WriteLine($"3. Atk : {Atk} (+10)");
            Console.WriteLine($"4. Def : {Def} (+5)");
            switch(Utility.Select(1,4))
            {
                case 1:
                    MaximumHealth += 100;
                    Console.WriteLine("HP가 상승했습니다.");
                    Utility.PressAnyKey();
                    break;
                case 2:
                    MaximumMana += 30;
                    Console.WriteLine("MP가 상승했습니다.");
                    Utility.PressAnyKey();
                    break;
                case 3:
                    Atk += 10;
                    Console.WriteLine("Atk가 상승했습니다.");
                    Utility.PressAnyKey();
                    break;
                case 4:
                    Def += 5;
                    Console.WriteLine("Def가 상승했습니다.");
                    Utility.PressAnyKey();
                    break;
            }

        }

        private void InitializeSkills()
        {
            Skills = new List<Skill>(); // 초기화

            switch (JobCode)
            {
                case 1: // 전사
                    Skills.Add(new Skill("파워 슬래시", 2.0f, 10, "단일 대상에게 공격력의 2배의 피해를 입힙니다."));
                    Skills.Add(new Skill("발도", 3.0f, 15, "단일 대상에게 공격력의 3배의 피해를 입힙니다."));
                    break;

                case 2: // 도적
                    Skills.Add(new Skill("사악한 일격", 2.0f, 10, "단일 대상에게 공격력의 2배의 피해를 입힙니다."));
                    Skills.Add(new Skill("절개", 3.0f, 15, "단일 대상에게 공격력의 3배의 피해를 입힙니다."));
                    break;

                case 3: // 마법사
                    Skills.Add(new Skill("파이어볼", 3.0f, 10, "단일 대상에게 공격력의 3배의 피해를 입힙니다."));
                    Skills.Add(new Skill("콜드 빔", 4.5f, 15, "단일 대상에게 공격력의 4.5배의 피해를 입힙니다."));
                    break;
            }

            // 레벨이 3 이상이면 자동으로 해당 레벨까지 배운 스킬 추가 - 추후 2레벨에 배우는 스킬 추가시 i = 2 로 해줘야됨
            for (int i = 3; i <= Level; i++)
            {
                LearnSkill(i);
            }
        }

        public void LearnSkill(int level)
        {
            switch (JobCode)
            {
                case 1: // 전사
                    if (level == 3 && !Skills.Exists(s => s.Name == "내려찍기"))
                    {
                        Skills.Add(new Skill("내려찍기", 4.0f, 20, "단일 대상에게 공격력의 4배의 피해를 입힙니다."));
                        Console.WriteLine("새로운 스킬을 습득했습니다! [내려찍기]");
                    }
                    else if (level == 7 && !Skills.Exists(s => s.Name == "분노의 일격"))
                    {
                        Skills.Add(new Skill("분노의 일격", 6.0f, 30, "단일 대상에게 공격력의 6배의 피해를 입힙니다."));
                        Console.WriteLine("새로운 스킬을 습득했습니다! [분노의 일격]");
                    }
                    break;

                case 2: // 도적
                    if (level == 3 && !Skills.Exists(s => s.Name == "소닉 블리츠"))
                    {
                        Skills.Add(new Skill("소닉 블리츠", 4.0f, 20, "단일 대상에게 공격력의 4배의 피해를 입힙니다"));
                        Console.WriteLine("새로운 스킬을 습득했습니다! [소닉 블리츠]");
                    }
                    else if (level == 7 && !Skills.Exists(s => s.Name == "암살"))
                    {
                        Skills.Add(new Skill("암살", 6.0f, 30, "단일 대상에게 공격력의 6배의 피해를 입힙니다."));
                        Console.WriteLine("새로운 스킬을 습득했습니다! [암살]");
                    }
                    break;

                case 3: // 마법사
                    if (level == 3 && !Skills.Exists(s => s.Name == "라이트닝 볼트"))
                    {
                        Skills.Add(new Skill("라이트닝 볼트", 6.0f, 25, "단일 대상에게 공격력의 6배의 피해를 입힙니다."));
                        Console.WriteLine("새로운 스킬을 습득했습니다! [라이트닝 볼트]");
                    }
                    else if (level == 7 && !Skills.Exists(s => s.Name == "윈드 블래스터"))
                    {
                        Skills.Add(new Skill("윈드 블래스터", 9.0f, 35, "단일 대상에게 공격력의 9배의 피해를 입힙니다."));
                        Console.WriteLine("새로운 스킬을 습득했습니다! [윈드 블래스터]");
                    }
                    break;
            }
        }

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

            Console.WriteLine("\n[보유 스킬]");
            if (Skills.Count == 0)
            {
                Console.WriteLine(" - 없음");
            }
            else
            {
                Console.WriteLine($"{"Name",-18} {"Text",-40} {"ManaCost",-10}");
                Console.WriteLine(new string('-', 70)); // 구분선

                foreach (var skill in Skills)
                {
                    Console.WriteLine($"{skill.Name,-18} {skill.Text,-40} {skill.ManaCost,-10}");
                }
            }
            Console.WriteLine("\n0. 나가기");
            int choice = Utility.Select(0, 0);
            GameManager.Instance.ShowMainScreen();
        }
    }
}
