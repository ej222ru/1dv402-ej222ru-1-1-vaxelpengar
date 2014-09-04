using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaxelpengar_1
{
    class calcChange
    {

        private static void writeErrorInput(string sInput)
        {
            Console.WriteLine("");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("FEL! '{0}' kan inte tolkas som en giltig summa pengar", sInput);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("");
        }

        private static double ReadPositiveDouble(string sText)
        {
            double dValue = 0;
            int iRoundedValue = 0;
            string s = "";

            do
            {
                iRoundedValue = 0;
                Console.Write(sText);

                try
                {
                    s = Console.ReadLine();
                    dValue = double.Parse(s);
                    iRoundedValue = (int)Math.Round(dValue);
                    if (iRoundedValue < 1)
                    {
                        writeErrorInput(s);
                    }
                }
                catch 
                {
                    writeErrorInput(s);
                    // Console.WriteLine(""); //throw;
                }
            }
            while (iRoundedValue < 1);

            return dValue;
        }

        private static uint ReadUint(string sText, uint uiMinValue)
        {
            string s = "";
            uint uiReceivedAmount = 0;
            do
            {
                try
                {
                    Console.Write(sText);

                    s = Console.ReadLine();
                    uiReceivedAmount = uint.Parse(s);

                    if (uiReceivedAmount < uiMinValue)
                    {
                        Console.WriteLine("");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("FEL! '{0}' är ett för litet belopp.", uiReceivedAmount);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("");
                    }
                }
                catch
                {
                    writeErrorInput(s);
                    // Console.WriteLine(""); //throw;
                }
            }
            while (uiReceivedAmount < uiMinValue);
            return uiReceivedAmount;
        }

        private static void SplitIntoDenominations(uint uiChange)
        {

            uint[] denomination = new uint[] {500, 100, 50, 20, 10, 5, 1};
            uint uiRest = uiChange;
            uint uiNumber = 0;
            uint uiCount = 0;
            foreach (uint element in denomination)
            {
                uiCount++;
                uiNumber = uiRest / element;
                uiRest = uiRest % element;
                if (uiNumber > 0)
                {
                    switch (uiCount)
                    {
                        case 1: 
                            Console.Write("500-lappar        : ");
                            break;
                        case 2:
                            Console.Write("100-lappar        : ");
                            break;
                        case 3:
                            Console.Write("50-lappar         : ");
                            break;
                        case 4:
                            Console.Write("20-lappar         : ");
                            break;
                        case 5:
                            Console.Write("10-kronor         : ");
                            break;
                        case 6:
                            Console.Write("5-kronor          : ");
                            break;
                        case 7:
                            Console.Write("1-kronor          : ");
                            break;


                    }
                    System.Console.WriteLine("{0}", uiNumber);
                }
            }
        }


        static void Main(string[] args)
        {
            double dTotal = 0;
            uint uiMoneyReceived = 0;
            uint uiRoundedTotal = 0;
            double dRoundedMoney;
            uint uiChange;
            bool bSkrivKvitto = false;
            ConsoleKeyInfo cki;

            do
            {
                dTotal = ReadPositiveDouble("Ange totalsumma     : ");
                uiRoundedTotal = (uint)Math.Round(dTotal);
                uiMoneyReceived = ReadUint("Ange erhållet belopp: ", uiRoundedTotal);

                dRoundedMoney = uiRoundedTotal - dTotal;
                uiChange = uiMoneyReceived - uiRoundedTotal;
                Console.WriteLine("");
                Console.WriteLine("KVITTO");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Totalt           :{0, 13:c}", dTotal);
                Console.WriteLine("Öresavrundning   :{0, 13:c}", dRoundedMoney);
                Console.WriteLine("Att betala       :{0, 13:c0}", uiRoundedTotal);
                Console.WriteLine("Kontant          :{0, 13:c0}", uiMoneyReceived);
                Console.WriteLine("Tillbaka         :{0, 13:c0}", uiChange);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("");

                SplitIntoDenominations(uiChange);
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.Green; 
                Console.WriteLine("Tryck tangent för ny beräkning - Esc avslutar.");
                Console.BackgroundColor = ConsoleColor.Black; 
                cki = Console.ReadKey(true);
                Console.WriteLine("");
                // or should the screen be cleared??? 
                // Console.Clear();
            } while (cki.Key != ConsoleKey.Escape);
         }
    }
}
