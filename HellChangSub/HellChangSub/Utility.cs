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
    }
}
