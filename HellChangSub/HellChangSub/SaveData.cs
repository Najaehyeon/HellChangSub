using System;
using System.IO;
using System.Text.Json;
using HellChangSub; // C# 기본 JSON 라이브러리

[Serializable]
public class SaveData
{
    public string Name { get; set; }
    public int JobCode { get; set; }  
    public string JobName { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    public int CurrentHealth { get; set; }
    public int MaximumHealth { get; set; }
    public int CurrentMana { get; set; }
    public int MaximumMana { get; set; }
    public float Atk { get; set; }
    public float EquipAtk { get; set; }
    public int Def { get; set; }
    public int EquipDef { get; set; }
    public int Gold { get; set; }
    public float Crit { get; set; }
    public float CritDamage { get; set; }
    public float Evasion { get; set; }

    public SaveData(Player player)
    {
        Name = player.Name;
        JobCode = player.JobCode;
        JobName = player.JobName;
        Level = player.Level;
        Exp = player.Exp;
        CurrentHealth = player.CurrentHealth;
        MaximumHealth = player.MaximumHealth;
        CurrentMana = player.CurrentMana;
        MaximumMana = player.MaximumMana;
        CurrentMana = player.CurrentMana;
        Atk = player.Atk;
        EquipAtk = player.EquipAtk;
        Def = player.Def;
        EquipDef = player.EquipDef;
        Gold = player.Gold;
        Crit = player.Crit;
        CritDamage = player.CritDamage;
        Evasion = player.Evasion;
    }
}
