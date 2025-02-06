using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellChangSub
{
    abstract class Monster
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaximumHealth {  get; set; }
        public int CurrentHealth { get; set; }
        public int Level { get; set; }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Crit { get; set; }
        Random rand = new Random();



        public Monster(int stageLvl) 
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl*5)
            {
                randomMonster = 3;
            }
            else if(randomDice < stageLvl*10)
            {
                randomMonster = 2;
            }
            else if(randomDice < stageLvl*20)
            {
                randomMonster = 1; 
            }
            else 
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Slime slime = new Slime(stageLvl);
                    break;
                case 1:
                   
                    break;
                case 2:
                    
                    break;
                case 3:
                   
                    break;

                default:
                    break;

            }
        }

    }

    class Slime : Monster
    {
        Random rand = new Random();
        public Slime(int stageLvl) : base(stageLvl)
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 3;
            }
            else if (randomDice < stageLvl * 10)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 1;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                case 1:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                case 2:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                default:
                    break;
            }
        }
    }

    class Skeleton : Monster
    {
        Random rand = new Random();
        public Skeleton(int stageLvl) : base(stageLvl)
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 3;
            }
            else if (randomDice < stageLvl * 10)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 1;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                case 1:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                case 2:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                default:
                    break;
            }
        }
    }

    class O : Monster
    {
        Random rand = new Random();
        public Slime(int stageLvl) : base(stageLvl)
        {
            int randomDice = rand.Next(0, 101);
            int randomMonster;
            if (randomDice < stageLvl * 5)
            {
                randomMonster = 3;
            }
            else if (randomDice < stageLvl * 10)
            {
                randomMonster = 2;
            }
            else if (randomDice < stageLvl * 20)
            {
                randomMonster = 1;
            }
            else
            {
                randomMonster = 0;
            }
            switch (randomMonster)
            {
                case 0:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                case 1:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                case 2:
                    Name = "";
                    Description = "";
                    MaximumHealth = 0;
                    CurrentHealth = 0;
                    Atk = 0;
                    Def = 0;
                    Crit = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
