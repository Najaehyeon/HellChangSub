﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HellChangSub
{
    class Battle
    {
        private Player player;
        private List<Monster> monsters;
        private int initialPlayerHealth;
        private int initialPlayerExp;
        public int turnCount = 0;

        public Battle(Player player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;
            this.initialPlayerHealth = player.CurrentHealth;        // 결과 화면에서 체력 변화량을 보여주기 위해 전투 시작시의 체력 저장
            this.initialPlayerExp = player.Exp;     // 윗줄가 마찬가지의 이유로 전투 시작시의 경험치 저장
            
        }

        public static int CalculateDamage(float attackerAtk, float attackerEquipAtk, float critDamage, bool crit,
                                  float randomMultiplier, float damageMultiplier, int defenderDef, int defenderEquipDef)
        {
            float baseDamage = (attackerAtk + attackerEquipAtk) * (crit ? critDamage : 1);
            double adjustedDamage = baseDamage * randomMultiplier * damageMultiplier;
            int finalDamage = (int)Math.Ceiling(adjustedDamage) - defenderDef - defenderEquipDef;

            return Math.Max(finalDamage, 0); // 방어력이 높아 데미지가 음수일 경우 0 처리
        }

        public void StartBattle()
        {
            while (true)
            {
                PlayerTurn();
                MonsterTurn();
                turnCount++;        // 지속 턴수가 있는 효과들 턴수 체크용
            }
        }

        private void PlayerTurn()   // 플레이어 턴
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)        // 몬스터들의 정보를 가져오고 이를 화면에 출력
            {
                string status = monsters[i].IsDead ? "Dead" : $"HP {monsters[i].CurrentHealth}";    // 몬스터의 현재 체력이 0 이하이면 Dead 상태로 표시, 그렇지 않다면 HP {현재체력}으로 표시하도록
                Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} {status}");
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.JobName})");
            Console.WriteLine($"HP {player.CurrentHealth}/{player.MaximumHealth}");
            Console.WriteLine($"MP {player.CurrentMana}/{player.MaximumMana}\n");

            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 아이템");
            Console.WriteLine("0. 도주");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            switch (Utility.Select(0, 4))       // 행동 선택
            {
                case 0:
                    Console.WriteLine($"{player.Name}은 도망쳤다!");
                    Utility.PressAnyKey();
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:     // 기본 공격
                    NormalAttack();
                    break;
                case 2:     // player.cs에 있는 스킬 목록에서 스킬 사용 - 스킬은 battle.cs 스킬사용 정리한 뒤 추가
                    UseSkill();
                    break;
                case 3:
                    Console.WriteLine("보유중인 소모품 이름 - 효과 - 갯수 보여주기");    // 스킬작업 다 끝나고 추가
                    break;

            }
        }

        private void MonsterTurn()      // 몬스터 턴
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            foreach (var monster in monsters.Where(m => !m.IsDead))
            {
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");

                bool useSkill = IsOccur(30); // 30% 확률로 스킬 사용
                if (useSkill && monster.Skills.Count > 0)
                {
                    UseMonsterSkill(monster); // 몬스터 스킬 사용
                }
                else
                {
                    NormalMonsterAttack(monster); // 일반 공격
                }

                if (player.IsDead)
                {
                    GameOver();
                    return;
                }
            }

        Utility.PressAnyKey();
        }

        private void NormalAttack()      // 플레이어 기본 공격
        {
            Console.WriteLine("공격할 대상을 선택하세요.");
            int targetIndex = Utility.Select(1, monsters.Count) - 1;
            Monster target = monsters[targetIndex];

            if (!target.IsDead)
            {
                int beforeHP = target.CurrentHealth;
                int mobHealth = target.CurrentHealth;

                Console.Clear();
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"{player.Name}의 공격!");

                if (IsOccur(10))  // 몬스터 10퍼확률로 회피, 추후 몬스터 개별 회피값 적용시 그 변수 넣어야함
                {
                    Console.WriteLine($"LV.{target.Level} {target.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                }
                else
                {
                    Random rand = new Random();
                    float randomMultiplier = (float)(rand.NextDouble() * 0.2 + 0.9); // 0.9 ~ 1.1 랜덤 보정값

                    int damage = CalculateDamage(player.Atk, player.EquipAtk, player.CritDamage, IsOccur(player.Crit), randomMultiplier, 1.0f, target.Def, 0);

                    target.CurrentHealth -= damage;
                    target.CurrentHealth = Math.Max(target.CurrentHealth, 0);

                    Console.WriteLine($"Lv.{target.Level} {target.Name}");
                    Console.WriteLine($"HP {beforeHP} -> {(target.IsDead ? "Dead" : target.CurrentHealth.ToString())}\n");
                    if (target.IsDead)
                        GetKillData(target.Name);
                    if (monsters.All(m => m.IsDead))
                        Victory();
                }
                Utility.PressAnyKey();
            }
            else if (target.IsDead)
            {
                Console.WriteLine("이미 죽은 대상입니다.");
                NormalAttack();
            }
        }

        private void UseSkill()      // 플레이어 스킬, 회피 불가
        {
            Console.WriteLine("사용할 스킬을 선택하세요.");

            for (int i = 0; i < player.Skills.Count; i++)
            {
                Skill skill = player.Skills[i];     // 스킬 목록 추가해야 빨간줄 사라질거임 - 사라졌음
                Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.ManaCost}\n   {skill.Text}");
            }
            Console.WriteLine("0. 뒤로가기");

            int skillChoice = Utility.Select(0, player.Skills.Count);
            if (skillChoice == 0)
            {
                PlayerTurn();
                return;
            }
            Skill selectedSkill = player.Skills[skillChoice - 1];

            if (player.CurrentMana < selectedSkill.ManaCost)    // MP가 부족할 경우 사용 불가
            {
                Console.WriteLine("마나가 부족합니다!\n");
                Utility.PressAnyKey();
                UseSkill();
            }

            Console.WriteLine("공격할 대상을 선택하세요.");
            int targetIndex = Utility.Select(1, monsters.Count) - 1;
            Monster target = monsters[targetIndex];
            if (target.IsDead)
            {
                Console.WriteLine("이미 죽은 대상입니다.");
                Utility.PressAnyKey();
                UseSkill();
            }
            else if (!target.IsDead)
            {
                int beforeHP = target.CurrentHealth;
                player.CurrentMana -= (int)selectedSkill.ManaCost; // MP 소모 - int? 형이라 (int) 추가로 오류해결
                Random rand = new Random();
                float randomMultiplier = (float)(rand.NextDouble() * 0.2 + 0.9); // 0.9 ~ 1.1 랜덤 보정값
                Console.Clear();
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"{player.Name}의 {selectedSkill.Name} 사용!");
                int damage = CalculateDamage(player.Atk, player.EquipAtk, player.CritDamage, IsOccur(player.Crit), randomMultiplier, selectedSkill.DamageMultiplier, target.Def, 0);
                
                target.CurrentHealth -= damage;
                target.CurrentHealth = Math.Max(target.CurrentHealth, 0);

                Console.WriteLine($"Lv.{target.Level} {target.Name}");
                Console.WriteLine($"HP {beforeHP} -> {(target.IsDead ? "Dead" : target.CurrentHealth.ToString())}\n");
                if (target.IsDead)
                    GetKillData(target.Name);
                if (monsters.All(m => m.IsDead))
                    Victory();

                Utility.PressAnyKey();
            }
        }

        private void NormalMonsterAttack(Monster monster)       // 몬스터 기본 공격
        {
            if (IsOccur(player.Evasion))  // 회피 가능
            {
                Console.WriteLine($"{player.Name} 는 {monster.Name}의 공격을 피해냈다!\n");
            }
            else
            {
                int beforeHP = player.CurrentHealth;
                Random rand = new Random();
                float randomMultiplier = (float)(rand.NextDouble() * 0.2 + 0.9); // 0.9 ~ 1.1 랜덤 보정값

                int damage = CalculateDamage(monster.Atk, 0, 1.6f, IsOccur(monster.Crit), randomMultiplier, 1.0f, player.Def, player.EquipDef);

                player.CurrentHealth -= damage;
                player.CurrentHealth = Math.Max(player.CurrentHealth, 0);

                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {beforeHP} -> {player.CurrentHealth}\n");
            }
        }

        private void UseMonsterSkill(Monster monster)       // 몬스터 스킬, 회피 불가
        {
            Skill selectedSkill = monster.Skills[new Random().Next(monster.Skills.Count)]; // 스킬 목록 추가해야 빨간줄 사라질거임
            int beforeHP = player.CurrentHealth;
            Random rand = new Random();
            float randomMultiplier = (float)(rand.NextDouble() * 0.2 + 0.9); // 0.9 ~ 1.1 랜덤 보정값
            Console.WriteLine($"{monster.Name} 이(가) {selectedSkill.Name} 을(를) 사용했다!");
            int damage = CalculateDamage(monster.Atk, 0, 1.6f, IsOccur(monster.Crit), randomMultiplier, selectedSkill.DamageMultiplier, player.Def, 0);

            player.CurrentHealth -= damage;
            player.CurrentHealth = Math.Max(player.CurrentHealth, 0);

            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {beforeHP} -> {player.CurrentHealth}\n");
        }


        public void Recover(string statname, ref int stat, ref int statmax, int heal)   //statname : HP, MP, stat : 회복할 프로퍼티, statmax : 프로퍼티 최댓값, heal : 회복 수단별로 정해진 값 
        {
            int finalStat = Math.Min(stat + heal, statmax); // 최대값을 초과하지 않도록 제한
            Console.WriteLine($"{statname} {stat} -> {finalStat}"); 
            stat = finalStat; 
        }

        private static bool IsOccur(float prob) => new Random().Next(0, 100) < prob;        // 확률 발동여부 판정

        private void GetKillData(String Name)
        {
            if (Name == "슬라임")
                GameManager.Instance.quest.questDataList[0].Progressed++;
            else
                return;
        }

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {initialPlayerHealth} -> 0\n");
            Utility.PressAnyKey();
            GameManager.Instance.ShowMainScreen();
        }

        private void Victory()
        {
            int monstersDefeated = monsters.Count;
            int expGained = monsters.Where(m => m.IsDead).Sum(m => m.Level);    //where를 사용해야 Sum으로 monster의 Level값 총합을 가져와 Exp를 계산 가능
            player.Exp += expGained;
            player.Gold += expGained * 100;     // 임시로 골드획득량 경험치의 100배로 해둠, 추후 골드를 많이 주는 몬스터, 덜 주는 몬스터 등이 생길 경우 수정
            

            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {monstersDefeated}마리를 잡았습니다.\n");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {initialPlayerHealth} -> {player.CurrentHealth}");
            int mp = player.CurrentMana;
            int mpmax = player.MaximumMana;
            Recover("MP", ref mp, ref mpmax, 10);       // 도전 기능 요구사항 - 전투 승리 시 MP 10 회복
            player.CurrentMana = mp;
            Console.WriteLine($"Exp {initialPlayerExp} -> {player.Exp}\n");
            player.LevelUp();   //경험치 얻은 뒤에는 항상 레벨업 가능 여부 확인해줘야 함
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine($"{expGained * 100} Gold");
            if (History.Instance.StageLvl == History.Instance.ChallengeLvl)     // 승리시 스테이지 Lv 상승
            {
                History.Instance.StageLvl++;
            }
            Utility.PressAnyKey();
            GameManager.Instance.ShowMainScreen();
        }
    }

    
}
