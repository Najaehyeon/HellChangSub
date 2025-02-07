using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Quest
    {
        public static void ShowQuestList()
        {
            Console.WriteLine("퀘스트 선택하기.\n");
            string[] quests = { "1. 마을을 위협하는 미니언 처치!", "2. 장비를 장착해보자.", "3. 더욱 더 강해지기!" };
            for (int i = 0; i < quests.Length; i++)
            {
                Console.WriteLine(quests[i]);
            }
            Console.WriteLine("\n0. 나가기");

            int choice = Utility.Select(0, 3);

            switch (choice)
            {
                case 0:
                    // 나가기 메서드
                    break;
                case 1:
                    // 1번 퀘스트 선택 메서드
                    ShowQuest(
                        "마을을 위협하는 미니언 처치!",
                        "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!",
                        "- 미니언 5마리 처치",
                        new string[] { "쓸만한 방패", "500 Gold" }
                        );
                    break;
                case 2:
                    // 2번 퀘스트 선택 메서드
                    ShowQuest(
                        "장비를 장착해보자.",
                        "'마을을 위협하는 미니언 처치' 퀘스트를 지행하면,\n'쓸만한 방패'를 얻을 수 있다.\n퀘스트를 진행하여 '쓸만한 방패'를 얻고, 장착해보자.",
                        "쓸만한 방패 장착하기",
                        new string[] { "EXP +50", "200 Gold" }
                        );
                    break;
                case 3:
                    // 3번 퀘스트 선택 메서드
                    ShowQuest(
                        "더욱 더 강해지기!",
                        "특정 레벨에 도달하면 새로운 강력한 아이템을 얻을 수 있다.\nLv.10을 달성하여 더욱 더 강해져보자!",
                        "Lv.10 달성하기",
                        new string[] { "AK-47", "EXP +80", "1000 Gold" }
                        );
                    break;
            }
        }

        // 미션 만드는 메서드
        public static void ShowQuest(string title, string description, string mission, string[] rewards)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine();
            Console.WriteLine(description);
            Console.WriteLine();
            Console.WriteLine(mission);
            Console.WriteLine("\n- 보상 -");
            foreach (string reward in rewards)
            {
                Console.WriteLine(reward);
            }
            Console.WriteLine();
            Console.WriteLine("1. 수락\n2. 거절");

            int choice = Utility.Select(1, 2);

            switch (choice)
            {
                case 1:
                    // 퀘스트 수락 (미션 수행 했는지 확인하는 매개체 / 수행 시 보상 주기 / 해당 퀘스트 수행 중으로 표시하기 / 퀘스트를 완료하면, 리스트에서 완료된 표시하기)

                    break;
                case 2:
                    Console.Clear();
                    ShowQuestList();
                    break;
            }
        }
    }
}
