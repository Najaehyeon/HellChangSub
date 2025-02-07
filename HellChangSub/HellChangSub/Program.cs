namespace HellChangSub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Quest.ShowQuestList();
        }
    }

    class GameManger
    {
        public GameManger() 
        {
            
        }

        public static void ShowMainScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("01");
            int choice = Utility.Select(0, 1);
            Console.WriteLine(choice);
            
            
        }
    }
}
