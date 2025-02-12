using System;
using System.IO;
using Newtonsoft.Json;  // Newtonsoft.Json 사용

namespace HellChangSub
{
    public static class SaveSystem
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "savegame.json");
        private static string questfilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "savegamequest.json");
        public static void SaveGame(SaveData data,SaveQuestData questData)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All  // 클래스 타입 정보를 포함하여 저장
                });

            File.WriteAllText(filePath, json);
            string questjson = JsonConvert.SerializeObject(questData, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All  // 클래스 타입 정보를 포함하여 저장
                });

            File.WriteAllText(questfilePath, questjson);
            Console.WriteLine("저장되었습니다.");
            Utility.PressAnyKey();
            GameManager.Instance.ShowMainScreen();
        }

        public static SaveData LoadGame()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<SaveData>(json,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All  // 저장된 타입 정보를 사용하여 객체 생성
                    });
            }

            Console.WriteLine("세이브 파일이 없습니다.");
            Utility.PressAnyKey();
            GameManager.Instance.AskLoadGame();
            return null;
        }

        public static SaveQuestData LoadGameQuest()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(questfilePath);
                return JsonConvert.DeserializeObject<SaveQuestData>(json,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All  // 저장된 타입 정보를 사용하여 객체 생성
                    });
            }

            Console.WriteLine("세이브 파일이 없습니다.");
            Utility.PressAnyKey();
            GameManager.Instance.AskLoadGame();
            return null;
        }
    }
}
