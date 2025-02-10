using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class Quest
    {
        public List<QuestData> questDataList;

        public Quest(SaveData saveData)
        {
            questDataList = saveData.questDataList;
        }

        public Quest()
        {
            questDataList = new List<QuestData>() { KillMinionQuest.Instance, EquipShieldQuest.Instance, StrongMoreQuest.Instance };
        }

        public void ShowQuestList() // 퀘스트 목록 씬을 보여주는 메서드
        {
            Console.Clear();
            Console.WriteLine("퀘스트 선택하기.\n");
            for (int i = 0; i < questDataList.Count; i++)
            {
                // 진행 상태에 따라 표시를 다르게 해줌
                if (questDataList[i].QuestState == QuestState.NotStarted) // 해당 퀘스트가 수행중이 아니고, 수행한 적이 없을 때
                {
                    Console.WriteLine($"{i + 1}. [수행가능]{questDataList[i].Title}");
                }
                else if (questDataList[i].QuestState == QuestState.InProgress) // 해당 퀘스트를 수행중일 때
                {
                    Console.WriteLine($"{i + 1}. [진행중]{questDataList[i].Title}");
                }
                else if (questDataList[i].QuestState == QuestState.Completed) // 해당 퀘스트의 미션을 완수했을 때
                {
                    Console.WriteLine($"{i + 1}. [미션완료]{questDataList[i].Title}");
                }
                else if (questDataList[i].QuestState == QuestState.RewardClaimed) // 해당 퀘스트의 보상을 받았을 때
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"{i + 1}. [진행완료]{questDataList[i].Title}");
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
    }
}