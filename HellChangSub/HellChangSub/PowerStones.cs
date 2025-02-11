using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    public enum StoneType
    {
        WeaponPowerStone,
        ArmorPowerStone
    }
    public class PowerStone
    {
        public string Name { get; set; }
        public string Description { get; }
        public int Value { get; set; }
        public StoneType StoneType { get; }
        public int Count { get; set; }

        public PowerStone() { }
        //string name, string description, int value, StoneType stoneType,int count

        public PowerStone(string name, string description, int value, StoneType stoneType, int count)
        {
            Name = name;
            Description = description;
            Value = value;
            StoneType = stoneType;
            Count = count;
        }
    }
}
