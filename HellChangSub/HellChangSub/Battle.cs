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

        public Battle(Player player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;
            this.initialPlayerHealth = player.CurrentHealth;
            this.initialPlayerExp = player.Exp;
            
        }

        public void StartBattle()
        {
            while (true)
            {
                PlayerTurn();
                MonsterTurn();
            }
        }

        private void PlayerTurn()   // 플레이어 턴
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");

            for (int i = 0; i < monsters.Count; i++)
            {
                string status = monsters[i].IsDead ? "Dead" : $"HP {monsters[i].CurrentHealth}";    // 몬스터의 현재 체력이 0 이하이면 Dead 상태로 표시, 그렇지 않다면 HP {현재체력}으로 표시하도록
                Console.WriteLine($"{i + 1}. Lv.{monsters[i].Level} {monsters[i].Name} {status}");
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.JobName})");
            Console.WriteLine($"HP {player.CurrentHealth}/{player.MaximumHealth}\n");

            Console.WriteLine("0. 취소");
            Console.Write("대상을 선택해주세요.");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0) return;
            if (choice > 0 && choice <= monsters.Count && !monsters[choice - 1].IsDead)
            {
                Console.Clear();
                Console.WriteLine("Battle!!\n");
                Console.WriteLine($"{player.Name}의 공격!");
                switch (IsOccur(10))     //몬스터 회피율 일단 10으로 계산, 추후 회피율이 다른 몬스터 만들거면 10 위치에 monster[choice - 1].Evasion 넣어야됨
                                         //스킬을 기반으로 한 데미지일 경우 회피 계산식이 작동하지 않게 해야함 - isskill 부울값으로 할까?
                {
                    case true:
                        Console.WriteLine($"LV.{monsters[choice - 1].Level} {monsters[choice - 1].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                        break;
                    case false:
                        int Mobbeforedmg = monsters[choice - 1].CurrentHealth;
                        TakeDamage(monsters[choice - 1].Name, monsters[choice - 1].CurrentHealth, player.Atk, player.EquipAtk, player.CritDamage, monsters[choice - 1].Def, 0, IsOccur(player.Crit));
                        Console.WriteLine($"Lv.{monsters[choice - 1].Level} {monsters[choice - 1].Name}");
                        Console.WriteLine($"HP {Mobbeforedmg} -> {(monsters[choice - 1].IsDead ? "Dead" : monsters[choice - 1].CurrentHealth.ToString())}\n");
                        if (monsters.All(m => m.IsDead))        //모든 monster의 IsDead값이 true인지 확인하는 과정
                        {
                            Victory();
                            return;
                        }
                        break;
                }
                Console.WriteLine("0. 다음");
                Console.ReadLine();
            }
        }

        private void MonsterTurn()
        {
            Console.Clear();
            Console.WriteLine("Battle!!\n");
            

            foreach (var monster in monsters.Where(m => !m.IsDead))
            {
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                switch (IsOccur(player.Evasion)) //스킬을 기반으로 한 데미지일 경우 회피 계산식이 작동하지 않게 해야함 - isskill 부울값으로 할까?
                {
                    case true:
                        Console.WriteLine($"{player.Name} 는 {monster.Name}의 공격을 피해냈다!\n");
                        break;
                    case false:
                        int Playerbeforedmg = player.CurrentHealth;
                        TakeDamage(player.Name, player.CurrentHealth, monster.Atk, 0, 1.6, player.Def, player.EquipDef, IsOccur(monster.Crit));
                        Console.WriteLine($"Lv.{player.Level} {player.Name}");
                        Console.WriteLine($"HP {Playerbeforedmg} -> {player.CurrentHealth}\n");
                        if (player.IsDead)
                        {
                            GameOver();
                            return; // 남은 몬스터 턴 스킵
                        }
                        break;
                }
            }

            Console.WriteLine("0. 다음");
            Console.ReadLine();
        }

        public void TakeDamage(string Name, ref int CurrentHealth, float Atk, float EquipAtk, float CritDmg, int Def, int EquipDef, bool crit)
        {
            Random rand = new Random();  // 기본기능 - 전투 - 공격 항의 공격력은 10%의 오차를 가지게 됩니다 구현
            double randomMultiplier = rand.NextDouble() * 0.2 + 0.9;  // 0.9 ~ 1.1 사이의 값 생성

            float baseDamage = (Atk + EquipAtk) * (crit ? CritDmg : 1); // 기본 데미지 계산
            double adjustedDamage = baseDamage * randomMultiplier;  // 90% ~ 110% 적용

            int finalDamage = (int)Math.Ceiling(adjustedDamage) - Def - EquipDef; // 올림 처리 후 방어력 적용
            if (finalDamage < 0) finalDamage = 0; // 방어력이 과도하게 높을경우 맞았는데 체력이 회복되는거 방지

            CurrentHealth -= finalDamage;
            if (CurrentHealth < 0) CurrentHealth = 0; // 체력이 음수가 되지 않도록 설정

            Console.Write($"{Name} 을(를) 맞췄습니다. [데미지 : {finalDamage}]\n");
        }



        private static bool IsOccur(float prob) => new Random().Next(0, 100) < prob;        // return 같은걸 써줄 필요가 전혀 없었음

        private void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {initialPlayerHealth} -> 0\n");
            Console.WriteLine("0. 다음");
            Console.ReadLine();
        }

        private void Victory()
        {
            int monstersDefeated = monsters.Count;
            int expGained = monsters.Where(m => m.IsDead).Sum(m => m.Level);    //where를 사용해야 Sum으로 monster의 Level값 총합을 가져와 Exp를 계산 가능
            player.Exp += expGained;
            

            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {monstersDefeated}마리를 잡았습니다.\n");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {initialPlayerHealth} -> {player.CurrentHealth}");
            Console.WriteLine($"Exp {initialPlayerExp} -> {player.Exp}\n");
            player.LevelUp();   //경험치 얻은 뒤에는 항상 레벨업 가능 여부 확인해줘야 함
            Console.WriteLine("0. 다음");
            Console.ReadLine();
        }
    }

    
}
