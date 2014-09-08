using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace _1DV402.S1L02C
{
	class drawDiamond	
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

		static void Main(string[] args)
		{
			rm = new ResourceManager("_1DV402.S1L02C.Strings", Assembly.GetExecutingAssembly());
			byte byWaist = 0;
			do
			{

				byWaist = ReadOddByte(rm.GetString("OddNumberAsterisk_Prompt"), 79);
				RenderDiamond(byWaist);


			} while (IsContinuing());
		}

		private static bool IsContinuing()
		{ 
				Console.WriteLine("");
				Console.BackgroundColor = ConsoleColor.Green;
				Console.WriteLine(rm.GetString("Continue_Prompt"));
				Console.BackgroundColor = ConsoleColor.Black; 
				ConsoleKeyInfo cki = Console.ReadKey(true);
				Console.WriteLine("");
				// or should the screen be cleared???   Console.Clear();
				return (cki.Key != ConsoleKey.Escape);

		}

		private static byte ReadOddByte(string sPrompt = null, byte byMaxValue = 255)
		{

			string sInput = "";
			byte byWaist = 0;
			do
			{
				try
				{
					Console.Write(sPrompt, byMaxValue);

					sInput = Console.ReadLine();
					byWaist = byte.Parse(sInput);

					if ((byWaist > byMaxValue) || ((byWaist % 2) != 1))
					{
						viewMessage(string.Format(drawDiamond.rm.GetString("Error_Message"), byMaxValue), true);
					}
				}
				catch
				{
					viewMessage(string.Format(drawDiamond.rm.GetString("Error_Message"), byMaxValue), true);
				}
			}
			while ((byWaist > byMaxValue) || ((byWaist % 2) != 1));
			return byWaist;	

		}

		private static void RenderDiamond(byte byMaxCount)
		{
			for (int i = 0; i < (byMaxCount - 1) / 2; i++)	// Upper half, start with one * and finish 
				RenderRow(byMaxCount, i * 2 + 1);				// with byMaxCount minus one on each side

			RenderRow(byMaxCount, byMaxCount);					// middle row, all *

			for (int i = (byMaxCount - 1) / 2; i > 0; i--)	// lower half, start with byMaxCount minus one on each side
				RenderRow(byMaxCount, i * 2 - 1);				// and finish with just one in the middle
		}



		private static void RenderRow(int iMaxCount, int iAsteriskCount)
		{
			int iIndex = (iMaxCount - iAsteriskCount) / 2;	// How many spaces before first *

			for (int i = 0; i < iIndex; i++)						// step spaces			
				Console.Write(" ");

			for (int i = iIndex; i < iIndex + iAsteriskCount; i++)	// draw calculated number of *, given as parameter
				Console.Write("*");

			Console.WriteLine();		// line feed
		}



	}
}
