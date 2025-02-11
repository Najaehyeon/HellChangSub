using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace HellChangSub
{
    public enum StoneType // 강화석 종류를 나타내는 열거형
    {
        WeaponPowerStone,  // 무기강화석
        ArmorPowerStone    // 방어구
    }
    public class ItPowerUp
    {
        private static readonly Random rand = new Random();

        public class Item
        {
            public int Value { get; internal set; }
            public string Name { get; internal set; }
            public int EnhanceLevel { get; set; }  // 강화 단계 추가
        }

        public class PowerStone
        {
            public string Name { get; }
            public string Description { get; }
            public int Value { get; set; }
            public StoneType StoneType { get; }

            public PowerStone(string name, string description, int value, StoneType stoneType)
            {
                Name = name;
                Description = description;
                Value = value;
                StoneType = stoneType;
            }
        }

        public void EnhanceItem(Item item, PowerStone stone, int currentEnhanceLevel, ref int powerStoneCount)
        {
            if (powerStoneCount <= 0)
            {
                Console.WriteLine("강화석이 부족하여 강화를 할 수 없습니다.");
                return;  // 강화석이 없으면 강화 불가
            }

            int successChance = rand.Next(1, 101);
            int successThreshold = 0;
            int failureThreshold = 0;

           
            switch (currentEnhanceLevel)  // 강화 확률 설정
            {
                case >= 1 and <= 3:
                    successThreshold = 70;
                    failureThreshold = 100;
                    break;
                case >= 4 and <= 7:
                    successThreshold = 50;
                    failureThreshold = 60;
                    break;
                case >= 8 and <= 10:
                    successThreshold = 30;
                    failureThreshold = 60;
                    break;
            }

            // 강화 성공
            if (successChance <= successThreshold)
            {
                item.Value += stone.Value;
                item.EnhanceLevel++; // 강화 단계 증가

                // 성공 메시지 배열
                string[] successMessages = 
                    { "하하 다 내 덕분이라고!", "오오 빛이 나기 시작하는군!", "내가 아니면 누가 이렇게 아름답게 만들겠나!" };

                // 랜덤으로 성공 메시지 선택
                string successMessage = successMessages[rand.Next(successMessages.Length)];

                Console.WriteLine($"강화에 성공했습니다! {item.Name}의 능력이 {stone.Value}만큼 증가하였습니다.");
                Console.WriteLine(successMessage);
            }
            // 강화 실패
            else if (successChance <= failureThreshold)
            {
                item.Value -= stone.Value;
                item.EnhanceLevel--;
               

                string[] failureMessages =
                    { "아이쿠 손이 미끄러 졌네.", "누구나 실수는 하는 법이지!", "평소에 장비관리를 열심히 하지 않았군!" };
                string failureMessage = failureMessages[rand.Next(failureMessages.Length)];

                Console.WriteLine($"{failureMessage} {item.Name}의 능력이 {stone.Value}만큼 감소하였습니다.");
            }
            else
            {
                
                Console.WriteLine("그래도 전보다 약해지진 않았지 얼마나 다행인가");

                Console.WriteLine("강화에 실패했습니다. 다시 시도하세요.");
            }

            if (currentEnhanceLevel >= 10)
            {
                Console.WriteLine("최고 단계에 도달했습니다. 더 이상 강화를 할 수 없습니다.");
            }

            // 강화석을 한 개 소모
            powerStoneCount--;
        }

        public static void BlacksmithScreen(ref int weaponStoneCount, ref int armorStoneCount)
        {
            Console.Clear();
            Console.WriteLine("대장간에 온것을 환영하네!");
            Console.WriteLine("이곳에서는 자네의 장비를 강화할 수 있다네");
            Console.WriteLine("장비를 강화해볼텐가?");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 강화하기");

            int input = Utility.Select(0, 1);
            switch (input)
            {
                case 0:
                    Console.Clear();
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:
                    Console.Clear();
                    ReinforceScreen(ref weaponStoneCount, ref armorStoneCount);
                    break;
            }
        }

        public static void ReinforceScreen(ref int weaponStoneCount, ref int armorStoneCount)
        {
            Console.Clear();
            Console.WriteLine("어떤 강화석을 사용하고 싶나?");
            Console.WriteLine("0. 돌아가기");
            Console.WriteLine("1. 무기 강화석");
            Console.WriteLine("2. 방어구 강화석");

            int input = Utility.Select(0, 2);
            PowerStone stone = null;

            switch (input)
            {
                case 0:
                    Console.Clear();
                    GameManager.Instance.ShowMainScreen();
                    break;
                case 1:
                    if (weaponStoneCount <= 0)
                    {
                        Console.WriteLine("무기 강화석이 부족합니다.");
                        return;
                    }

                    stone = new PowerStone("무기 강화석", "무기를 강화하는 강화석", 5, StoneType.WeaponPowerStone);
                    // 인벤토리에서 무기 종류 나열
                    // 강화할 무기를 선택하고 강화
                    break;
                case 2:
                    if (armorStoneCount <= 0)
                    {
                        Console.WriteLine("방어구 강화석이 부족합니다.");
                        return;
                    }

                    stone = new PowerStone("방어구 강화석", "방어구를 강화하는 강화석", 5, StoneType.ArmorPowerStone);
                    // 인벤토리에서 방어구 종류 나열
                    // 강화할 방어구를 선택하고 강화
                    break;
            }
        }
    }
    // 무기가 강화에 성공한다면 무기 옆에 강화 단계에 따른 번호를 무기이름 뒤에 +몇강 으로 표시한다
    //무기 정보에 공격력에 강화 단계에 따른 +5 * 강화단계를 부여
    //방어구가 강화에 성공한다면 방어구 옆에 강화 단계에 따른 번호를 무기이름 뒤에 +몇강 으로 표시한다
    //방어구 정보에 방어력에 강화 단계에 따른 +5 * 강화단계를 부여

}


