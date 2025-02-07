using System;
using System.IO;
using System.Text.Json; // C# 기본 JSON 라이브러리

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
}
