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
            
            SelectStage(stageLvl);
        }

        public void SelectStage(int stageLvl)
        {
            Console.WriteLine($"도전 하실 스테이지를 선택해주세요 (지금까지 진행된 스테이지 : {stageLvl})");
            int choice = Utility.Select(1, stageLvl);
            History.Instance.ChallengeLvl = choice;
            ShowStage(choice);
        }

        public void ShowStage(int stageLvl)
        {
            Console.Clear();
            monsters = new List<Monster>();
            int mosterQuantity = (stageLvl == 5 ? 1 : rand.Next(1, 4 + stageLvl / 2));//1스테이지에서 최대 3마리 이후 스테이지레벨/2 만큼 증가
            for (int i = 0; i < mosterQuantity; i++)
            {
                Monster monster = MonsterFactory.CreateMonster(stageLvl);
                monsters.Add(monster);
            }
            Console.WriteLine($"[스테이지 : {stageLvl}]\n몬스터가 등장했습니다.\n\n[몬스터]");
            for(int i = 0;i < monsters.Count;i++) 
            {
                Console.WriteLine($"{Utility.FixWidth($"{i + 1}",3)}{Utility.FixWidth($"Lv {monsters[i].Level}",5)} {Utility.FixWidth($"{monsters[i].Name}", 12)} HP {Utility.FixWidth($"{monsters[i].MaximumHealth}", 12)}");
            }
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv {player.Level} {player.Name} {player.JobName}");
            Console.WriteLine($"HP {player.CurrentHealth}/{player.MaximumHealth}");
            Console.WriteLine($"MP {player.CurrentMana}/{player.MaximumMana}");
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
