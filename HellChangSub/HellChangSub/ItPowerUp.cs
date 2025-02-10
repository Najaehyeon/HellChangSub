using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public enum StoneType // 강화석 종류를 나타내는 열거형
    {
        WeaponPowerStone,  // 무기강화석
        AmorPowerStone    // 방어구
    }
    public class ItPowerUp
    {
        class ArPowerStone 
        {
            public string Name { get; }
            public string Description { get; }
            public int Value { get; set; }
            public ItemType ItemType { get; }
        }
        class WpPowerStone
        { 
            public string Name { get;}
            public string Description { get; }
            public int Value { get; set; }
            public ItemType ItemType { get; }

        }
        
        public void BlacksmithScreen() //대장간
        {
            Console.Clear();
            Console.WriteLine("대장간에 온것을 환영하네!");
            Console.WriteLine("이곳에서는 자네의 장비를 강화할 수 있다네");
            Console.WriteLine("장비를 강화해볼텐가?");
            Console.WriteLine();

            while (true)
            {

                Console.WriteLine("1. 강화하기");
                Console.WriteLine("2. 나가기");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        //강화 씬

                        break;

                    case "2":
                        Console.Clear();
                        //돌아가기
                        break;

                    default:
                        Console.WriteLine("잘못된 입력입니다 올바른 숫자를 입력해 주세요.");
                        continue;

                }
            }
        }
        //대장간 스크립트
        //강화 스크립트
        //강화 확율
        //아이템 카운트 기능

    }
}
