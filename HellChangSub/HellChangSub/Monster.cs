using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    interface Monster
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth {  get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }
        
        public bool IsDead => CurrentHealth <= 0;
        public int RewardExp { get; set; } //몬스터별 기본 보상 설정 필요
        public int RewardGold { get; set; } 


    }

    class Slime : Monster
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }

        public bool IsDead => CurrentHealth <= 0;
        public int RewardExp { get; set; } = 0;//몬스터별 기본 보상 설정 필요
        public int RewardGold { get; set; } = 0;
        Random rand = new Random();
        public Slime(int stageLvl)
        {
            int randomDice = rand.Next(0, 101);//티어 별 몬스터 생성률 을 다르게 지정 스테이지 상승시 높은티어 몬스터 생성률 증가
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 15)
            {
                randomMonster = 1;
            }
            else if (randomDice < stageLvl * 30)
            {
                randomMonster = 0;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "슬라임";
                    Description = "끈적끈적한 몬스터";
                    MaximumHealth = 50+stageLvl*10;
                    CurrentHealth = 50 + stageLvl * 10;
                    Atk = 5 + stageLvl;
                    Def = 2 + stageLvl;
                    Crit = 10;
                    break;
                case 1:
                    Name = "검은 슬라임";
                    Description = "더강한 슬라임";
                    MaximumHealth = 80 + stageLvl * 10;
                    CurrentHealth = 80 + stageLvl * 10;
                    Atk = 8 + stageLvl;
                    Def = 3 + stageLvl;
                    Crit = 10;
                    break;
                case 2:
                    Name = "황금 슬라임";
                    Description = "보물로 이뤄진 슬라임";
                    MaximumHealth = 100 + stageLvl * 10;
                    CurrentHealth = 100 + stageLvl * 10;
                    Atk = 10 + stageLvl;
                    Def = 5 + stageLvl;
                    Crit = 10;
                    break;
                default:
                    break;
            }
        }
    }

    class Skeleton : Monster
    {
        Random rand = new Random();
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }

        public bool IsDead => CurrentHealth <= 0;
        public int RewardExp { get; set; } = 0;//몬스터별 기본 보상 설정 필요
        public int RewardGold { get; set; } = 0;
        public Skeleton(int stageLvl) 
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 15)
            {
                randomMonster = 1;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 0;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "스켈레톤";
                    Description = "전사의 유골";
                    MaximumHealth = 120 + stageLvl * 10;
                    CurrentHealth = 120 + stageLvl * 10;
                    Atk = 10 + stageLvl;
                    Def = 6 + stageLvl;
                    Crit = 10;
                    break;
                case 1:
                    Name = "검은 스켈레톤";
                    Description = "암흑 마법 스켈레톤";
                    MaximumHealth = 150 + stageLvl * 20;
                    CurrentHealth = 150 + stageLvl * 20;
                    Atk = 12 + stageLvl;
                    Def = 8 + stageLvl;
                    Crit = 10;
                    break;
                case 2:
                    Name = "황금 스켈레톤";
                    Description = "황금 갑옷을 입은 엘리트";
                    MaximumHealth = 180 + stageLvl * 30;
                    CurrentHealth = 180 + stageLvl * 30;
                    Atk = 15 + stageLvl;
                    Def = 10 + stageLvl;
                    Crit = 10;
                    break;
                default:
                    break;
            }
        }
    }

    class Orge : Monster
    {
        Random rand = new Random();
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }

        public bool IsDead => CurrentHealth <= 0;
        public int RewardExp { get; set; } = 0;//몬스터별 기본 보상 설정 필요
        public int RewardGold { get; set; } = 0;
        public Orge(int stageLvl)
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 15)
            {
                randomMonster = 1;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 0;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "오우거";
                    Description = "근육질 괴물";
                    MaximumHealth = 200 + stageLvl * 10;
                    CurrentHealth = 200 + stageLvl * 10;
                    Atk = 20 + stageLvl;
                    Def = 10 + stageLvl;
                    Crit = 10;
                    break;
                case 1:
                    Name = "검은 오우거";
                    Description = "어둠의 힘을 받은 오우거";
                    MaximumHealth = 250 + stageLvl * 20;
                    CurrentHealth = 250 + stageLvl * 20;
                    Atk = 25 + stageLvl;
                    Def = 12 + stageLvl;
                    Crit = 10;
                    break;
                case 2:
                    Name = "황금 오우거";
                    Description = "전설 속 오우거";
                    MaximumHealth = 300 + stageLvl * 30;
                    CurrentHealth = 300 + stageLvl * 30;
                    Atk = 30 + stageLvl;
                    Def = 15 + stageLvl;
                    Crit = 10;
                    break;
                default:
                    break;
            }
        }
    }

    class Dragon : Monster
    {
        Random rand = new Random();
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }

        public bool IsDead => CurrentHealth <= 0;
        public int RewardExp { get; set; } = 0;//몬스터별 기본 보상 설정 필요
        public int RewardGold { get; set; } = 0;
        public Dragon(int stageLvl)
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 15)
            {
                randomMonster = 1;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 0;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "드래곤";
                    Description = "강력한 힘을 가진 드래곤";
                    MaximumHealth = 500 + stageLvl * 10;
                    CurrentHealth = 500 + stageLvl * 10;
                    Atk = 50 + stageLvl;
                    Def = 30 + stageLvl;
                    Crit = 10;
                    break;
                case 1:
                    Name = "암흑 드래곤";
                    Description = "어둠의 힘을 지닌 고대 드래곤";
                    MaximumHealth = 600 + stageLvl * 20;
                    CurrentHealth = 600 + stageLvl * 20;
                    Atk = 60 + stageLvl;
                    Def = 35 + stageLvl;
                    Crit = 10;
                    break;
                case 2:
                    Name = "황금 드래곤";
                    Description = "전설 속의 빛과 마법을 지닌 드래곤";
                    MaximumHealth = 700 + stageLvl * 30;
                    CurrentHealth = 700 + stageLvl * 30;
                    Atk = 70 + stageLvl;
                    Def = 40 + stageLvl;
                    Crit = 10;
                    break;
                default:
                    break;
            }
        }
    }

    class HellChangSub : Monster
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }

        public bool IsDead => CurrentHealth <= 0;
        public int RewardExp { get; set; } = 0;//몬스터별 기본 보상 설정 필요
        public int RewardGold { get; set; } = 0;
        public HellChangSub()
        {
            Name = "헬창 Sup";
            Description = "슈퍼 헬창. 뇌 근육까지 수의로 움직일 수 있어 높은 지능을 가지고 있다. 특정 게임 디렉터와는 관계없다.";
            MaximumHealth = 3000;
            CurrentHealth = 3000;
            Atk = 100;
            Def = 50;
            Crit = 30;
        }
    }
}
