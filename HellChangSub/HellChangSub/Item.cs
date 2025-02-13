using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public enum ItemType
    {
        Weapon,
        Armor,
        HpPotion,
        MpPotion,
        AtkPotion,
        DefPotion
    }

    public class EquipItem
    {
        public string ItemName { get; }
        public string Description { get; }

        public int Value { get; set; }
        public int Price { get; }
        public bool isPurchase { get; set; }
        public bool isEquip {  get; set; }
        public ItemType ItemType { get; }
        public int EnhanceLvl { get; set; }
        public int EnhanceValue { get; set; }

        public int TotalValue { get; set; }

        public EquipItem(string itemname, int value, string description, int price, ItemType itemtype, int enhanceLvl)
        {
            ItemName = itemname;
            Description = description;
            Price = price;
            Value = value;
            isPurchase = false;
            isEquip = false;
            ItemType = itemtype;
            EnhanceLvl = enhanceLvl;
            EnhanceValue = enhanceLvl * 5;
            TotalValue = Value + EnhanceValue;
        }  

        public string EquipInvenStatus() //소지아이템목록
        {
            string enhanceStr = EnhanceLvl > 0 ? $"+ {EnhanceLvl.ToString()}" : ""; 
            string equipStr = isEquip ? "[E]" : "";
            string str = $"{Utility.FixWidth(equipStr + ItemName + enhanceStr,20)} | {Utility.FixWidth(GetItemType(),6)} | {Utility.FixWidth((TotalValue).ToString(),5)} | {Utility.FixWidth(Description,35)}";
            return str;
        }

        public string EquipItemStatus() //아이템목록
        {
            string str = $"{Utility.FixWidth(ItemName, 20)} | {Utility.FixWidth(GetItemType(), 6)} | {Utility.FixWidth((Value).ToString(), 5)} | {Utility.FixWidth(Description, 35)}  | {Utility.FixWidth(IsPurchased(),10)}";
            return str;
        }

        public string EquipSellStatus() //아이템목록
        {
            string enhanceStr = EnhanceLvl > 0 ? $"+ {EnhanceLvl.ToString()}" : "";
            string equipStr = isEquip ? "[E]" : "";
            string str = $"{Utility.FixWidth(equipStr + ItemName + enhanceStr, 20)} | {Utility.FixWidth(GetItemType(), 6)} | {Utility.FixWidth((Value).ToString(), 5)} | {Utility.FixWidth(Description, 35)}";
            return str;
        }

        public string GetItemType()
        {
            string itemStr = "";
            switch (ItemType)
            {
                case ItemType.Weapon:
                    itemStr = $"공격력"; 
                    break;
                case ItemType.Armor:
                    itemStr = $"방어력";
                    break;
                default:
                    break;
            }
            return itemStr;
        }

        public string IsPurchased()
        {
            string purchasedStr = isPurchase ? "구매완료" : $"{Price} G";
            return purchasedStr;
        }
    }

    public class UseItem
    {
        public string ItemName { get; }
        public string Description { get; }
        public int Price { get; }
        public int Value { get; }
        public ItemType ItemType { get; }
        public int Count { get; set; }
        public int PotionDuration {  get; set; }

        public bool ItemBuff {  get; set; }

        public UseItem(string itemname, int value, string description, int price, ItemType itemType, int count)
        {
            ItemName = itemname;
            Description = description;
            Price = price;
            Value = value;
            ItemType = itemType;
            Count = count;
            PotionDuration = 4;
            ItemBuff = false;
        }
        public string UseItemStatus()  //소지아이템목록
        {
            string str = $"{Utility.FixWidth(ItemName,20)} | {Utility.FixWidth(GetItemType(),6)} | {Utility.FixWidth(Value.ToString(),5)} | {Utility.FixWidth(Description,25)} | {Utility.FixWidth(Count.ToString(),10)}";
            return str;
        }

        public string UseShopStatus() //아이템목록
        {
            string str = $"{Utility.FixWidth(ItemName, 20)} | {Utility.FixWidth(GetItemType(), 6)} | {Utility.FixWidth(Value.ToString(), 5)} | {Utility.FixWidth(Description, 25)} | {Utility.FixWidth(Price.ToString(), 10)}";
            return str;
        }

        public string GetItemType()
        {
            string itemStr = "";
            switch (ItemType)
            {
                case ItemType.HpPotion:
                    itemStr = $"HP";
                    break;
                case ItemType.MpPotion:
                    itemStr = $"MP";
                    break;
                case ItemType.AtkPotion:
                    itemStr = $"ATK";
                    break;
                case ItemType.DefPotion:
                    itemStr = $"DEF";
                    break;
                default:
                    break;
            }
            return itemStr;
        }

    }

}
