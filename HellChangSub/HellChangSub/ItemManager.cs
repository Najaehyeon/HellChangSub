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
        bool purchased = false;
        bool goldRequired = false;

        public ItemManager(Player player)
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
            new UseItem("체력포션", 50, "체력 상승", 50, ItemType.HpPotion),
            new UseItem("힘포션", 20, "공격력 상승", 50, ItemType.AtkPotion),
            new UseItem("방어포션", 20, "방어력 상승", 50, ItemType.DefPotion)
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

        //장비 상점
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

            SoldOut(purchased);
            GoldRequired(goldRequired); //구매완료,재화부족 message 메서드
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = Utility.Select(0, equipItems.Count);
            switch (input)
            {
                case 0:
                    ShopScene();
                    break;
                default:
                    EquipBuy(player, input);
                    break;
            }
        }

        //소비 상점
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

            GoldRequired(goldRequired); //재화부족 message 메서드
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

        public void SoldOut(bool purchased)
        {
            if(purchased)
                Console.WriteLine("품절된 아이템입니다.");
        }

        public void GoldRequired(bool goldRequired)
        {
            if (goldRequired)
                Console.WriteLine("골드가 부족합니다.");
        }

        public void UseBuy(Player player, int input)
        {
            UseItem item = useItems[input - 1];
            if (player.Gold < item.Price)
            {
                goldRequired = true;
            }
            else
            {
                player.Gold -= item.Price;
                item.Count++;
            }
            UseShopScene();
        }

        public void UseSell(Player player, int input)
        {
            UseItem item = useItems[input - 1];
            player.Gold += (item.Price / 2);
            item.Count--;
            UseShopScene(); // 판매씬으로 바꿔야함
        }

        public void EquipBuy(Player player, int input)
        {
            EquipItem item = equipItems[input - 1];
            if (item.isPurchase)
            {
                purchased = true;
            }
            else
            {
                if (player.Gold < item.Price)
                {
                    goldRequired = true;
                }
                else
                {
                    player.Gold -= item.Price;
                    equipInventory.Add(item);
                    item.isPurchase = true;
                    purchased = false;
                }
            }
            EquipShopScene();
        }

        public void EquipSell(Player player, int input)
        {
            EquipItem item = equipInventory[input - 1];
            player.Gold += (item.Price / 2);
            equipInventory.Remove(item);
            EquipShopScene(); // 판매씬으로 바꿔야함
        }

        //장착 메서드들
        public void EquipChange(Player player, int input)
        {
            EquipItem item = equipInventory[input - 1];
            for (int i = 0; i < equipInventory.Count; i++)
            {
                if (equipInventory[i].isEquip && item != equipInventory[i] && equipInventory[i].ItemType == item.ItemType)
                {
                    UnEquip(player, equipInventory[i]);
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

        public void UsePotion(Player player, int input)
        {
            UseItem item = useItems[input-1];
            switch(item.ItemType)
            {
                case ItemType.HpPotion:
                    player.CurrentHealth += item.Value;
                    player.CurrentHealth = player.CurrentHealth >= player.MaximumHealth ? player.MaximumHealth : player.CurrentHealth;
                    break;
                case ItemType.AtkPotion:
                    player.EquipAtk += item.Value;
                    break;
                case ItemType.DefPotion:
                    player.EquipDef += item.Value;
                    break;
            }
        }

        public void EndPotion(Player player)
        {

        }


    }
}
