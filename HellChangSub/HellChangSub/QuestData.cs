using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public abstract class QuestData
    {
        public abstract string Title { get; }
        public abstract string Mission { get;  }
        public abstract string[] Rewards { get; }
        public abstract string Goal { get; }
        public abstract string Progressed { get; set; }
        public abstract QuestState QuestState { get; set; }

        public void PickQuest()
        {
            if (!History.Instance.Quests.ContainsKey(Title) || History.Instance.Quests[Title].State != QuestState.RewardClaimed)
            {
                ShowQuest();
            }
            else
            {
                Console.WriteLine("이미 완료한 퀘스트입니다.");
            }
        }
        
        // 퀘스트 목록에서 특정 퀘스트를 선택했을 때 실행되는 메서드(미션이랑 보상을 보여줌, 진행 상태에 따라 보여지는 게 다름)
        public void ShowQuest()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine("\n- 미션 -");
            Console.WriteLine(Mission);
            Console.WriteLine("\n- 보상 -");
            foreach (string reward in Rewards)
            {
                Console.WriteLine(reward);
            }
            Console.WriteLine();

            if (!History.Instance.Quests.ContainsKey(Title)) // 미션을 수행중이 아니거나, 수행한 적이 없을 때
            {
                Console.WriteLine("1. 수락\n2. 거절");

                int choice = Utility.Select(1, 2);

                switch (choice)
                {
                    case 1:
                        Quest.AcceptQuest(Title, Goal, Progressed);
                        break;
                    case 2:
                        Console.Clear();
                        Quest.ShowQuestList();
                        break;
                }
            }
            else if (History.Instance.Quests[Title].State == QuestState.InProgress) // 미션을 수행 중일 때
            {
                Console.WriteLine("- 진척도 -");
                Console.WriteLine($"{History.Instance.Quests[Title].NowProgressed} / {History.Instance.Quests[Title].Goal}\n");
                Console.WriteLine("1. 미션포기\n0. 나가기");

                int choice = Utility.Select(0, 1);

                switch (choice)
                {
                    case 1: // 미션 포기
                        History.Instance.Quests.Remove(Title);
                        Console.WriteLine("미션을 포기했습니다!");
                        Console.WriteLine("\n0. 나가기");
                        Console.WriteLine("다음 행동을 선택해주세요.");
                        int choice2 = Utility.Select(0, 0);
                        switch (choice2)
                        {
                            case 0:
                                Console.Clear();
                                Quest.ShowQuestList();
                                break;
                        }
                        break;
                    case 0: // 나가기
                        Console.Clear();
                        Quest.ShowQuestList();
                        break;
                }
            }
            else if (History.Instance.Quests[Title].State == QuestState.Completed)
            {
                Console.WriteLine("1. 보상 받기");

                int choice = Utility.Select(1, 1);

                switch (choice)
                {
                    case 1: // 보상 받기
                        Quest.ClaimReward(Title);
                        Console.WriteLine("보상을 받았습니다.");
                        Console.WriteLine("\n0. 나가기");
                        Console.WriteLine("다음 행동을 선택해주세요.");
                        int choice2 = Utility.Select(0, 0);
                        switch (choice2)
                        {
                            case 0:
                                Console.Clear();
                                Quest.ShowQuestList();
                                break;
                        }
                        break;
                }
            }
        }
    }


    // "마을을 위협하는 미니언 처치" 퀘스트 데이터
    public class KillMinionQuest : QuestData
    {
        private static KillMinionQuest _instance;
        public static KillMinionQuest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KillMinionQuest();
                }
                return _instance;
            }
        }

        public override string Title { get; } = "마을을 위협하는 미니언 처치!";
        public override string Mission { get; } = "미니언 10마리 처치!";
        public override string[] Rewards { get; } = new string[] { "쓸만한 방패", "500 Gold" };
        public override string Goal { get; } = "10";
        public override string Progressed { get; set; } = "0";
        public override QuestState QuestState { get; set; } = QuestState.NotStarted;
    }


    // "장비 장착하기" 퀘스트 데이터
    public class EquipShieldQuest : QuestData
    {
        private static EquipShieldQuest _instance;
        public static EquipShieldQuest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EquipShieldQuest();
                }
                return _instance;
            }
        }
        public override string Title { get; } = "장비를 장착해보자.";
        public override string Mission { get; } = "쓸만한 방패 장착하기";
        public override string[] Rewards { get; } = new string[] { "EXP +50", "200 Gold" };
        public override string Goal { get; } = "쓸만한 방패 장착하기";
        public override string Progressed { get; set; } = "장착 안됨";
        public override QuestState QuestState { get; set; } = QuestState.NotStarted;
    }


    // "더욱 더 강해지기" 퀘스트 데이터
    public class StrongMoreQuest : QuestData
    {
        private static StrongMoreQuest _instance;
        public static StrongMoreQuest Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StrongMoreQuest();
                }
                return _instance;
            }
        }
        public override string Title { get; } = "더욱 더 강해지기!";
        public override string Mission { get; } = "10레벨 달성하기";
        public override string[] Rewards { get; } = new string[] { "AK-47", "EXP + 80", "1000 Gold" };
        public override string Goal { get; } = "Lv.10";
        public override string Progressed { get; set; } = $"Lv.{GameManager.Instance.player.Level}";
        public override QuestState QuestState { get; set; } = QuestState.NotStarted;
    }

    // 퀘스트 진행 상태 열거형
    public enum QuestState
    {
        NotStarted,
        InProgress,
        Completed,
        RewardClaimed
    }
}
