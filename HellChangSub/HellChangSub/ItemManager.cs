using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class ItemManager
    {
        Player player;
        public ItemUtil itemUtil;
        public bool purchased = false;
        public bool goldRequired = false;
        public bool noItem = false;
        public bool equippedItem = false;
        public ItemManager(SaveData saveData,Player player)
        {
            this.player = player;
            itemUtil = new ItemUtil(this);
            equipItems = saveData.equipItems;
            equipInventory = saveData.equipInventory;
            useItems = saveData.useItems;
        }
        public ItemManager(Player player)
        {
            this.player = player;
            itemUtil = new ItemUtil(this);
        }
        //string name, int value, string description, int price, ItemType itemtype
        public List<EquipItem> equipItems = new List<EquipItem>
        {
            new EquipItem("낡은 덤벨", 10, "쉽게 볼 수 있는 낡은 덤벨이다.", 600, ItemType.Weapon,0),
            new EquipItem("청동 바벨", 30, "어디선가 사용됐던 것 같은 바벨.", 1500, ItemType.Weapon, 0),
            new EquipItem("스파르탄 케틀벨", 50, "전설의 스파르탄이 사용했던 케틀벨.", 2500, ItemType.Weapon, 0),
            new EquipItem("수련자의 조끼", 5, "초보 헬창을 위한 조끼.", 1000, ItemType.Armor, 0),
            new EquipItem("언더아머압박조끼", 10, "근력 운동 중 부상을 막아준다. ", 2000, ItemType.Armor, 0),
            new EquipItem("스파르탄중량조끼", 20, "전설의 스파르탄이 착용했던 조끼.", 3500, ItemType.Armor, 0)
        };
        public List<EquipItem> equipInventory = new List<EquipItem>();
        //string itemname, int value, string description, int price, ItemType itemType
        public List<UseItem> useItems = new List<UseItem>()
        {
            new UseItem("체력포션", 50, "체력 회복", 50, ItemType.HpPotion,0),
            new UseItem("마나포션", 50, "마나 회복", 50, ItemType.MpPotion, 0),
            new UseItem("단백질 쉐이크", 20, "공격력 상승", 50, ItemType.AtkPotion, 0),
            new UseItem("관절 보호제", 20, "방어력 상승", 50, ItemType.DefPotion, 0)
        };

        //인벤토리
        public void InventoryScene()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            Console.WriteLine("[장비 아이템]");
            for (int i = 0; i < equipInventory.Count; i++)
            {
                Console.WriteLine($"- {equipInventory[i].EquipInvenStatus()}");
            }
            Console.WriteLine("[소비 아이템]");
            for (int i = 0; i < useItems.Count; i++)
            {
                if (useItems[i].Count == 0)
                    continue;
                Console.WriteLine($"- {Utility.FixWidth(useItems[i].ItemName, 10)}  | {Utility.FixWidth(useItems[i].Count.ToString(), 5)} 개");
            }

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, 1);
            switch (input)
            {
                case 0:
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:
                    EquipScene();
                    break;
            }
        }

        //장착관리
        public void EquipScene()
        {
            Console.Clear();
            Console.WriteLine("[인벤토리 - 장착 관리]");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[장비 목록]");
            for (int i = 0; i < equipInventory.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i+1}",3)}. {equipInventory[i].EquipInvenStatus()}");
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, equipInventory.Count);
            switch (input)
            {
                case 0:
                    InventoryScene();
                    break;
                default:
                    itemUtil.EquipChange(player, input);
                    break;
            }
        }

        //상점 - 목록
        public void ShopScene()
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("구매를 원하는 아이템의 종류를 선택해주세요");
            Console.WriteLine("1. 장비 아이템 구매");
            Console.WriteLine("2. 소비 아이템 구매");
            Console.WriteLine("3. 장비 아이템 판매");
            Console.WriteLine("4. 소비 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, 4);
            switch (input)
            {
                case 0:
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:
                    EquipShopScene();
                    break;
                case 2:
                    UseShopScene();
                    break;
                case 3:
                    EquipSellScene();
                    break;
                case 4:
                    UseSellScene();
                    break;
            }
        }

        //장비 상점
        public void EquipShopScene()
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("필요한 장비를 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[장비 아이템]");
            for (int i = 0; i < equipItems.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i+1}", 3)} {equipItems[i].EquipItemStatus()}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            itemUtil.SoldOut(purchased);
            itemUtil.GoldRequired(goldRequired); //구매완료,재화부족 message 
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, equipItems.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    itemUtil.EquipBuy(player, input);
                    break;
            }
        }

        public void EquipSellScene()
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("장비를 팔수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[장비 아이템]");
            for (int i = 0; i < equipInventory.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i+1}", 3)} {equipInventory[i].EquipSellStatus()}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            itemUtil.CantSell(noItem);
            itemUtil.EquippedItem(equippedItem);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, equipInventory.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    itemUtil.EquipSell(player, input);
                    break;
            }
        }

        //소비 상점
        public void UseShopScene()
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("소비 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[소비 아이템]");
            for (int i = 0; i < useItems.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i+1}", 3)} {useItems[i].UseShopStatus()}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            itemUtil.GoldRequired(goldRequired); //재화부족 message 메서드
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, useItems.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    itemUtil.UseBuy(player, input);
                    break;
            }
        }

        public void UseSellScene()
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("소비 아이템을 팔수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[소비 아이템]");
            for (int i = 0; i < useItems.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i+1}", 3)} {useItems[i].UseShopStatus()}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            itemUtil.CantSell(noItem);
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, useItems.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    itemUtil.UseSell(player, input);
                    break;
            }
        }

    }
}
