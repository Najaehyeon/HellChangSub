using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    class Utility
    {
        public static int Select(string title, string[] options, string description)
        {
            Console.WriteLine($"\n{title}\n"); // 선택 제목 출력

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i+1}. {options[i]}");
            }

            Console.WriteLine($"\n{description}");

            while (true)
            {
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= options.Length)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("❌ 올바른 번호를 입력하세요.");
                }
            }
        }
    }
}
