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


        public EquipItem(string name, int value, string description, int price, ItemType itemtype)
        {
            ItemName = name;
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
            string str = string.Format("{0,-15}  | {1,-4}  | {2,-4}  | {3,-15}",equipStr + ItemName, GetItemType(), Value, Description);
            return str;
        }

        public string EquipItemStatus()
        {
            string str = string.Format("{0,-15}  | {1,-4}  | {2,-4}  | {3,-15}  | {4}",ItemName, GetItemType(), Value, Description, IsPurchased());
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

        public UseItem(string itemname, int value, string description, int price, ItemType itemType)
        {
            ItemName = itemname;
            Description = description;
            Price = price;
            Value = value;
            ItemType = itemType;
            Count = 0;
        }
        public string UseItemStatus()
        {
            string str = string.Format("{0,-15}  | {1,-4}  | {2,-4}  | {3,-15}  | {4}개 보유", ItemName, GetItemType(), Value, Description, Count);
            return str;
        }

        public string UseShopStatus()
        {
            string str = string.Format("{0,-15}  | {1,-4}  | {2,-4}  | {3,-15}  | {4} G", ItemName, GetItemType(), Value, Description, Price);
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
