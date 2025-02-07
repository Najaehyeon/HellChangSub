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
                    ShowFirstQuest();
                    break;
                case 2:
                    // 2번 퀘스트 선택 메서드
                    ShowSecondQuest();
                    break;
                case 3:
                    // 3번 퀘스트 선택 메서드
                    ShowThirdQuest();
                    break;
            }
        }

        public static void ShowFirstQuest() // "마을을 위협하는 미니언 처치" 퀘스트 보여주는 메서드 
        {
            Console.Clear();
            Console.WriteLine("마을을 위협하는 미니언 처치!");
            Console.WriteLine("\n이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\n모험가인 자네가 좀 처치해주게!");
            Console.WriteLine("\n- 미니언 5마리 처치 (0/5)");
            Console.WriteLine("\n- 보상- \n쓸만한 방패 x 1\n5G");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");

            int choice = Utility.Select(1,2);

            switch (choice)
            {
                case 1:
                    // 퀘스트 수락 (필요한 것 : 미니언을 몇 마리 처치했는지 확인 / 수행 시 보상 주기)
                    break;
                case 2:
                    // 퀘스트 거절, 돌아가기
                    Console.Clear();
                    ShowQuestList();
                    break;
            }
        }

        public static void ShowSecondQuest() // "장비를 장착해보자" 퀘스트 보여주는 메서드
        {
            Console.Clear();
            Console.WriteLine("장비를 장착해보자.");
            Console.WriteLine("\n'마을을 위협하는 미니언 처치' 퀘스트를 지행하면,\n'쓸만한 방패'를 얻을 수 있다.\n퀘스트를 진행하여 '쓸만한 방패'를 얻고, 장착해보자.");
            Console.WriteLine("\n- 쓸만한 방패 장착하기 (장착 안 됨)");
            Console.WriteLine("\n- 보상- \nEXP x 30\n5G");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");

            int choice = Utility.Select(1, 2);

            switch (choice)
            {
                case 1:
                    // 퀘스트 수락 (필요한 것 : 아이템을 장착했는지 확인 / 수행 시 보상 주기)
                    break;
                case 2:
                    // 퀘스트 거절, 돌아가기
                    Console.Clear();
                    ShowQuestList();
                    break;
            }
        }

        public static void ShowThirdQuest() // "더욱 더 강해지기" 퀘스트 보여주는 메서드
        {
            Console.Clear();
            Console.WriteLine("더욱 더 강해지기!");
            Console.WriteLine("\n특정 레벨에 도달하면 새로운 강력한 아이템을 얻을 수 있다.\nLv.10을 달성하여 더욱 더 강해져보자!");
            Console.WriteLine("\n- Lv.10 달성하기 (Lv.1 / Lv.10)");
            Console.WriteLine("\n- 보상- \nAK-47 x 1\n5G");

            Console.WriteLine("\n1. 수락");
            Console.WriteLine("2. 거절");

            int choice = Utility.Select(1, 2);

            switch (choice)
            {
                case 1:
                    // 퀘스트 수락 (레벨을 달성 했는 지 확인 / 수행 시 보상 주기)
                    break;
                case 2:
                    // 퀘스트 거절, 돌아가기
                    Console.Clear();
                    ShowQuestList();
                    break;
            }
        }
    }
}
