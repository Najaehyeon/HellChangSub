using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                PlayerTurn();   //몬스터와 플레이어의 정보를 계속해서 띄워줘야 하니 PlayerTurn, MonsterTurn으로 따로 정리
                if (monsters.All(m => m.IsDead))   //모든 monster의 IsDead값이 true인지 확인하는 과정
                {
                    Victory();
                    break;
                }

                
                
                MonsterTurn();
                if (player.IsDead)
                {
                    GameOver();
                    break;
                }
            }
        }
        /*public void StartBattle()     //위에꺼 다 다듬으면 지울거임
        {
            Console.WriteLine("===== 전투 시작 =====");
            while (!player.IsDead && monsters.Any(m => !m.IsDead))
            {
                Console.WriteLine("\n[내 정보]");
                Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.JobName}) - HP: {player.CurrentHealth}/{player.MaximumHealth}");

                Console.WriteLine("\n[몬스터 목록]");
                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {monsters[i].Name} (HP: {monsters[i].CurrentHealth}/{monsters[i].MaximumHealth})");
                }

                Console.WriteLine("\n공격할 몬스터 번호를 선택하세요 (0: 도망)");
                int choice = Utility.Select(0, monsters.Count);

                if (choice == 0)
                {
                    Console.WriteLine("플레이어가 전투에서 도망쳤습니다!");
                    return;
                }

                Monster target = monsters[choice - 1];

                if (IsOccur(target.Evasion))
                {
                    Console.WriteLine($"{target.Name}이(가) 공격을 회피했습니다!");
                }
                else
                {
                    target.TakeDamage(player.Atk, player.CritDamage, IsOccur(player.Crit));
                    if (target.IsDead)
                    {
                        Console.WriteLine($"{target.Name}이(가) 쓰러졌습니다!");
                        monsters.Remove(target);
                    }
                }

                if (!monsters.Any(m => !m.IsDead)) break;

                // 몬스터 공격
                foreach (var monster in monsters)
                {
                    if (monster.IsDead) continue;

                    Console.WriteLine($"{monster.Name}의 공격!");
                    if (IsOccur(player.Evasion))
                    {
                        Console.WriteLine($"{player.Name}이(가) 공격을 회피했습니다!");
                    }
                    else
                    {
                        player.TakeDamage(monster.Atk, monster.Crit, IsOccur(monster.Crit));
                        if (player.IsDead)
                        {
                            Console.WriteLine("플레이어가 사망했습니다!");
                            OnCharacterDeath?.Invoke(player);
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("===== 전투 종료 =====");
        }*/

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
            player.LevelUp();   //경험치 얻은 뒤에는 항상 레벨업 가능 여부 확인해줘야 함

            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("Victory\n");
            Console.WriteLine($"던전에서 몬스터 {monstersDefeated}마리를 잡았습니다.\n");
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {initialPlayerHealth} -> {player.CurrentHealth}");
            Console.WriteLine($"Exp {initialPlayerExp} -> {player.Exp}\n");
            Console.WriteLine("0. 다음");
            Console.ReadLine();
        }
    }

    
}
