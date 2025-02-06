namespace HellChangSub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManger gb = new GameManger();

            gb.ShowMainScreen();
        }
    }

    class GameManger
    {
        public GameManger() 
        { 
        
        }

        public void ShowMainScreen()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n원하시는 이름을 입력해주세요.");
            Console.Write(">> ");
            string name = Console.ReadLine(); // 여기 name 값을 캐릭터가 객체로 생성될 때 넣어줘야함.

            int choice = Utility.Select("직업 선택", new string[] { "전사", "마법사", "엘프"}, "원하시는 직업을 선택해주세요."); // 선택지 생성

            switch (choice)
            {
                case 1:
                    Console.WriteLine("전사 선택");
                    break;
            }
        }
    }   
}
