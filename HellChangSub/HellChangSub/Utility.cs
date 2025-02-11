using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Utility
    {
        public static int Select(int min, int max)
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

        public static int GetWidth(string input)
        {
            int width = 0;
            foreach (char c in input)
            {
                if(c > '가' && c < '힣')
                {
                    width += 2;
                }
                else
                    width += 1;
            }
            return width;
        }

        public static string FixWidth(string input, int width)
        {
            int realWidth = GetWidth(input);
            int pad = width - realWidth;

            return input.PadRight(input.Length + pad);
        }
    }
}
