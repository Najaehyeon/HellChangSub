using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public static class Quest
    {
        public static void ShowQuestList() // 퀘스트 목록 씬을 보여주는 메서드
        {
            Console.Clear();
            Console.WriteLine("퀘스트 선택하기.\n");
            string[] quests = { "마을을 위협하는 미니언 처치!", "장비를 장착해보자.", "더욱 더 강해지기!" };
            for (int i = 0; i < quests.Length; i++)
            {
                // 진행 상태에 따라 표시를 다르게 해줌
                if (!History.Instance.Quests.ContainsKey(quests[i])) // 해당 퀘스트가 수행중이 아니고, 수행한 적이 없을 때
                {
                    Console.WriteLine($"{i + 1}. [수행가능]{quests[i]}");
                }
                else if (History.Instance.Quests[quests[i]].State == QuestState.InProgress) // 해당 퀘스트를 수행중일 때
                {
                    Console.WriteLine($"{i + 1}. [진행중]{quests[i]}");
                }
                else if (History.Instance.Quests[quests[i]].State == QuestState.Completed) // 해당 퀘스트의 미션을 완수했을 때
                {
                    Console.WriteLine($"{i + 1}. [미션완료]{quests[i]}");
                }
                else if (History.Instance.Quests[quests[i]].State == QuestState.RewardClaimed) // 해당 퀘스트의 보상을 받았을 때
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. [진행완료]{quests[i]}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n0. 나가기");

            int choice = Utility.Select(0, 3);

            // 퀘스트 선택 시 작동
            switch (choice)
            {
                case 1: // 1번 퀘스트 선택 시
                    KillMinionQuest.Instance.PickQuest();
                    break;
                case 2: // 2번 퀘스트 선택 시
                    EquipShieldQuest.Instance.PickQuest();
                    break;
                case 3: // 3번 퀘스트 선택 시
                    StrongMoreQuest.Instance.PickQuest();
                    break;
                case 0: // 0. 선택 시 나가기
                    break;
            }
        }
        

        // 퀘스트를 수락했을 때 실행되는 메서드 (퀘스트의 이름이랑, 목표, 진척도를 전달해줌)
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

        // 미션을 완료하고 보상을 받을 때 실행되는 메서드
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