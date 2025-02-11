﻿using System.Text.Json;
using System.Text;

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
        private bool isLoaded = false;

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

        

        public void CreateObjects(bool isLoaded)
        {
            if (isLoaded)
            {
                SaveData loadedData = SaveSystem.LoadGame();//로드 메서드를 통해 saveData객체생성
                player = new Player(loadedData);//saveData를 받는 플레이어 객체 생성
                itemManager = new ItemManager(loadedData);
                SaveQuestData loadedQuestData = SaveSystem.LoadGameQuest();//퀘스트데이터 객체생성시 player의 프로퍼티값 참조필요 플레이어 객체 생성시점 뒤로 이동
                quest = new Quest(loadedQuestData);
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
            }

            ShowMainScreen();
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

        public void ShowMainScreen()
        {
            
            Console.Clear();
            Console.WriteLine("프로틴 헬창 마을에 오신 것을 환영합니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기\n2. 스테이지 진입\n3. 인벤토리\n4. 상점\n5. 대장간\n6. 퀘스트\n7. 휴식하기\n8. 저장하기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int choice = Utility.Select(1, 8);
            switch (choice)
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
                    ItPowerUp.BlacksmithScreen();
                    break;
                case 6:
                    quest.ShowQuestList();
                    break;
                case 7:
                    Rest();
                    break;
                case 8:
                    SaveData saveData = new SaveData(player, itemManager);
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
                    Console.WriteLine("체력을 회복했습니다.");
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
    }
}
