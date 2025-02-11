using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace HellChangSub
{
    public class ItemForge
    {
        ItemManager itemManager;
        Random random = new Random();

        public ItemForge(SaveData saveData)
        {
            this.itemManager = GameManager.Instance.itemManager;
            powerStones = saveData.powerStones;
        }
        public ItemForge()
        {
            this.itemManager = GameManager.Instance.itemManager;
            powerStones = new List<PowerStone>
        {
            new PowerStone("무기강화석","무기를 강화할 수 있습니다.",5,StoneType.WeaponPowerStone,5),
            new PowerStone("방어구강화석","방어구를 강화할 수 있습니다.",5,StoneType.ArmorPowerStone,5)
        };
        }


        //string name, string description, int value, StoneType stoneType,int count
        public List<PowerStone> powerStones;
        public void BlacksmithScreen()
        {
            Console.Clear();
            Console.WriteLine("대장간에 온것을 환영하네!");
            Console.WriteLine("이곳에서는 자네의 장비를 강화할 수 있다네");
            Console.WriteLine("장비를 강화해볼텐가?");

            Console.WriteLine();
            Console.WriteLine("[장비 아이템]");
            for (int i = 0; i < itemManager.equipInventory.Count; i++)
            {
                Console.WriteLine($"- {itemManager.equipInventory[i].EquipInvenStatus()}");
            }
            PrintPowerStone();
            Console.WriteLine();
            Console.WriteLine("1. 강화하기");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            int input = Utility.Select(0, 1);
            switch (input)
            {
                case 0:
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:
                    ReinforceScreen();
                    break;
            }
        }

        public void ReinforceScreen()
        {
            Console.Clear();
            Console.WriteLine("어떤 장비를 강화하고 싶나?");

            Console.WriteLine();
            Console.WriteLine("[장비 목록]");
            for (int i = 0; i < itemManager.equipInventory.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i + 1}", 3)}. {itemManager.equipInventory[i].EquipInvenStatus()}");
            }
            PrintPowerStone();
            Console.WriteLine();
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine();


            int input = Utility.Select(0, itemManager.equipInventory.Count);
            switch (input)
            {
                case 0:
                    GameManager.Instance.ShowMainScreen();
                    break;
                default:
                    EnhanceItem(itemManager.equipInventory[input - 1]);
                    break;
            }

        }
        public void EnhanceItem(EquipItem item)
        {
            if (item.ItemType == ItemType.Weapon)
            {
                Enhance(item, 0);
            }
            else
            {
                Enhance(item, 1);
            }
        }

        public void Enhance(EquipItem item, int i)
        {

            if (powerStones[i].Count <= 0)
            {
                Console.WriteLine("강화석이 부족하여 강화를 할 수 없습니다.");
                Utility.PressAnyKey();
                ReinforceScreen();
            }

            if (item.EnhanceLvl >= 10)
            {
                Console.WriteLine("최고 단계에 도달했습니다. 더 이상 강화를 할 수 없습니다.");
                Utility.PressAnyKey();
                ReinforceScreen();
            }

            int successChance = random.Next(1, 101);
            int successThreshold = 0;
            int failureThreshold = 0;


            switch (item.EnhanceLvl)  // 강화 확률 설정
            {
                case 0:
                    successThreshold = 70;
                    failureThreshold = 0;
                    break;
                case >= 1 and <= 3:
                    successThreshold = 70;
                    failureThreshold = 80;
                    break;
                case >= 4 and <= 7:
                    successThreshold = 50;
                    failureThreshold = 65;
                    break;
                case >= 8 and <= 10:
                    successThreshold = 30;
                    failureThreshold = 50;
                    break;
            }

            // 강화 성공
            if (successChance <= successThreshold)
            {
                item.Value += powerStones[i].Value;
                item.EnhanceLvl++; // 강화 단계 증가

                // 성공 메시지 배열
                string[] successMessages =
                    { "하하 다 내 덕분이라고!", "오오 빛이 나기 시작하는군!", "내가 아니면 누가 이렇게 아름답게 만들겠나!" };

                // 랜덤으로 성공 메시지 선택
                string successMessage = successMessages[random.Next(successMessages.Length)];

                Console.WriteLine($"강화에 성공했습니다! {item.ItemName}의 능력이 {powerStones[i].Value}만큼 증가하였습니다.");
                Console.WriteLine(successMessage);
            }
            // 강화 실패
            else if (successChance <= failureThreshold)
            {
                item.Value -= powerStones[i].Value;
                item.EnhanceLvl--;


                string[] failureMessages =
                    { "아이쿠 손이 미끄러 졌네.", "누구나 실수는 하는 법이지!", "평소에 장비관리를 열심히 하지 않았군!" };
                string failureMessage = failureMessages[random.Next(failureMessages.Length)];

                Console.WriteLine($"{failureMessage} {item.ItemName}의 능력이 {powerStones[i].Value}만큼 감소하였습니다.");
            }
            else
            {

                Console.WriteLine("그래도 전보다 약해지진 않았지 얼마나 다행인가");

                Console.WriteLine("강화에 실패했습니다. 다시 시도하세요.");
            }
            // 강화석을 한 개 소모
            powerStones[i].Count--;
            Utility.PressAnyKey();
            ReinforceScreen();
            
        }

        public void PrintPowerStone()
        {
            Console.WriteLine();
            Console.WriteLine("[강화석]");
            for (int i = 0; i < powerStones.Count; i++)
            {
                Console.WriteLine($"{Utility.FixWidth($"{i + 1}", 3)}. {Utility.FixWidth($"{powerStones[i].Name}", 15)} | {Utility.FixWidth($"{powerStones[i].Count}", 8)} 개");
            }
        }


    }
}
