using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                {
                    case true:
                        Console.WriteLine($"LV.{monsters[choice - 1].Level} {monsters[choice - 1].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
                        break;
                    case false:
                        int Mobbeforedmg = monsters[choice - 1].CurrentHealth;
                        monsters[choice - 1].TakeDamage(player.Atk, player.CritDamage, IsOccur(player.Crit));
                        Console.WriteLine($"\nLv.{monsters[choice - 1].Level} {monsters[choice - 1].Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");   //여기부터 아래로 3줄 TakeDamage에 구현할것 - 치명타 공격! 까지
                        Console.WriteLine($"Lv.{monsters[choice - 1].Level} {monsters[choice - 1].Name}");
                        Console.WriteLine($"HP {Mobbeforedmg} -> {(monsters[choice - 1].IsDead ? "Dead" : monsters[choice - 1].CurrentHealth.ToString())}\n");
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
                switch (IsOccur(player.Evasion))
                {
                    case true:
                        Console.WriteLine($"{player.Name} 는 {monster.Name}의 공격을 피해냈다!\n");
                        break;
                    case false:
                        int Playerbeforedmg = player.CurrentHealth;
                        player.TakeDamage(monster.Atk, 1.6, IsOccur(monster.Crit));
                        Console.WriteLine($"{player.Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n");     //여기부터 아래로 3줄 TakeDamage에 구현할것 - 치명타 공격! 까지
                        Console.WriteLine($"Lv.{player.Level} {player.Name}");      //이부분을 Player.TakeDamage()에 추가하고 굳이 안써도 될듯?
                        Console.WriteLine($"HP {Playerbeforedmg} -> {player.CurrentHealth}\n");
                        break;
                }
            }

            Console.WriteLine("0. 다음");
            Console.ReadLine();
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
