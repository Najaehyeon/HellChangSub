using System.Text.Json;
using System.Text;
using System.Media;
using NAudio.Wave;

namespace HellChangSub
{
    public delegate void Delegate();
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager.Instance.ShowStartScreen();
        }
    }

    

    class GameManager//싱글톤으로 작업 싱글톤으로 사용시 최초접근시 전역적으로 접근 가능한 객체 생성후 객체 생성 불가
    {
        public Player player;
        public ItemManager itemManager;
        public Quest quest;
        public ItemForge itemForge;
        private bool isLoaded = false;
        static WaveOutEvent outputDevice;
        static AudioFileReader audioFile;

        private static GameManager _instance; // 1️ 유일한 인스턴스를 저장할 정적 변수

        public static GameManager Instance  // 2️ 전역적으로 접근 가능한 프로퍼티
        {
            get
            {
                if (_instance == null)       // 3️ 인스턴스가 없으면 생성
                {
                    _instance = new GameManager();
                }
                return _instance;            // 4️ 인스턴스를 반환
            }
        }
        private GameManager() // 5 외부에서 생성하지 못하게 private 싱글톤 처리후 게임매니저 클래스에서 생성된 객체에 접근가능 GameManager.Instance.객체명(게임매니저내 메서드).프로퍼티
        {
            
           
        }

        public void ShowStartScreen()
        {
            Console.Clear();
            Encoding originalEncoding = Console.OutputEncoding;

            Console.OutputEncoding = Encoding.UTF8;
            string asciiArt = @"
.......................,,,........................
..................,:;+++;+++,.....................
................,+*;,,.,:;:,+,....................
...............,?;,,::;+%,..:+....................
...............:?;;;;;:::+...*:...................
................++,,,....::,:+*...................
................:*,....:;:,++**...................
.................;%*;,*%*,.;:+*,..................
..................?+:,::..:;:;:*;.................
..................:++;:;::,.++.,;+;,..............
...................,+*++;:;+:.,,,,+*;:............
............,,,,,....:?+;+:.:+;:,,..,;+...........
...........:*;;;;+:.,*,....,?,........+;..........
..........,?:.,+;+*;*,:,...?;.........,?..........
..........++..,*:,++*+;..,+%:.........;*..........
..........%,..,,:+,:%*..:?+,........:*?+..........
..........%:..:?+...??:;%:..........:?*:..........
..........:?,..,+;,,?:;%;..........,*++...........
...........:?,...:;+%,:?..........:++*............
............;?,....,;+%?........,+++*,............
.............;?,......,+:....,+++:;%+.............
..............:?:............?+,...:+.............
...............,**;,.......:?;.....,*.............
.................:;+*?;..;*+:......;%.............
....................,;%*++,..,,,,,;?%,............
......................*+::;;++;;:,,.*,............
..................................................";



            for (int i = 0; i < 2; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine(asciiArt);
                Thread.Sleep(70);
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine(asciiArt);
                Thread.Sleep(70);
            }
            for (int i = 0; i < 1; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                Console.WriteLine(asciiArt);
                Thread.Sleep(150);
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine(asciiArt);
                Thread.Sleep(150);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();
            Console.WriteLine(asciiArt);
            Console.OutputEncoding = originalEncoding;
            Console.ResetColor();
            Console.WriteLine("헬창섭의 저주");
            Utility.PressAnyKey();
            Console.Clear();
            Console.WriteLine("저장된 게임을 불러오시겠습니까?");
            Console.WriteLine("\n1. 예\n2. 아니오");
            int choice = Utility.Select(1, 2);

            if (choice == 1)
            {
                isLoaded = true;
            }
            else
            {
                isLoaded = false;
            }
            CreateObjects(isLoaded);


        }

        public void CreateObjects(bool isLoaded)
        {
            if (isLoaded)
            {
                SaveData loadedData = SaveSystem.LoadGame();//로드 메서드를 통해 saveData객체생성
                player = new Player(loadedData);//saveData를 받는 플레이어 객체 생성
                itemManager = new ItemManager(loadedData,player);
                SaveQuestData loadedQuestData = SaveSystem.LoadGameQuest();//퀘스트데이터 객체생성시 player의 프로퍼티값 참조필요 플레이어 객체 생성시점 뒤로 이동
                quest = new Quest(loadedQuestData);
                itemForge = new ItemForge(loadedData);
                History.Instance.SetHistory(loadedData);
            }
            else
            {
                Console.WriteLine("플레이어 이름을 입력해주세요.");
                Console.Write(">>");
                string playerName = Console.ReadLine();
                Console.WriteLine("직업을 정해주세요.\n1. 전사\n2. 도적\n3. 마법사");
                int playerJob = Utility.Select(1, 3);
                player = new Player(playerName, playerJob); //
                itemManager = new ItemManager(player);
                quest = new Quest();
                itemForge = new ItemForge();
            }

            ShowMainScreen();
        }

        

        public void ShowMainScreen()
        {
            
            Console.Clear();
            Console.WriteLine("프로틴 헬창 마을에 오신 것을 환영합니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기\n2. 스테이지 진입\n3. 인벤토리\n4. 상점\n5. 대장간\n6. 퀘스트\n7. 휴식하기 (100 G)\n8. 저장하기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            switch (Utility.Select(1, 8))
            {
                case 1:
                    player.ShowStatus();
                    break;
                case 2:
                    Stage stage = new Stage(player, History.Instance.StageLvl);//도전 층수에대해 고민 스테이지 진입시 도전층수 선택? 히스토리에 지금까지 클리어한 도전층수 저장 게임 승리시 히스토리에 프로퍼티 수정 만약 5층 진입시 헬창섭 소환
                    break;
                case 3:
                    itemManager.InventoryScene();
                    break;
                case 4:
                    itemManager.ShopScene();
                    break;
                case 5:
                    itemForge.BlacksmithScreen();
                    break;
                case 6:
                    quest.ShowQuestList();
                    break;
                case 7:
                    Rest();
                    break;
                case 8:
                    SaveData saveData = new SaveData(player, itemManager, itemForge);
                    SaveQuestData saveQuestData = new SaveQuestData(quest);
                    SaveSystem.SaveGame(saveData,saveQuestData);//저장기능
                    break;
            }
        }

        public void Rest()
        {
            while (true)
            {
                if (player.Gold >= 100)
                {
                    int maxHP = player.MaximumHealth;
                    player.CurrentHealth = maxHP;
                    int maxMP = player.MaximumMana;
                    player.CurrentMana = maxMP;
                    Console.WriteLine("HP,MP를 전부 회복했습니다.\n100 G 지불했습니다.");
                    Utility.PressAnyKey();
                    ShowMainScreen();
                }
                else
                {
                    Console.WriteLine("골드가 부족합니다.");
                    Utility.PressAnyKey();
                    ShowMainScreen();
                }
            }
        }

        public void EndingScene()
        {
            Console.Clear();
            Console.WriteLine("──────────────────────────────────────");
            Console.WriteLine("             최후의 일격  ");
            Console.WriteLine("──────────────────────────────────────");
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("  『 으아아아아아아!! 』");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("  정상화의 신 '헬창섭'이 거대한 근육의 파도를 일으키며 쓰러진다...");
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("  『 이건... 정상화가 아니다...!!! 』");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("  헬창섭의 몸이 금빛으로 빛나며 사라져 간다.");
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("  그리고... 그가 남긴 마지막 말이 귓가를 울린다.");
            Thread.Sleep(500);
            Console.WriteLine();
            string line = "  『 진정한 정상화란... 힘으로 만드는 것이 아니었다... 』";
            char[] chars = line.ToCharArray();
            foreach (char c in chars) { Console.Write(c); Thread.Sleep(150); }
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("──────────────────────────────────────");
            Console.WriteLine();
            Console.WriteLine("  세상은 다시 평화를 되찾았다.");
            Thread.Sleep(800);
            Console.WriteLine();
            Console.Write("  그러나"); Thread.Sleep(500); Console.Write("."); Thread.Sleep(500); Console.Write("."); Thread.Sleep(500); Console.Write("."); Thread.Sleep(500); Console.WriteLine(".");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("  플레이어는 공허한 마음으로 폐허가 된 세계를 바라본다.");
            Thread.Sleep(800);
            Console.WriteLine();
            Console.WriteLine("  『 나는... 정말 옳은 길을 걸은 것일까? 』");
            Thread.Sleep(800);
            Console.WriteLine();
            Console.WriteLine("──────────────────────────────────────");
            Console.WriteLine("  새로운 시대가 시작되려 하고 있다...");
            Thread.Sleep(800);
            Console.WriteLine();
            Console.WriteLine("  그리고... 당신의 전설은 영원히 기억될 것이다.");
            Thread.Sleep(800);
            Console.WriteLine();
            Console.WriteLine("──────────────────────────────────────");
            Console.WriteLine();
            Console.WriteLine("  [ 엔딩 크레딧이 시작됩니다... ]");
            Console.WriteLine();
            Utility.PressAnyKey();            
            ShowCredits();
        }

        public void ShowCredits()
        {
            string musicPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bgm.wav");
            outputDevice = new WaveOutEvent(); // 오디오 장치 초기화
            audioFile = new AudioFileReader(musicPath); // 파일 로드
            outputDevice.Init(audioFile); // 오디오 출력 장치에 파일 연결
            outputDevice.Play(); // 음악 재생

            Console.Clear();
            int screenHeight = Console.WindowHeight; // 콘솔 창 높이
            int screenWidth = Console.WindowWidth;   // 콘솔 창 너비
            Encoding originalEncoding = Console.OutputEncoding;
            Console.OutputEncoding = Encoding.UTF8;

            List<string> credits = new List<string>
        {
            "──────────────────────────────────────",
            "            🎬 엔딩 크레딧 🎬",
            "──────────────────────────────────────",
            "",
            "✨ 기획 및 제작 ✨",
            "고생한 우리 팀원 모두",
            "",
            "",
            "❤나재현❤",
            "든든한 팀장, 퀘스트 기능 담당",
            "",
            "",
            "❤유준영❤",
            "발표 담당, 장비강화, 대장간 구현",
            "부족한 실력이지만 팀원분들이 많이 도와주셔서 잘 진행할 수 있었습니다.",
            "",
            "❤류석민❤",
            "Item,ItemManager,ItemUtil 파트 담당",
            "버스 달달했습니다",
            "",
            "❤양훈모❤",
            "Player, Battle 파트 담당 ",
            "머지할때 너무 무섭습니다...",
            "",
            "❤황희돈❤",
            "게임매니저, 몬스터, 저장기능 담당",
            "다같이 끝까지 화이팅!",
            "",
            "🎮 주요 등장인물 🎮",
            "정상화의 신 헬창섭",
            "전설의 용사 (당신)",
            "수많은 몬스터들과 NPC들",
            "",
            "🎙️ 명대사 🎙️",
            "'이건... 정상화가 아니다!' - 헬창섭",
            "'나는... 정말 옳은 길을 걸은 것일까?' - 플레이어",
            "정상화의 신 '헬창섭'이 거대한 근육의 파도를 일으키며 쓰러진다...",
            "",
            "🎮 플레이해 주셔서 감사합니다! 🎮",
            "",
            "──────────────────────────────────────",
            "",
            "  『 게임을 종료하려면 아무 키나 누르세요 』"
        };

            int totalLines = credits.Count;  // 크레딧 전체 줄 개수
            int startLine = screenHeight;    // 크레딧이 시작할 위치 (화면 아래쪽에서 시작)

            // 크레딧을 화면 아래에서부터 위로 스크롤
            for (int i = 0; i < totalLines + screenHeight; i++)
            {
                Console.Clear();

                // 크레딧이 화면 위로 올라가는 연출
                for (int j = 0; j < totalLines; j++)
                {
                    int currentLine = startLine - i + j; // 현재 출력할 줄 위치 계산
                    if (currentLine >= 0 && currentLine < screenHeight)
                    {
                        Console.SetCursorPosition((screenWidth - credits[j].Length) / 2, currentLine);
                        Console.Write(credits[j]);
                    }
                }

                Thread.Sleep(200); // 크레딧 속도 조절 (더 빠르게 하고 싶으면 숫자를 줄임)
            }
            Console.OutputEncoding = originalEncoding;
            Console.SetCursorPosition(0, screenHeight - 1);
            StopMusic();
            Console.Write("  『 게임을 종료하려면 아무 키나 누르세요 』");
            Console.ReadKey(); // 종료 대기
            ShowMainScreen();
        }

        static void StopMusic()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop(); // 재생 중지
                outputDevice.Dispose(); // 리소스 해제
            }

            if (audioFile != null)
            {
                audioFile.Dispose(); // 파일 리소스 해제
            }
        }
    }
}

