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
            // GameManager.Instance를 이용해 각 퀘스트의 인스턴스를 리스트에 추가
            questDataList = new List<QuestData>
        {
            new KillMinionQuest(),
            new EquipShieldQuest(),
            new StrongMoreQuest()
        };
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
            if (choice > 0 && choice <= questDataList.Count)
            {
                questDataList[choice - 1].PickQuest(); // 선택한 퀘스트 실행
            }
            else if (choice == 0)
            {
                GameManager.Instance.ShowMainScreen();
            }
        }
    }
}