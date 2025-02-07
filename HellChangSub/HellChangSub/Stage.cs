using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Stage
    {
        Random rand = new Random();
        List<Monster> monsters;
        private Player player;
        private Battle battle;   

        public Stage(Player player, int stageLvl)
        {
            this.player = player;
            monsters = new List<Monster>();
            int mosterQuantity = (stageLvl == 5 ? 1:rand.Next(1, 5 + stageLvl));
            for (int i = 0; i < mosterQuantity; i++)
            {
                Monster monster = MonsterFactory.CreateMonster(stageLvl);
                monsters.Add(monster);
            }
            ShowStage(stageLvl);
        }

        public void ShowStage(int stageLvl)
        {
            Console.Clear();
            Console.WriteLine($"스테이지 : {stageLvl}\n몬스터가 등장했습니다.\n\n");
            for(int i = 0;i < monsters.Count;i++) 
            {
                Console.WriteLine($"{i+1}. {monsters[i].Level} {monsters[i].Name} HP {monsters[i].MaximumHealth}");
            }
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv. {player.Level} {player.Name} {player.JobName}");
            Console.WriteLine($"HP {player.CurrentHealth}/{player.MaximumHealth}");
            Console.WriteLine($"MP /{player.CurrentMana}/{player.MaximumMana}");
            Console.WriteLine("\n1. 전투시작\n0. 나가기");
            int choice = Utility.Select(0, 1);
            switch (choice)
            {
                case 0:
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:
                    battle = new Battle(player, monsters);
                    battle.StartBattle();
                    break;
            }
        }
        
    }
}
