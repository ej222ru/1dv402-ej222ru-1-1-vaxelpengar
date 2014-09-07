using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace CalcChange
{
	class calcChange
	{
		static ResourceManager rm;



		private static void viewMessage(string sMessage, bool bIsError = false)
		{
			Console.WriteLine("");
			if (bIsError)
				Console.BackgroundColor = ConsoleColor.Red;
			else
				Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine(sMessage);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("");
		}

		private static void viewReceipt(double dSubtotal, double dRoundingOfAmount, uint uiTotal, uint uiCash, uint uiChange, uint[] uiNotes, uint[] uiDenominations)
		{
			Console.WriteLine("");
			Console.WriteLine("KVITTO");
			Console.WriteLine("-------------------------------");
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c}",  "Totalt", dSubtotal));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c}",  "Öresavrundning", dRoundingOfAmount));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c0}", "Att betala", uiTotal));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c0}", "Kontant", uiCash));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c0}", "Tillbaka", uiChange));
			Console.WriteLine("-------------------------------"); 
			Console.WriteLine("");

			string sDenomText = "";
			uint uiCount = 0;
			foreach (uint element in uiDenominations)
			{
				if (uiNotes[uiCount] > 0)
				{
					if (element > 10)
						sDenomText = "-lappar       :";
					else
						sDenomText = "-kronor       :";

					Console.Write("{0,3}", element);
					Console.Write(sDenomText);

					System.Console.WriteLine(" {0}", uiNotes[uiCount]);
				}
				uiCount++;
			}
		}
		 

		private static double ReadPositiveDouble(string sText)
		{
			double dValue = 0;
			int iRoundedValue = 0;
			string sInput = "";

			do
			{
				iRoundedValue = 0;
				Console.Write(sText);

				try
				{
					sInput = Console.ReadLine();
					dValue = double.Parse(sInput);
					iRoundedValue = (int)Math.Round(dValue);
					if (iRoundedValue < 1)
					{
						viewMessage(string.Format(calcChange.rm.GetString("ErrorInvalidSum"), sInput), true);
					}
				}
				catch 
				{
					viewMessage(string.Format(calcChange.rm.GetString("ErrorInvalidSum"), sInput), true);
				}
			}
			while (iRoundedValue < 1);

			return dValue;
		}

		private static uint ReadUint(string sText, uint uiMinValue)
		{
			string sInput = "";
			uint uiReceivedAmount = 0;
			do
			{
				try
				{
					Console.Write(sText);

					sInput = Console.ReadLine();
					uiReceivedAmount = uint.Parse(sInput);

					if (uiReceivedAmount < uiMinValue)
					{
						viewMessage(string.Format(calcChange.rm.GetString("ErrorSmallAmount"), sInput), true);
					}
				}
				catch
				{
					viewMessage(string.Format(calcChange.rm.GetString("ErrorInvalidSum"), sInput), true);
				}
			}
			while (uiReceivedAmount < uiMinValue);
			return uiReceivedAmount;
		}

		private static uint[] SplitIntoDenominations(uint uiChange, uint[] uiDenomination)
		{
			uint[] uiDenominationCount = new uint[] { 0, 0, 0, 0, 0, 0, 0 };
			uint uiRest = uiChange;
			uint uiNumber = 0;
			uint uiCount = 0;
			foreach (uint element in uiDenomination)
			{
				uiNumber = uiRest / element;
				uiRest = uiRest % element;

				if (uiNumber > 0)
				{
					uiDenominationCount[uiCount] = uiNumber;
				}
				uiCount++;
			}
			return uiDenominationCount;
		}

		static void Main(string[] args)
		{
			double dTotal = 0;
			uint uiMoneyReceived = 0;
			uint uiRoundedTotal = 0;
			double dRoundedMoney;
			uint uiChange;
			ConsoleKeyInfo cki;
			uint[] uiDenomination = new uint[] { 500, 100, 50, 20, 10, 5, 1 };

			rm = new ResourceManager("CalcChange.Strings", Assembly.GetExecutingAssembly());
			do
			{
				// Create a resource manager to retrieve resources.

				dTotal = ReadPositiveDouble(rm.GetString("TotalCost_Prompt"));
				uiRoundedTotal = (uint)Math.Round(dTotal);
				uiMoneyReceived = ReadUint(rm.GetString("Cash_Prompt"), uiRoundedTotal);

				dRoundedMoney = uiRoundedTotal - dTotal;
				uiChange = uiMoneyReceived - uiRoundedTotal;

				viewReceipt(dTotal, dRoundedMoney, uiRoundedTotal, uiMoneyReceived, uiChange, SplitIntoDenominations(uiChange, uiDenomination), uiDenomination);

				Console.WriteLine("");
				Console.BackgroundColor = ConsoleColor.Green;
				Console.WriteLine(rm.GetString("Continue_Prompt"));
				Console.BackgroundColor = ConsoleColor.Black; 
				cki = Console.ReadKey(true);
				Console.WriteLine("");
				// or should the screen be cleared???   Console.Clear();
			} while (cki.Key != ConsoleKey.Escape);
		}
    }
}
