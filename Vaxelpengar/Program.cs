using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaxelpengar_1
{
    class Program
    {

        static void Main(string[] args)
        {
            double dTotal = 0;
            uint uiMoneyReceived = 0;
            uint uiRoundedTotal = 0;
            double dRoundingOffAmount;
            uint uiChange;
            bool bSkrivKvitto = false;

            try
            {
                Console.Write("Ange totalsumma     : ");
                dTotal = double.Parse(Console.ReadLine());
                uiRoundedTotal = (uint)Math.Round(dTotal);
                if (uiRoundedTotal < 1)
                {
                    throw new Exception();
                }
                do
                {
                    try
                    {

                        Console.Write("Ange erhållet belopp: ");
                        uiMoneyReceived = uint.Parse(Console.ReadLine());
                        if (uiMoneyReceived < uiRoundedTotal)
                        {
                            Console.WriteLine("");
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.WriteLine("Totalsumman är för liten. Köpet kunde inte genomföras.");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("");
                        }
                        else
                            bSkrivKvitto = true;
                    }
                    catch
                    {
                        Console.WriteLine("");
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("FEL! Erhållet belopp felaktig.");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("");
                    }
                }
                while (!bSkrivKvitto);
            }
            catch
            {
                Console.WriteLine("");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("Totalsumman är för liten. Köpet kunde inte genomföras.");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("");
            }

            if (bSkrivKvitto)
            {
                dRoundingOffAmount = uiRoundedTotal - dTotal;
                uiChange = uiMoneyReceived - uiRoundedTotal;
                Console.WriteLine("");
                Console.WriteLine("KVITTO");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Totalt           :{0, 13:c}", dTotal);
                Console.WriteLine("Öresavrundning   :{0, 13:c}", dRoundingOffAmount);
                Console.WriteLine("Att betala       :{0, 13:c0}", uiRoundedTotal);
                Console.WriteLine("Kontant          :{0, 13:c0}", uiMoneyReceived);
                Console.WriteLine("Tillbaka         :{0, 13:c0}", uiChange);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("");


                uint uiNotes500 = uiChange / 500;
                uint uiRest = uiChange % 500;
                uint uiNotes100 = uiRest / 100;
                uiRest = uiRest % 100;
                uint uiNotes50 = uiRest / 50;
                uiRest = uiRest % 50;
                uint uiNotes20 = uiRest / 20;
                uiRest = uiRest % 20;
                uint uiCoin10 = uiRest / 10;
                uiRest = uiRest % 10;
                uint uiCoin5 = uiRest / 5;
                uiRest = uiRest % 5;
                uint uiCoin1 = uiRest / 1;

                if (uiNotes500 > 0)
                    Console.WriteLine(" 500-lappar      : {0}", uiNotes500);
                if (uiNotes100 > 0)
                    Console.WriteLine(" 100-lappar      : {0}", uiNotes100);
                if (uiNotes50 > 0)
                    Console.WriteLine("  50-lappar      : {0}", uiNotes50);
                if (uiNotes20 > 0)
                    Console.WriteLine("  20-lappar      : {0}", uiNotes20);
                if (uiCoin10 > 0)
                    Console.WriteLine("  10-kronor      : {0}", uiCoin10);
                if (uiCoin5 > 0)
                    Console.WriteLine("   5-kronor      : {0}", uiCoin5);
                if (uiCoin1 > 0)
                    Console.WriteLine("   1-kronor      : {0}", uiCoin1);
            }

            string s = Console.ReadLine();
        }
    }
}
