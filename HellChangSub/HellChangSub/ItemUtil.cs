﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class ItemUtil
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

        public void EquippedItem(bool equippedItem)
        {
            if (equippedItem)
            {
                Console.WriteLine("장비 중인 아이템입니다.");
                ItemManager.equippedItem = false;
            }
        }
        //장비

        public void QuestEquip(EquipItem item)
        {
            if (item.isPurchase)
            {
                Console.WriteLine("이미 구매한 아이템입니다");
                GameManager.Instance.player.Gold += item.Price;
            }
            else
            {
                ItemManager.equipInventory.Add(item);
                item.isPurchase = true;
                ItemSort(ItemManager.equipInventory);
            }
        }

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
                ItemManager.equippedItem = true;
            }
            else
            {
                player.Gold += (item.Price / 2);
                ItemManager.equipInventory.Remove(item);
                item.isPurchase = false;
                item.EnhanceLvl = 0;
                item.EnhanceValue = 0;
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
            GameManager.Instance.quest.questDataList[1].Progressed++;
            GameManager.Instance.quest.questDataList[1].JudgeState();
            if (!item.isEquip)
            {
                item.isEquip = true;
                if (item.ItemType == ItemType.Weapon)
                {
                    player.EquipAtk += item.TotalValue;
                }
                else
                {
                    player.EquipDef += item.TotalValue;
                }
            }
            else
                UnEquip(player, item);
        }

        public void UnEquip(Player player, EquipItem item)
        {
            GameManager.Instance.quest.questDataList[1].Progressed--;
            GameManager.Instance.quest.questDataList[1].JudgeState();
            if (item.isEquip)
            {
                item.isEquip = false;
                if (item.ItemType == ItemType.Weapon)
                {
                    player.EquipAtk -= item.TotalValue;
                }
                else
                {
                    player.EquipDef -= item.TotalValue;
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
                    if(item.ItemBuff)
                    {
                        Console.WriteLine("이미 사용중입니다");
                        Utility.PressAnyKey();
                        return;
                    }
                    else
                    {
                        player.EquipAtk += item.Value; //포션 공격력으로 바꿔야함
                        item.Count--;
                        item.PotionDuration = 4;
                        item.ItemBuff = true;
                    }
                    break;
                case ItemType.DefPotion:
                    if (item.ItemBuff)
                    {
                        Console.WriteLine("이미 사용중입니다");
                        Utility.PressAnyKey();
                        return;
                    }
                    else
                    {
                        player.EquipDef += item.Value; //포션 방어력으로 바꿔야함
                        item.Count--;
                        item.PotionDuration = 4; //쿨타임 +1
                        item.ItemBuff = true;
                    }
                    break;
            }
        }

        public void CoolDownCheck() // (PotionDuration -1)이 지속시간 
        {
            for (int i = 2; i < ItemManager.useItems.Count; i++)
            {
                UseItem item = ItemManager.useItems[i];
                if (item.ItemBuff)
                {
                    item.PotionDuration--;

                    if (item.PotionDuration == 0)
                    {
                        EndPotion(GameManager.Instance.player, item);
                    }
                }
            }
        }
        public void EndPotion(Player player, UseItem item)
        {
            switch (item.ItemType)
            {
                case ItemType.AtkPotion:
                    player.EquipAtk -= item.Value;
                    item.ItemBuff = false;
                    item.PotionDuration = 4; //쿨타임 +1
                    break;
                case ItemType.DefPotion:
                    player.EquipDef -= item.Value;
                    item.ItemBuff = false;
                    item.PotionDuration = 4; //쿨타임 +1
                    break;
            }
        }


        //아이템 추가시 정렬
        public void ItemSort(List<EquipItem> itemlist)
        {
            List < EquipItem > sortedlist = itemlist.OrderBy(item => item.ItemType).ThenBy(item => item.Value).ToList();
            itemlist.Clear();
            itemlist.AddRange(sortedlist);
        }

    }
}
