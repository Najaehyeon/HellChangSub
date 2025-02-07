using System.Text.Json;


namespace HellChangSub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager.Instance.ShowMainScreen();
        }
    }

    class GameManager//싱글톤으로 작업 싱글톤으로 사용시 최초접근시 전역적으로 접근 가능한 객체 생성후 객체 생성 불가
    {
        Player player;
        ItemManager itemManager;
        private static GameManager _instance; // 1️ 유일한 인스턴스를 저장할 정적 변수

        public static GameManager Instance  // 2️ 전역적으로 접근 가능한 프로퍼티
        {
            get
            {
                if (_instance == null)       // 3️ 인스턴스가 없으면 생성
                    _instance = new GameManager();
                return _instance;            // 4️ 인스턴스를 반환
            }
        }
        private GameManager() // 5 외부에서 생성하지 못하게 private
        {
            Console.WriteLine("저장된 게임을 불러오시겠습니까?");
            Console.WriteLine("\n1. 예\n2. 아니오");
            int choice = Utility.Select(1, 2);
            
            if (choice == 1)
            {
                SaveData saveData = SaveSystem.LoadGame();
                player = new Player(saveData);
            }
            else
            {
                Console.WriteLine("플레이어 이름을 입력해주세요.");
                Console.Write(">>");
                string playerName = Console.ReadLine();
                Console.WriteLine("직업을 정해주세요.\n1. 전사\n2. 도적\n3. 마법사");
                int playerJob = Utility.Select(1, 3);
                player = new Player(playerName, playerJob); //
            }
            itemManager = new ItemManager(player);
        }

        public void ShowMainScreen()
        {
            Console.Clear();
            Console.WriteLine("프로틴 헬창 마을에 오신 것을 환영합니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기\n2. 스테이지 진입\n3. 인벤토리\n4. 상점\n5. 퀘스트\n6. 저장하기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int choice = Utility.Select(1, 6);
            switch (choice)
            {
                case 1:
                    player.ShowStatus();
                    break;
                case 2:
                    Stage stage = new Stage(player, 1);//도전 층수에대해 고민 스테이지 진입시 도전층수 선택? 히스토리에 지금까지 클리어한 도전층수 저장 게임 승리시 히스토리에 프로퍼티 수정 만약 5층 진입시 헬창섭 소환
                    break;
                case 3:
                    itemManager.InventoryScene();
                    break;
                case 4:
                    itemManager.ShopScene();
                    break;
                case 5:
                    Quest.ShowQuestList();
                    break;
                case 6:
                    SaveData saveData = new SaveData(player);
                    SaveSystem.SaveGame(saveData);//저장기능
                    break;
            }
        }
    }
}
