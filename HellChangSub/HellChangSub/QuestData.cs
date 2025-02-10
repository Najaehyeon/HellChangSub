﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public abstract class QuestData<T>
    {
        public abstract string Title { get; }
        public abstract string Mission { get;  }
        public abstract string[] Rewards { get; }
        public abstract T Goal { get; }
        public abstract T Progressed { get; set; }
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
            JudgeState();
            if (QuestState == QuestState.NotStarted) // 수행한 적이 없을 때, 보여줄 내용
            {
                Console.WriteLine("1. 수락\n2. 거절");

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
                Console.WriteLine($"{Progressed} / {Goal}\n");
                Console.WriteLine("1. 미션포기\n0. 나가기");

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
                Console.WriteLine("1. 보상 받기");

                int choice = Utility.Select(1, 1);

                switch (choice)
                {
                    case 1: // 보상 받기
                        GiveRewards();
                        QuestState = QuestState.RewardClaimed;
                        Console.WriteLine("보상을 받았습니다.");
                        Console.WriteLine("\n0. 나가기");
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
            Console.WriteLine($"\"{Title}\" 퀘스트를 수락했습니다!");
            Console.WriteLine("\n0. 나가기");
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
            Console.WriteLine("\n0. 나가기");
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
            if (Progressed is int progressInt && Goal is int goalInt)
            {
                if (progressInt >= goalInt) // 정수형일 경우, 크거나 같을 때 완료
                {
                    QuestState = QuestState.Completed;
                }
            }
            else if (Progressed is string progressStr && Goal is string goalStr)
            {
                if (progressStr == goalStr) // 문자열일 경우, 동일할 때 완료
                {
                    QuestState = QuestState.Completed;
                }
            }
        }

        // 퀘스트를 포기했을 때, 수행한 미션 초기화
        public abstract void FormatMission();

        //
        public abstract void GiveRewards();
    }


    // "마을을 위협하는 미니언 처치" 퀘스트 데이터
    public class KillMinionQuest : QuestData<int>
    {
        public override string Title { get; } = "마을을 위협하는 미니언 처치!";
        public override string Mission { get; } = "미니언 10마리 처치!";
        public override string[] Rewards { get; } = new string[] { "쓸만한 방패", "500 Gold" };
        public override int Goal { get; } = 10;


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

        public override QuestState QuestState { get; set; } = QuestState.NotStarted;

        public override void FormatMission()
        {
            progressed = 0;
        }

        public override void GiveRewards()
        {

            GameManager.Instance.player.Gold += 500;
        }
    }


    // "장비 장착하기" 퀘스트 데이터
    public class EquipShieldQuest : QuestData<string>
    {
        public override string Title { get; } = "장비를 장착해보자.";
        public override string Mission { get; } = "쓸만한 방패 장착하기";
        public override string[] Rewards { get; } = new string[] { "EXP +50", "200 Gold" };
        public override string Goal { get; } = "쓸만한 방패 장착하기";


        private string progressed = "장착 안됨";
        public override string Progressed
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

        public override QuestState QuestState { get; set; } = QuestState.NotStarted;

        public override void FormatMission()
        {
            progressed = "장착 안됨";
        }

        public override void GiveRewards()
        {
            GameManager.Instance.player.Exp += 50;
            GameManager.Instance.player.Gold += 200;
        }
    }


    // "더욱 더 강해지기" 퀘스트 데이터
    public class StrongMoreQuest : QuestData<string>
    {
        public override string Title { get; } = "더욱 더 강해지기!";
        public override string Mission { get; } = "10레벨 달성하기";
        public override string[] Rewards { get; } = new string[] { "AK-47", "EXP + 80", "1000 Gold" };
        public override string Goal { get; } = "Lv.10";


        private string progressed = $"Lv.{GameManager.Instance.player.Level}"; 
        public override string Progressed
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

        public override QuestState QuestState { get; set; } = QuestState.NotStarted;

        public override void FormatMission()
        {
            progressed = $"Lv.{GameManager.Instance.player.Level}";
        }

        public override void GiveRewards()
        {
            GameManager.Instance.player.Exp += 80;
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
