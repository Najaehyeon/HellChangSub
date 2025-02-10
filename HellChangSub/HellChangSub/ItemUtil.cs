using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    internal class ItemUtil
    {
        private ItemManager ItemManager;

        public ItemUtil(ItemManager itemManager)
        {
            this.ItemManager = itemManager;
        }

        //구매 불가, 판매 불가 조건
        public void SoldOut(bool purchased)
        {
            if (purchased)
            {
                Console.WriteLine("품절된 아이템입니다.");
                ItemManager.purchased = false;
            }
        }

        public void GoldRequired(bool goldRequired)
        {
            if (goldRequired)
            {
                Console.WriteLine("골드가 부족합니다.");
                ItemManager.goldRequired = false;
            }
        }

        public void CantSell(bool noItem)
        {
            if(noItem)
            {
                Console.WriteLine("해당 아이템이 없습니다");
                ItemManager.noItem = false;
            }
        }

        public void EquiptedItem(bool equiptedItem)
        {
            if (equiptedItem)
            {
                Console.WriteLine("장비 중인 아이템입니다.");
                ItemManager.equiptedItem = false;
            }
        }
        //장비
        public void EquipBuy(Player player, int input)
        {
            EquipItem item = ItemManager.equipItems[input - 1];
            if (item.isPurchase)
            {
                ItemManager.purchased = true;
            }
            else
            {
                if (player.Gold < item.Price)
                {
                    ItemManager.goldRequired = true;
                }
                else
                {
                    player.Gold -= item.Price;
                    ItemManager.equipInventory.Add(item);
                    item.isPurchase = true;
                    ItemSort(ItemManager.equipInventory);
                }
            }
            ItemManager.EquipShopScene();
        }

        public void EquipSell(Player player, int input)
        {
            EquipItem item = ItemManager.equipInventory[input - 1];
            if (!item.isPurchase)
            {
                ItemManager.noItem = true;
            }
            else if(item.isEquip)
            {
                ItemManager.equiptedItem = true;
            }
            else
            {
                player.Gold += (item.Price / 2);
                ItemManager.equipInventory.Remove(item);
                item.isPurchase = false;
            }
            ItemManager.EquipSellScene();
        }

        //장착 메서드들
        public void EquipChange(Player player, int input)
        {
            EquipItem item = ItemManager.equipInventory[input - 1];
            for (int i = 0; i < ItemManager.equipInventory.Count; i++)
            {
                if (ItemManager.equipInventory[i].isEquip && item != ItemManager.equipInventory[i] && ItemManager.equipInventory[i].ItemType == item.ItemType)
                {
                    UnEquip(player, ItemManager.equipInventory[i]);
                }
            }
            Equip(player, item);

            ItemManager.EquipScene();
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

        //소비
        public void UseBuy(Player player, int input)
        {
            UseItem item = ItemManager.useItems[input - 1];
            if (player.Gold < item.Price)
            {
                ItemManager.goldRequired = true;
            }
            else
            {
                player.Gold -= item.Price;
                item.Count++;
            }
            ItemManager.UseShopScene();
        }

        public void UseSell(Player player, int input)
        {
            UseItem item = ItemManager.useItems[input - 1];
            if(item.Count <= 0)
            {
                ItemManager.noItem = true;
            }
            else
            {
                player.Gold += (item.Price / 2);
                item.Count--;
            }
            ItemManager.UseSellScene();
        }


        public void UsePotion(Player player, int input) //배틀 아이템 이용창 필요
        {
            UseItem item = ItemManager.useItems[input - 1];
            switch (item.ItemType)
            {
                case ItemType.HpPotion:
                    player.CurrentHealth += item.Value;
                    player.CurrentHealth = player.CurrentHealth >= player.MaximumHealth ? player.MaximumHealth : player.CurrentHealth;
                    item.Count--;
                    break;
                case ItemType.MpPotion:
                    player.CurrentMana += item.Value;
                    player.CurrentMana = player.CurrentMana >= player.MaximumMana ? player.MaximumMana : player.CurrentMana;
                    item.Count--;
                    break;
                case ItemType.AtkPotion:
                    player.EquipAtk += item.Value;
                    item.Count--;
                    //EndPotion(player, item);
                    break;
                case ItemType.DefPotion:
                    player.EquipDef += item.Value;
                    item.Count--;
                    //EndPotion(player, item);
                    break;
            }
        }

        public void EndPotion(Player player, UseItem item) //History 또는 Battle에서 쿨타임 설정
        {
            switch (item.ItemType)
            {
                case ItemType.AtkPotion:
                    player.EquipAtk -= item.Value;
                    break;
                case ItemType.DefPotion:
                    player.EquipDef -= item.Value;
                    break;
            }
        }


        //아이템 추가시 정렬
        public void ItemSort(List<EquipItem> itemlist)
        {
            itemlist = itemlist.OrderBy(item => item.ItemType).ThenBy(item => item.Value).ToList();
        }

    }
}
