using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Utility
    {
        public static int Select(int min, int max)//숫자 선택시 사용 예시 
        {
            while (true)
            {
                Console.Write(">> ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >=min && choice <=max)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public static void PressAnyKey()
        {
            Console.WriteLine("진행하려면 아무 키나 눌러주세요.");
            Console.ReadKey();
        }

        //실제 길이 계산기 (한글이면 +2 나머지 +1)
        public static int GetWidth(string input)
        {
            int width = 0;
            foreach (char c in input)
            {
                if(c >= '가' && c <= '힣')
                {
                    width += 2;
                }
                else
                    width += 1;
            }
            return width;
        }

        //오른쪽 pad랑 글자길이 합해서 원하는 width 나오게끔하기 예시)13칸을 차지하는 string을 출력할 경우 Console.WriteLine("FixWidth("원하는 내용을 입력하세요", 13)");
        public static string FixWidth(string input, int width)
        {
            int realWidth = GetWidth(input);
            int pad = width - realWidth;

            return input.PadRight(input.Length + pad);
        }
    }
}
