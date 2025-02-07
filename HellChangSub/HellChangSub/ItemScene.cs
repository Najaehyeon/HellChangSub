using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class ItemScene
    {
        Player player;

        public ItemScene(Player player)
        {
            this.player = player;
        }
        //string name, int value, string description, int price, ItemType itemtype
        public List<EquipItem> equipItems = new List<EquipItem>
        {
            new EquipItem("수련자의 갑옷", 4, "수련에 도움을 주는 갑옷입니다. ", 1000, ItemType.Armor),
            new EquipItem("무쇠갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다. ", 2000, ItemType.Armor),
            new EquipItem("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ", 3500, ItemType.Armor),
            new EquipItem("낣은 검", 5, "쉽게 볼 수 있는 낡은 검 입니다. ", 600, ItemType.Weapon),
            new EquipItem("청동 도끼", 10, "어디선가 사용됐던거 같은 도끼입니다. ", 1500, ItemType.Weapon),
            new EquipItem("스파르타의 창", 20, "스파르타의 전사들이 사용했다는 전설의 창입니다. ", 2500, ItemType.Weapon)
        };
        public List<EquipItem> equipInventory = new List<EquipItem>();
        //string itemname, int value, string description, int price, ItemType itemType
        public List<UseItem> useItems = new List<UseItem>()
        {
            new UseItem("체력포션", 30, "체력 상승", 50, ItemType.HpPotion),
            new UseItem("힘포션", 30, "체력 상승", 50, ItemType.HpPotion),
            new UseItem("방어포션", 30, "체력 상승", 50, ItemType.HpPotion)
        };
        public List<UseItem> useInventory = new List<UseItem>();

        public void Add()
        {
            equipInventory.Add(equipItems[0]);
            useInventory.Add(useItems[0]);
        }

        public void InventoryScene()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
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
                Console.WriteLine($"- {useItems[i].UseItemStatus()}");
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
        public void EquipScene()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[장비 목록]");
            for (int i = 0; i < equipInventory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {equipInventory[i].EquipInvenStatus()}");
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
                    EquipChange(player, input);
                    break;
            }
        }

        public void ShopScene()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("구매를 원하는 아이템의 종류를 선택해주세요");
            Console.WriteLine("1. 장비 아이템");
            Console.WriteLine("2. 소비 아이템");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, 2);
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
            }
        }

        public void EquipShopScene()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 장비를 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[장비 아이템]");
            for (int i = 0; i < equipItems.Count; i++)
            {
                Console.WriteLine($"{i+1} {equipItems[i].EquipItemStatus()}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, equipItems.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    if (equipItems[input-1].isPurchase)
                    {
                        Console.WriteLine("품절입니다.");
                    }
                    else
                        EquipBuy(player, input);
                    break;
            }
        }

        public void UseShopScene()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 장비를 얻을 수 있는 상점입니다.");
            Console.WriteLine();

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine();

            Console.WriteLine("[소비 아이템]");
            for (int i = 0; i < useItems.Count; i++)
            {
                Console.WriteLine($"{i + 1} {useItems[i].UseShopStatus()}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, useItems.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    UseBuy(player, input);
                    break;
            }
        }

        public void UseBuy(Player player, int input)
        {
            UseItem item = useItems[input - 1];
            player.Gold -= item.Price;
            item.Count++;
            EquipShopScene();
        }

        public void UseSell(Player player, int input)
        {
            UseItem item = useItems[input - 1];
            player.Gold += (item.Price / 2);
            item.Count--;
            EquipShopScene();
        }

        public void EquipBuy(Player player, int input)
        {
            EquipItem item = equipItems[input - 1];
            player.Gold -= item.Price;
            equipInventory.Add(item);
            EquipShopScene();
        }

        public void EquipSell(Player player, int input)
        {
            EquipItem item = equipItems[input - 1];
            player.Gold += (item.Price / 2);
            equipInventory.Remove(item);
            EquipShopScene();
        }

        //장착 메서드들
        public void EquipChange(Player player, int input)
        {
            EquipItem item = equipInventory[input - 1];
            for (int i = 0; i < equipInventory.Count; i++)
            {
                if (equipInventory[i].isEquip && item != equipInventory[i] && equipInventory[i].ItemType == item.ItemType)
                {
                    UnEquip(player, item);
                }
            }
            Equip(player, item);

            EquipScene();
        }
        public void Equip(Player player, EquipItem item)
        {
            if (!item.isEquip)
            {
                item.isEquip = true;
                if (item.ItemType == ItemType.Weapon)
                {
                    player.EquipAtk += item.Value;
                }
                else
                {
                    player.EquipDef += item.Value;
                }
            }
            else
                UnEquip(player, item);
        }

        public void UnEquip(Player player, EquipItem item)
        {
            if (item.isEquip)
            {
                item.isEquip = false;
                if (item.ItemType == ItemType.Weapon)
                {
                    player.EquipAtk -= item.Value;
                }
                else
                {
                    player.EquipDef -= item.Value;
                }
            }
        }
        
    }
}
