using System;
using System.Globalization;

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
		// Declare a resource manager to retrieve resources in all class methods.
		static ResourceManager rm;
		/// <summary>
		/// kalle anka
		/// </summary>
		/// <param name="message"></param>
		/// <param name="isError"></param>
		private static void viewMessage(string message, bool isError = false)
		{
			Console.WriteLine("");
			if (isError)
				Console.BackgroundColor = ConsoleColor.Red;
			else
				Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("");
		}

		private static void viewReceipt(double subtotal, double roundingOfAmount, uint total, uint cash, uint change, uint[] noOfDenomination, uint[] denominations)
		{
			Console.WriteLine("");
			Console.WriteLine(calcChange.rm.GetString("Receipt_text"));
			Console.WriteLine(calcChange.rm.GetString("Divider_string"));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c}", calcChange.rm.GetString("Total_text"), subtotal));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c}", calcChange.rm.GetString("Rounding_text"), roundingOfAmount));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c0}", calcChange.rm.GetString("Payable_text"), total));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c0}", calcChange.rm.GetString("Cash_text"), cash));
			Console.WriteLine(String.Format("{0,-17}:{1, 13:c0}", calcChange.rm.GetString("Change_text"), change));
			Console.WriteLine(calcChange.rm.GetString("Divider_string")); 
			Console.WriteLine("");

			uint index = 0;
			foreach (uint element in denominations)
			{
				if (noOfDenomination[index] > 0)
				{
					if (element > 10)
						Console.WriteLine(String.Format("{0,3}{1, -14}: {2}", element, calcChange.rm.GetString("Notes_text"), noOfDenomination[index]));
				else
						Console.WriteLine(String.Format("{0,3}{1, -14}: {2}", element, calcChange.rm.GetString("Coin_text"), noOfDenomination[index]));
				}
				index++;
			}
		}
		 
		private double ReadPositiveDouble(string prompt)
		{
			Exception MyException = new Exception();
			double value = 0;
			int roundedValue = 0;
			string input = "";

			do
			{
				roundedValue = 0;
				Console.Write(prompt);

				try
				{
					input = Console.ReadLine();
					value = double.Parse(input);
					roundedValue = (int)Math.Round(value);
					if (roundedValue < 1)
					{
						throw (MyException);
						//viewMessage(string.Format(calcChange.rm.GetString("ErrorInvalidSum"), input), true);
					}
				}						
				catch 
				{
					viewMessage(string.Format(calcChange.rm.GetString("ErrorInvalidSum"), input), true);
				}
			}
			while (roundedValue < 1);

			return value;
		}
		 
		private static uint ReadUint(string text, uint minValue)
		{
			string input = "";
			uint receivedAmount = 0;
			do
			{
				try
				{
					Console.Write(text);

					input = Console.ReadLine();
					receivedAmount = uint.Parse(input);

					if (receivedAmount < minValue)
					{
						viewMessage(string.Format(calcChange.rm.GetString("ErrorSmallAmount"), input), true);
					}
				}
				catch
				{
					viewMessage(string.Format(calcChange.rm.GetString("ErrorInvalidSum"), input), true);
				}
			}
			while (receivedAmount < minValue);
			return receivedAmount;
		}

		private static uint[] SplitIntoDenominations(uint change, uint[] denomination)
		{
			uint[] denominationCount = new uint[] { 0, 0, 0, 0, 0, 0, 0 };
			uint rest = change;
			uint numberOfDenomination = 0;
			uint index = 0;
			foreach (uint element in denomination)
			{
				numberOfDenomination = rest / element;
				rest = rest % element;

				if (numberOfDenomination > 0)
				{
					denominationCount[index] = numberOfDenomination;
				}
				index++;
			}
			return denominationCount;
		}

		static void Main(string[] args)
		{
			double total = 0;
			uint moneyReceived = 0;
			uint roundedTotal = 0;
			double roundedMoney;
			uint change;
			ConsoleKeyInfo cki;
			uint[] denomination = new uint[] { 500, 100, 50, 20, 10, 5, 1 };

			// Create a resource manager to retrieve resources.
			rm = new ResourceManager("CalcChange.Strings", Assembly.GetExecutingAssembly());

			do
			{
				total = ReadPositiveDouble(rm.GetString("TotalCost_Prompt"));
				roundedTotal = (uint)Math.Round(total);
				moneyReceived = ReadUint(rm.GetString("Cash_Prompt"), roundedTotal);

				roundedMoney = roundedTotal - total;
				change = moneyReceived - roundedTotal;

				viewReceipt(total, roundedMoney, roundedTotal, moneyReceived, change, SplitIntoDenominations(change, denomination), denomination);

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
