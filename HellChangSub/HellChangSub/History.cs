using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    internal class History
    {
        private static History _instance; // 1️ 유일한 인스턴스를 저장할 정적 변수

        public static History Instance  // 2️ 전역적으로 접근 가능한 프로퍼티
        {
            get
            {
                if (_instance == null)       // 3️ 인스턴스가 없으면 생성
                    _instance = new History();
                return _instance;            // 4️ 인스턴스를 반환
            }
        }

        History.Instance.

        private History()
        {

        }
        public History() { }
        public int MonsterKill {  get; set; }
        public bool QuestClear1 { get; set; }

    }
}
