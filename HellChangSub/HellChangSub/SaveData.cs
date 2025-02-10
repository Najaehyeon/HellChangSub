using System;
using System.IO;
using System.Text.Json;
namespace HellChangSub
{
    [Serializable]//직렬화 가능하게 해주는 어트리뷰트(속성)  부여
    public class SaveData// 아이템 리스트 인벤토리및 상점 아이템 , 히스토리내 기록 프로퍼티 저장 필요 추가로 로드 기능 추가 필요
    {
        public string Name { get; set; }
        public int JobCode { get; set; }
        public string JobName { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int CurrentHealth { get; set; }
        public int MaximumHealth { get; set; }
        public int CurrentMana { get; set; }
        public int MaximumMana { get; set; }
        public float Atk { get; set; }
        public float EquipAtk { get; set; }
        public int Def { get; set; }
        public int EquipDef { get; set; }
        public int Gold { get; set; }
        public float Crit { get; set; }
        public float CritDamage { get; set; }
        public float Evasion { get; set; }

        public List<EquipItem> equipItems { get; set; }
        public List<EquipItem> equipInventory { get; set; }
        public List<UseItem> useItems;
        public int stageLvl;
        public List<QuestData> questDataList {  get; set; }

        public SaveData() { }//로드시 매개변수 없는 객체 생성을 위해 오버로드

        public SaveData(Player player,ItemManager itemManager,Quest quest)//저장시 플레이어 객체의 프로퍼티를 받아옴 향후 히스토리의 값을 받아와야함 히스토리는 싱글톤
        {
            Name = player.Name;
            JobCode = player.JobCode;
            JobName = player.JobName;
            Level = player.Level;
            Exp = player.Exp;
            CurrentHealth = player.CurrentHealth;
            MaximumHealth = player.MaximumHealth;
            MaximumMana = player.MaximumMana;
            CurrentMana = player.CurrentMana;
            Atk = player.Atk;
            EquipAtk = player.EquipAtk;
            Def = player.Def;
            EquipDef = player.EquipDef;
            Gold = player.Gold;
            Crit = player.Crit;
            CritDamage = player.CritDamage;
            Evasion = player.Evasion;
            equipItems = itemManager.equipItems;
            equipInventory = itemManager.equipInventory;
            useItems = itemManager.useItems;//itemamanager 생성자 신규생성필요
            stageLvl = History.Instance.stageLvl;
            questDataList = quest.questDataList;

        }


    }
}
