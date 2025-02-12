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
            ShowDialogue();
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

        public abstract void ShowDialogue();
    }


    // "마을을 위협하는 미니언 처치" 퀘스트 데이터
    public class KillMinionQuest : QuestData
    {
        public override string Title { get; } = "마을을 위협하는 슬라임 처치!";
        public override string Mission { get; } = "슬라임 3마리 처치!";
        public override string[] Rewards { get; } = new string[] { "스파르타의 케틀벨", "500 Gold", "EXP +20" };
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
            GameManager.Instance.player.Exp += 20;
            GameManager.Instance.player.LevelUp();
        }

        public override void ShowDialogue()
        {
            Console.WriteLine("마을을 지나 서쪽으로 가면 ‘헬스 던전’이 있어.\r\n그곳에는… 기괴한 슬라임들이 우글거리지.\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("평범한 슬라임이 아니라, 근육이 불룩불룩한 괴물들이야!\n맨날 던전 안에서 덤벨을 들고 으르렁거리면서 운동을 한다고.\n처음엔 그냥 신기한 구경거리였는데…\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("문제는 요즘 그놈들이 던전 밖으로 나오기 시작했다는 거야.\n마을 주민들을 붙잡고 “운동했냐?”, “가슴 운동은 했냐?” 같은 말을 해댄다고…\n심지어 지나가는 사람한테 단백질 쉐이크를 억지로 먹이려는 놈들도 있대!\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("이러다간 마을 전체가 근육 슬라임들에게 점령당할지도 몰라…\n부탁이야! 헬스 던전에 들어가서 근육 슬라임 10마리를 처치해줘!\n");
            Utility.PressAnyKey();
            Console.Clear();
        }
    }


    // "장비 장착하기" 퀘스트 데이터
    public class EquipShieldQuest : QuestData
    {
        public override string Title { get; } = "장비를 구매하여 장착해보자.";
        public override string Mission { get; } = "장비 장착하기";
        public override string[] Rewards { get; } = new string[] { "EXP +10", "200 Gold" };
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
            GameManager.Instance.player.Exp += 10;
            GameManager.Instance.player.LevelUp();
            GameManager.Instance.player.Gold += 200;
        }

        public override void ShowDialogue()
        {
            Console.WriteLine("좋아, 모험을 떠난다고? 하지만 지금 너를 보면… 음… 너무 허술하군.\n이러다가 바람만 불어도 쓰러지는 거 아니야?\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("모험가라면 장비를 제대로 갖춰야 해!\n갑옷이든, 검이든, 활이든 네 몸에 맞는 장비를 장착하는 법부터 익혀야 한다고!\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("장비를 착용하면 너의 힘이 한층 더 강해질 거야.\n물론, 장비만 믿고 방심하면 안 된다.\n하지만 최소한… 평범한 슬라임한테 지지는 않겠지?\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("얼른 아무 장비나 착용하고 돌아와!\n네가 진정한 모험가로서 첫걸음을 뗐는지 확인해 보겠어.\n");
            Utility.PressAnyKey();
            Console.Clear();
        }
    }


    // "더욱 더 강해지기" 퀘스트 데이터
    public class StrongMoreQuest : QuestData
    {
        public override string Title { get; } = "더욱 더 강해지기!";
        public override string Mission { get; } = "7레벨 달성하기";
        public override string[] Rewards { get; } = new string[] { "EXP + 30", "1000 Gold" };
        public override int Goal { get; } = 7;

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
            GameManager.Instance.player.Exp += 30;
            GameManager.Instance.player.LevelUp();
            GameManager.Instance.player.Gold += 1000;
        }

        public override void ShowDialogue()
        {
            Console.WriteLine("좋아, 이제 막 모험을 시작했구나.\n하지만 지금의 너로는… 음, 솔직히 말해서 너무 약해!\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("이 세상은 강한 자만이 살아남는 법.\n적들이 점점 강해질 텐데, 지금처럼 허접한 상태로 싸우려고?\n그럼 그냥 몬스터 밥이 되겠지!\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("강해지고 싶다면 레벨 7까지 올려라.\n전투를 하든, 퀘스트를 수행하든, 뭐든 좋다.\n경험치를 쌓고, 네 몸을 단련하는 거야!\n");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("레벨 7에 도달하면 넌 지금과는 전혀 다른 사람이 될 거다.\n힘도 강해지고, 새로운 스킬도 배울 수 있을 거야.\n");
            Utility.PressAnyKey();
            Console.Clear();
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
