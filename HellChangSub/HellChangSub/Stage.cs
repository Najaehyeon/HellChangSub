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
        List<Monster> monster;
        private Player player;

        public Stage(Player player, int stageLvl)
        {
            this.player = player;
            monster = new List<Monster>();
            int mosterQuantity = rand.Next(0,5 + stageLvl*2);
            for (int i = 0; i < mosterQuantity; i++)
            {
                Monster monster = new Monster(stageLvl);
            }
            ShowStage(stageLvl);
        }

        public void ShowStage(int stageLvl)
        {
            Console.WriteLine($"스테이지 : {stageLvl}\n몬스터가 등장했습니다.\n\n");
            for(int i = 0;i < monster.Count;i++) 
            {
                Console.WriteLine($"{i+1}. {monster[i].Level} {monster[i].Name} HP {monster[i].MaximumHealth}");
            }
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv. {player.Level} {player.Name} {player.JobCode}");
            Console.WriteLine($"HP {player.MaximumHealth}/{player.CurrentHealth}");
            Console.WriteLine($"MP {player.MaximumMP}/{player.CurrentMP}");
            Console.WriteLine("\n1. 전투시작\n0. 나가기");
            int choice = Utility.Select(0, 1);
            switch (choice)
            {
                case 0:
                    GameManger.ShowMainScreen();
                    break;
                case 1:
                    Battle.StartBattle();
                    break;
            }
        }
        
    }
}
