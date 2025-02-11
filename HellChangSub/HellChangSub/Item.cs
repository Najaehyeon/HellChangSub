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

        public int Value { get; }
        public int Price { get; }
        public bool isPurchase { get; set; }
        public bool isEquip {  get; set; }
        public ItemType ItemType { get; }


        public EquipItem(string itemname, int value, string description, int price, ItemType itemtype)
        {
            ItemName = itemname;
            Description = description;
            Price = price;
            Value = value;
            isPurchase = false;
            isEquip = false;
            ItemType = itemtype;
        }  

        public string EquipInvenStatus()
        {
            string equipStr = isEquip ? "[E]" : "";
            string str = $"{Utility.FixWidth(equipStr + ItemName,20)} | {Utility.FixWidth(GetItemType(),6)} | {Utility.FixWidth((Value).ToString(),5)} | {Utility.FixWidth(Description,25)}";
            return str;
        }

        public string EquipItemStatus()
        {
            string str = $"{Utility.FixWidth(ItemName, 20)} | {Utility.FixWidth(GetItemType(), 6)} | {Utility.FixWidth((Value).ToString(), 5)} | {Utility.FixWidth(Description, 25)}  | {Utility.FixWidth(IsPurchased(),10)}";
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

        public UseItem(string itemname, int value, string description, int price, ItemType itemType, int count)
        {
            ItemName = itemname;
            Description = description;
            Price = price;
            Value = value;
            ItemType = itemType;
            Count = count;
        }
        public string UseItemStatus()
        {
            string str = $"{Utility.FixWidth(ItemName,20)} | {Utility.FixWidth(GetItemType(),6)} | {Utility.FixWidth(Value.ToString(),5)} | {Utility.FixWidth(Description,25)} | {Utility.FixWidth(Count.ToString(),10)}";
            return str;
        }

        public string UseShopStatus()
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
