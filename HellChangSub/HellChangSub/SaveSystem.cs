using System;
using System.Text.Json;
namespace HellChangSub
{
    public static class SaveSystem
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "savegame.json");// filePath는 패스콤바인(기본디렉토리,저장할 파일명)

        public static void SaveGame(SaveData data)//SaveData의 객체를 받아온다
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }); //System.Text.Json에 있는 메서드를 활용 Serialize() → 객체 → JSON 문자열 변환 (직렬화) Deserialize() → JSON 문자열 → 객체 변환(역직렬화)
            File.WriteAllText(filePath, json);//(경로,데이터) 경로에 있는 파일에 데이터를 저장한다.
            Console.WriteLine("저장되었습니다.");
            Utility.PressAnyKey();
            GameManager.Instance.ShowMainScreen();
        }


        public static SaveData LoadGame()//SaveData 객체를 반환하는 로드게임 메서드
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<SaveData>(json);//경로에있는 파일을 읽어온뒤 역정렬화 해주고 이를 Plyaer객체로 반환한다.
            }

            Console.WriteLine("세이브 파일이 없습니다.");
            Utility.PressAnyKey();
            GameManager.Instance.ShowMainScreen();
            return null;
        }
    }
}