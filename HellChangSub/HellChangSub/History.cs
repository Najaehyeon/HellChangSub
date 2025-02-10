using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    // 퀘스트의 진행 여부를 기록하는 클래스
    internal class History
    {
        private static History _instance;
        public static History Instance // 싱글톤을 이용해 하나의 객체로 생성하여, 퀘스트 클래스나 다른 클래스에서도 접근 가능하게 함.
        {
            get
            {
                if (_instance == null)
                    _instance = new History();
                return _instance;
            }
        }

        public int stageLvl { get; set; } = 1;

        public Dictionary<string, QuestStateData > Quests { get; private set; }

        private History()
        {
            Quests = new Dictionary<string, QuestStateData>(); // 딕셔너리로 각 퀘스트 이름에 맞는 미션을 관리 (EX. { {"미니언 5마리 처치" , <해당 퀘스트 데이터>} , {"장비 착용", <해당 퀘스트 데이터>} } )
        }

        public void SetHistory(SaveData saveData)
        {
            Quests = saveData.Quests;
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

    // 퀘스트의 목표와 진척도, 상태 데이터를 관리하는 클래스
    public class QuestStateData
    {
        public object Goal { get; private set; } // 목표 (int, bool 등 여러 타입 가능)
        public object NowProgressed { get; set; } // 진척도
        public QuestState State { get; set; } // 진행 상태

        public QuestStateData(object goal) // 생성할 때 목표를 받아오고, 상태는 NotStarted를 기본 값으로 설정
        {
            Goal = goal;
            State = QuestState.NotStarted;
        }
    }
}
