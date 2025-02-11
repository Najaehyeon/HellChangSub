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

        public int StageLvl { get; set; } = 1;
        public int ChallengeLvl { get; set; } = 1;
        
        private History() { }

        public void SetHistory(SaveData saveData)
        {
            StageLvl = saveData.stageLvl;
        }
    }
}
