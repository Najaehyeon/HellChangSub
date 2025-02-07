using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    internal class History
    {
        private static History _instance;
        public static History Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new History();
                return _instance;
            }
        }

        private History()
        {
            Quests = new Dictionary<string, QuestData>();
        }

        public int stageLvl { get; set; } = 1;


        // 딕셔너리로 각 퀘스트에 맞는 미션을 관리 (EX. { {"미니언 5마리 처치" , 5} , {"장비 착용", true} } )
        public Dictionary<string, QuestData> Quests { get; private set; }

        public void StartQuest(string questName, object goal, object nowProgressed)
        {
            if (!Quests.ContainsKey(questName))
            {
                Quests[questName] = new QuestData(goal);
                Quests[questName].NowProgressed = nowProgressed;
                Quests[questName].State = QuestState.InProgress;
            }
        }

        // 진척도 확인하는 메서드
        public void UpdateProgress(string questName)
        {
            if (!Quests.ContainsKey(questName)) return;

            var quest = Quests[questName];

            // 목표 달성시 Completed 로 전환
            // 🎯 목표 타입에 따라 다르게 처리!
            if (quest.Goal is int goalInt && Instance.Quests[questName].NowProgressed is int progressInt)
            {
                if (progressInt >= goalInt)
                {
                    quest.State = QuestState.Completed;
                }
            }
            else if (quest.Goal is bool goalBool && Instance.Quests[questName].NowProgressed is bool progressBool)
            {
                if (progressBool == goalBool)
                {
                    quest.State = QuestState.Completed;
                }
            }
        }

        // 보상받기를 했을 때 실행되는 메서드
        public void ClaimReward(string questName)
        {
            Quests[questName].State = QuestState.RewardClaimed;
            Console.WriteLine($"\"{questName}\"의 보상을 받았습니다!");
            Console.WriteLine("0. 돌아가기");
            int choice = Utility.Select(0, 0);

            switch (choice)
            {
                case 0:
                    Console.Clear();
                    Quest.ShowQuestList();
                    break;
            }
        }
    }

    public class QuestData
    {
        public object Goal { get; private set; } // 목표 (int, bool 등 여러 타입 가능)
        public object NowProgressed { get; set; }
        public QuestState State { get; set; } // 진행 상태

        public QuestData(object goal)
        {
            Goal = goal;
            State = QuestState.NotStarted;
        }
    }


    public enum QuestState
    {
        NotStarted,
        InProgress,
        Completed,
        RewardClaimed
    }
}