using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public static class Quest
    {
        public static void ShowQuestList()
        {
            Console.Clear();
            Console.WriteLine("퀘스트 선택하기.\n");
            string[] quests = { "마을을 위협하는 미니언 처치!", "장비를 장착해보자.", "더욱 더 강해지기!" };
            for (int i = 0; i < quests.Length; i++)
            {
                if (!History.Instance.Quests.ContainsKey(quests[i]))
                {
                    Console.WriteLine($"{i + 1}. [수행가능]{quests[i]}");
                }
                else if (History.Instance.Quests[quests[i]].State == QuestState.InProgress)
                {
                    Console.WriteLine($"{i + 1}. [진행중]{quests[i]}");
                }
                else if (History.Instance.Quests[quests[i]].State == QuestState.Completed)
                {
                    Console.WriteLine($"{i + 1}. [미션완료]{quests[i]}");
                }
                else if (History.Instance.Quests[quests[i]].State == QuestState.RewardClaimed)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. [진행완료]{quests[i]}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n0. 나가기");

            int choice = Utility.Select(0, 3);

            switch (choice)
            {
                case 1: // 1번 퀘스트 선택 시
                    if (!History.Instance.Quests.ContainsKey("마을을 위협하는 미니언 처치!") || History.Instance.Quests["마을을 위협하는 미니언 처치!"].State != QuestState.RewardClaimed)
                    {
                        ShowQuest("마을을 위협하는 미니언 처치!", "미니언 5마리 처치", new string[] { "쓸만한 방패", "500 Gold" }, 5, 0);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("이미 완료한 퀘스트입니다.");
                        break;
                    }
                case 2: // 2번 퀘스트 선택 시
                    if (!History.Instance.Quests.ContainsKey("장비를 장착해보자.") || History.Instance.Quests["장비를 장착해보자."].State != QuestState.RewardClaimed)
                    {
                        ShowQuest("장비를 장착해보자.", "쓸만한 방패 장착하기", new string[] { "EXP +50", "200 Gold" }, "쓸만한 방패 장착하기", "장착 안됨");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("이미 완료한 퀘스트입니다.");
                        break;
                    }
                case 3: // 3번 퀘스트 선택 시
                    if (!History.Instance.Quests.ContainsKey("더욱 더 강해지기!") || History.Instance.Quests["더욱 더 강해지기!"].State != QuestState.RewardClaimed)
                    {
                        ShowQuest("더욱 더 강해지기!", "Lv.10 달성하기", new string[] { "AK-47", "EXP + 80", "1000 Gold" }, 10, GameManager.Instance.Player.Level); // 마지막 인자 = 현재레벨
                        break;
                    }
                    else
                    {
                        Console.WriteLine("이미 완료한 퀘스트입니다.");
                        break;
                    }
                case 0: // 0. 선택 시 나가기
                    break;
            }
        }

        public static void ShowQuest(string title, string mission, string[] rewards, object goal, object nowProgressed)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine("\n- 미션 -");
            Console.WriteLine(mission);
            Console.WriteLine("\n- 보상 -");
            foreach (string reward in rewards)
            {
                Console.WriteLine(reward);
            }
            Console.WriteLine();

            if (!History.Instance.Quests.ContainsKey(title)) // 미션을 수행중이 아니거나, 수행한 적이 없을 때
            {
                Console.WriteLine("1. 수락\n2. 거절");

                int choice = Utility.Select(1, 2);

                switch (choice)
                {
                    case 1:
                        AcceptQuest(title, goal, nowProgressed);
                        break;
                    case 2:
                        Console.Clear();
                        ShowQuestList();
                        break;
                }
            }
            else if (History.Instance.Quests[title].State == QuestState.InProgress) // 미션을 수행 중일 때
            {
                Console.WriteLine("- 진척도 -");
                Console.WriteLine($"{History.Instance.Quests[title].NowProgressed} / {History.Instance.Quests[title].Goal}\n");
                Console.WriteLine("1. 미션포기\n0. 나가기");

                int choice = Utility.Select(0, 1);

                switch (choice)
                {
                    case 1: // 미션 포기
                        History.Instance.Quests.Remove(title);
                        Console.WriteLine("미션을 포기했습니다!");
                        Console.WriteLine("\n0. 나가기");
                        Console.WriteLine("다음 행동을 선택해주세요.");
                        int choice2 = Utility.Select(0, 0);
                        switch (choice2)
                        {
                            case 0:
                                Console.Clear();
                                ShowQuestList();
                                break;
                        }
                        break;
                    case 0: // 나가기
                        Console.Clear();
                        ShowQuestList();
                        break;
                }
            }
            else if (History.Instance.Quests[title].State == QuestState.Completed)
            {
                Console.WriteLine("1. 보상 얻기");

                int choice = Utility.Select(1, 1);

                switch (choice)
                {
                    case 1: // 보상 받기
                        History.Instance.ClaimReward(title);
                        Console.WriteLine("보상을 받았습니다.");
                        Console.WriteLine("\n0. 나가기");
                        Console.WriteLine("다음 행동을 선택해주세요.");
                        int choice2 = Utility.Select(0, 0);
                        switch (choice2)
                        {
                            case 0:
                                Console.Clear();
                                ShowQuestList();
                                break;
                        }
                        break;
                }
            }
        }

        public static void AcceptQuest(string questName, object goal, object nowProgressed)
        {
            History.Instance.StartQuest(questName, goal, nowProgressed);
            Console.WriteLine($"\"{questName}\" 퀘스트를 수락했습니다!");
            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("다음 행동을 선택해주세요.");
            int choice = Utility.Select(0, 0);
            switch (choice)
            {
                case 0:
                    Console.Clear();
                    ShowQuestList();
                    break;
            }
        }

        public static void CompleteMission(string questName)
        {
            History.Instance.UpdateProgress(questName);

            if (History.Instance.Quests.ContainsKey(questName) && History.Instance.Quests[questName].State == QuestState.Completed)
            {
                Console.WriteLine($"{questName}를 완료했습니다!");
                Console.WriteLine("보상을 받으려면 1을 입력하세요.");
                int rewardChoice = Utility.Select(1, 1);
                if (rewardChoice == 1)
                {
                    History.Instance.ClaimReward(questName);
                }
            }
        }
    }
}