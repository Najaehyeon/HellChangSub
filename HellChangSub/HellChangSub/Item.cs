using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public class Item
    {
        public string Name { get; }
        public string Description { get; }

        public int Value { get; }
        public int Price { get; }
        public bool isPurchase { get; set; }
        public bool isEquipt {  get; set; }

        public Item(string name, int value, string description, int price)
        {
            Name = name;
            Description = description;
            Price = price;
            Value = value;
            isPurchase = false;
            isEquipt = false;
        }
       

    }

}
