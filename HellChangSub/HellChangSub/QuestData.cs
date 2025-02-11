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
        public abstract int Goal { get; }
        public abstract int Progressed { get; set; }
        public abstract QuestState QuestState { get; set; }

        public void PickQuest()
        {
            if (QuestState != QuestState.RewardClaimed)
            {
                ShowQuest();
            }
            else
            {
                Console.WriteLine("이미 완료한 퀘스트입니다.");
                Utility.PressAnyKey();
                Console.Clear();
                GameManager.Instance.quest.ShowQuestList();
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
            if (QuestState == QuestState.NotStarted) // 수행한 적이 없을 때, 보여줄 내용
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("1. ");
                Console.ResetColor();
                Console.WriteLine("수락");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("2. ");
                Console.ResetColor();
                Console.WriteLine("거절");

                int choice = Utility.Select(1, 2);

                switch (choice)
                {
                    case 1:
                        AcceptQuest();
                        break;
                    case 2:
                        Console.Clear();
                        GameManager.Instance.quest.ShowQuestList();
                        break;
                }
            }
            else if (QuestState == QuestState.InProgress) // 미션을 수행 중일 때, 보여줄 내용
            {
                Console.WriteLine("- 진척도 -");
                Console.WriteLine($" {Progressed} / {Goal}\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("1. ");
                Console.ResetColor();
                Console.WriteLine("미션포기");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("0. ");
                Console.ResetColor();
                Console.WriteLine("나가기");

                int choice = Utility.Select(0, 1);

                switch (choice)
                {
                    case 1: // 미션 포기
                        GiveUpQuest();
                        break;
                    case 0: // 나가기
                        Console.Clear();
                        GameManager.Instance.quest.ShowQuestList();
                        break;
                }
            }
            else if (QuestState == QuestState.Completed) // 퀘스트 미션을 완료했을 때, 보여줄 내용
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("1. ");
                Console.ResetColor();
                Console.WriteLine("보상받기");

                int choice = Utility.Select(1, 1);

                switch (choice)
                {
                    case 1: // 보상 받기
                        GiveRewards();
                        QuestState = QuestState.RewardClaimed;
                        Console.WriteLine("보상을 받았습니다.");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("\n0. ");
                        Console.ResetColor();
                        Console.WriteLine("나가기");
                        Console.WriteLine("다음 행동을 선택해주세요.");
                        int choice2 = Utility.Select(0, 0);
                        switch (choice2)
                        {
                            case 0:
                                Console.Clear();
                                GameManager.Instance.quest.ShowQuestList();
                                break;
                        }
                        break;
                }
            }
        }

        // 퀘스트를 수락했을 때, 실행되는 메서드
        public void AcceptQuest ()
        {
            QuestState = QuestState.InProgress;
            JudgeState();
            Console.WriteLine($"\"{Title}\" 퀘스트를 수락했습니다!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n0. ");
            Console.ResetColor();
            Console.WriteLine("나가기");
            Console.WriteLine("다음 행동을 선택해주세요.");
            int choice2 = Utility.Select(0, 0);
            switch (choice2)
            {
                case 0:
                    Console.Clear();
                    GameManager.Instance.quest.ShowQuestList();
                    break;
            }
        }

        // 퀘스트를 포기했을 때, 실행되는 메서드
        public void GiveUpQuest()
        {
            FormatMission();
            QuestState = QuestState.NotStarted;
            Console.WriteLine("미션을 포기했습니다!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\n0. ");
            Console.ResetColor();
            Console.WriteLine("나가기");
            Console.WriteLine("다음 행동을 선택해주세요.");
            int choice2 = Utility.Select(0, 0);
            switch (choice2)
            {
                case 0:
                    Console.Clear();
                    GameManager.Instance.quest.ShowQuestList();
                    break;
            }
        }

        // 수행한 게 목표와 같을 때, 상태 변경
        public void JudgeState()
        {
            if (Progressed >= Goal) QuestState = QuestState.Completed;
        }

        // 퀘스트를 포기했을 때, 수행한 미션 초기화
        public abstract void FormatMission();

        // 보상 받기를 선택했을 때, 보상 주는 메서드
        public abstract void GiveRewards();
    }


    // "마을을 위협하는 미니언 처치" 퀘스트 데이터
    public class KillMinionQuest : QuestData
    {
        public override string Title { get; } = "마을을 위협하는 슬라임 처치!";
        public override string Mission { get; } = "슬라임 10마리 처치!";
        public override string[] Rewards { get; } = new string[] { "스파르타의 창", "500 Gold" };
        public override int Goal { get; } = 3;


        private int progressed = 0;
        public override int Progressed
        {
            get { return progressed; }
            set
            {
                if (QuestState == QuestState.InProgress) // 퀘스트가 진행중일 때만 수정 가능
                {
                    progressed = value;
                }
                else
                {
                    return;
                }
            }
        }


        private QuestState questState = QuestState.NotStarted;
        public override QuestState QuestState
        {
            get { return questState; }
            set
            {
                if (QuestState == QuestState.RewardClaimed)
                {
                    return;
                }
                else
                {
                    questState = value;
                }
            }
        }

        public override void FormatMission()
        {
            progressed = 0;
        }

        public override void GiveRewards()
        {
            EquipItem item = GameManager.Instance.itemManager.equipItems[2];
            GameManager.Instance.itemManager.itemUtil.QuestEquip(item);
            GameManager.Instance.player.Gold += 500;
        }
    }


    // "장비 장착하기" 퀘스트 데이터
    public class EquipShieldQuest : QuestData
    {
        public override string Title { get; } = "장비를 구매하여 장착해보자.";
        public override string Mission { get; } = "장비 장착하기";
        public override string[] Rewards { get; } = new string[] { "EXP +50", "200 Gold" };
        public override int Goal { get; } = 1;


        private int progressed = 0;
        public override int Progressed
        {
            get { return progressed; }
            set
            {
                if (QuestState == QuestState.InProgress || QuestState == QuestState.Completed) // 퀘스트가 진행중일 때만 수정 가능
                {
                    progressed = value;
                }
                else
                {
                    return;
                }
            }
        }

        private QuestState questState = QuestState.NotStarted;
        public override QuestState QuestState
        {
            get { return questState; }
            set
            {
                if (QuestState == QuestState.RewardClaimed)
                {
                    return;
                }
                else
                {
                    questState = value;
                }
            }
        }

        public override void FormatMission()
        {
            progressed = 0;
        }

        public override void GiveRewards()
        {
            GameManager.Instance.player.Exp += 50;
            GameManager.Instance.player.LevelUp();
            GameManager.Instance.player.Gold += 200;
        }
    }


    // "더욱 더 강해지기" 퀘스트 데이터
    public class StrongMoreQuest : QuestData
    {
        public override string Title { get; } = "더욱 더 강해지기!";
        public override string Mission { get; } = "3레벨 달성하기";
        public override string[] Rewards { get; } = new string[] { "EXP + 80", "1000 Gold" };
        public override int Goal { get; } = 3;

        private int progressed = GameManager.Instance.player.Level;
        public override int Progressed
        {
            get { return progressed; }
            set
            {
                progressed = value;
            }
        }

        private QuestState questState = QuestState.NotStarted;
        public override QuestState QuestState
        {
            get { return questState; }
            set
            {
                if (QuestState == QuestState.RewardClaimed)
                {
                    return;
                }
                else
                {
                    questState = value;
                }
            }
        }

        public override void FormatMission()
        {
            progressed = GameManager.Instance.player.Level;
        }

        public override void GiveRewards()
        {
            GameManager.Instance.player.Exp += 80;
            GameManager.Instance.player.LevelUp();
            GameManager.Instance.player.Gold += 1000;
        }
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
