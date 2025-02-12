using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Monster
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
        public List<Skill> Skills = new List<Skill>();
        protected int CalculateLevel(int baseLevel, int stageLvl, int bonus = 0)
        {
            Random rand = new Random();
            int randomFactor = rand.Next(-1, 2); // ±1 랜덤 적용
            return Math.Max(1, baseLevel + (stageLvl / 2) + randomFactor + bonus); // 최소 레벨 1 보장
        }
    }

    class Slime : Monster
    {
        
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
                    Level = CalculateLevel(1, stageLvl);
                    MaximumHealth = 50 + Level*10;
                    CurrentHealth = 50 + Level * 10;
                    Atk = 5 + Level;
                    Def = 2 + Level;
                    Crit = 10;
                    break;
                case 1:
                    Name = "단백질 슬라임";
                    Description = "뭉쳐진 단백질 덩어리. 프로틴 냄새가 진동한다.";
                    Level = CalculateLevel(3, stageLvl);
                    MaximumHealth = 80 + Level * 10;
                    CurrentHealth = 80 + Level * 10;
                    Atk = 8 + Level;
                    Def = 3 + Level;
                    Crit = 10;
                    break;
                case 2:
                    Name = "몬스터 쉐이크";
                    Description = "마시기만 해도 100g의 단백질이 흡수되는 궁극의 슬라임.";
                    Level = CalculateLevel(5, stageLvl,2);
                    MaximumHealth = 100 + Level * 10;
                    CurrentHealth = 100 + Level * 10;
                    Atk = 10 + Level;
                    Def = 5 + Level;
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
                    Level = CalculateLevel(4, stageLvl);
                    MaximumHealth = 120 + Level * 10;
                    CurrentHealth = 120 + Level * 10;
                    Atk = 10 + Level;
                    Def = 6 + Level;
                    Crit = 10;
                    break;
                case 1:
                    Name = "근손실 스켈레톤";
                    Description = "근육을 잃고 뼈만 남은 슬픈 헬창의 유령.";
                    Level = CalculateLevel(6, stageLvl);
                    MaximumHealth = 150 + Level * 20;
                    CurrentHealth = 150 + Level * 20;
                    Atk = 12 + Level;
                    Def = 8 + Level;
                    Crit = 10;
                    break;
                case 2:
                    Name = "식단 붕괴 스켈레톤";
                    Description = "3일 연속 치킨을 먹고 타락한 헬창의 잔재.";
                    Level = CalculateLevel(8, stageLvl,2);
                    MaximumHealth = 180 + Level * 30;
                    CurrentHealth = 180 + Level * 30;
                    Atk = 15 + Level;
                    Def = 10 + Level;
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
                    Level = CalculateLevel(7, stageLvl);
                    MaximumHealth = 200 + Level * 10;
                    CurrentHealth = 200 + Level * 10;
                    Atk = 20 + Level;
                    Def = 10 + Level;
                    Crit = 10;
                    break;
                case 1:
                    Name = "중량 오우거";
                    Description = "탄수화물을 흡수할수록 강해지는 초고열량 괴물.";
                    Level = CalculateLevel(9, stageLvl);
                    MaximumHealth = 250 + Level * 20;
                    CurrentHealth = 250 + Level * 20;
                    Atk = 25 + Level;
                    Def = 12 + Level;
                    Crit = 10;
                    break;
                case 2:
                    Name = "벌크업 오우거";
                    Description = "3대 5000을 돌파한 전설의 오우거. 바벨을 휘두른다.";
                    Level = CalculateLevel(11, stageLvl,3);
                    MaximumHealth = 300 + Level * 30;
                    CurrentHealth = 300 + Level * 30;
                    Atk = 30 + Level;
                    Def = 15 + Level;
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
                    Level = CalculateLevel(12, stageLvl);
                    MaximumHealth = 500 + Level * 10;
                    CurrentHealth = 500 + Level * 10;
                    Atk = 50 + Level;
                    Def = 30 + Level;
                    Crit = 10;
                    break;
                case 1:
                    Name = "게이너 드래곤";
                    Description = "벌크업을 위해 탄수화물과 지방을 끝없이 섭취하는 드래곤.";
                    Level = CalculateLevel(14, stageLvl);
                    MaximumHealth = 600 + Level * 20;
                    CurrentHealth = 600 + Level * 20;
                    Atk = 60 + Level;
                    Def = 35 + Level;
                    Crit = 10;
                    break;
                case 2:
                    Name = "스테로이드 드래곤";
                    Description = "자연의 법칙을 초월한 몸. 근육이 너무 커서 날개가 퇴화했다.";
                    Level = CalculateLevel(16, stageLvl,3);
                    MaximumHealth = 700 + Level * 30;
                    CurrentHealth = 700 + Level * 30;
                    Atk = 70 + Level;
                    Def = 40 + Level;
                    Crit = 10;
                    break;
                default:
                    break;
            }
        }
    }

    class HellChangSub : Monster
    {
        
        public HellChangSub()
        {
            Name = "헬창 Sup";
            Description = "슈퍼 헬창. 뇌 근육까지 수의로 움직일 수 있어 높은 지능을 가지고 있다. 특정 게임 디렉터와는 관계없다.";
            Level = CalculateLevel(50, 5);
            MaximumHealth = 3000;
            CurrentHealth = 3000;
            Atk = 100;
            Def = 50;
            Crit = 30;
        }
    }
}
