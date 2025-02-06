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
        private Player player; // 플레이어
        private Monster monster; // 몬스터
        // 보상 아이템들 선언하기

        // 이벤트 델리게이트
        public delegate void GameEvent(Player character);
        public delegate void GameEvent(Monster enemy); 
        public event GameEvent OnCharacterDeath; // 캐릭터가 죽었을 때 발생하는 이벤트
        public event GameEvent OnEnemyDeath;  // 몬스터가 죽었을 때 발생하는 이벤트

        public Battle(Player player, Monster monster /*여기에 아이템*/)
        {
            this.player = player;
            this.monster = monster;
            //여기에 this 아이템
            OnCharacterDeath += GameOver;   // 캐릭터가 죽었을 때 GameOver 메서드 호출
            OnEnemyDeath += StageClear;     // 몬스터가 죽었을 때 StageClear 메서드 호출
        }

        // 스테이지 시작 메서드
        public void Start()
        {
            /*Console.WriteLine($"Battle! 플레이어 정보: 체력({player.CurrentHealth}/{player.MaximumHealth}), 공격력({player.Atk})");
            Console.WriteLine($"몬스터 정보: 이름({monster.Name}), 체력({monster.CurrentHealth}/{monster.MaximumHealth}), 공격력({monster.Atk})");
            Console.WriteLine("----------------------------------------------------");*/
            
            while (!player.IsDead && !monster.IsDead) // 플레이어 혹은 몬스터가 죽을 때까지 반복, 몬스터가 "전부" 죽어야 반복문 탈출하도록 바꿔야됨
            {
                // 플레이어의 턴
                Console.WriteLine($"{player.Name}의 턴!");
                if (player.IsOccur(monster.Evasion) == true)
                    Console.WriteLine($"{monster.Name}는 공격을 피했다!");
                else if(player.IsOccur(player.CritChance) == true)
                    monster.TakeDamage(player.Atk * player.CritDamage);
                else
                    monster.TakeDamage(player.Atk);
                Console.WriteLine();
                Thread.Sleep(100);  

                if (monster.IsDead) break;  // 몬스터가 죽었다면 턴 종료

                // 몬스터의 턴
                Console.WriteLine($"{monster.Name}의 턴!");
                //monster.IsOccur(monster.Evasion);
                player.IsOccur(player.CritChance);
                if
                player.TakeDamage(monster.Atk);
                Console.WriteLine();
                Thread.Sleep(100);  
            }

            // 플레이어나 몬스터가 죽었을 때 이벤트 호출
            if (player.IsDead)
            {
                OnCharacterDeath?.Invoke(player);
            }
            else if (monster.IsDead)
            {
                OnEnemyDeath?.Invoke(monster);
            }
        }
    }
}
