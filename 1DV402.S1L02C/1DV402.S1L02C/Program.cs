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
		// Declare a resource manager to retrieve resources in all class methods.
		static ResourceManager rm;
		const int MAX_ASTERISK = 79;

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

		static void Main(string[] args)
		{
			// Create a resource manager to retrieve resources.
			rm = new ResourceManager("_1DV402.S1L02C.Strings", Assembly.GetExecutingAssembly());
			byte waist = 0;
			do
			{
				waist = ReadOddByte(rm.GetString("OddNumberAsterisk_Prompt"));
				RenderDiamond(waist);

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

		private static byte ReadOddByte(string prompt = null, byte maxWaist = MAX_ASTERISK)
		{
			string input = "";
			byte waist = 0;
			do
			{
				try
				{
					Console.Write(prompt, maxWaist);

					input = Console.ReadLine();
					waist = byte.Parse(input);

					if ((waist > maxWaist) || ((waist % 2) != 1))
					{
						viewMessage(string.Format(drawDiamond.rm.GetString("Error_Message"), maxWaist), true);
					}
				}
				catch
				{
					viewMessage(string.Format(drawDiamond.rm.GetString("Error_Message"), maxWaist), true);
				}
			}
			while ((waist > maxWaist) || ((waist % 2) != 1));
			return waist;	
		}

		private static void RenderDiamond(byte maxWaist)
		{
			for (int i = 0; i < (maxWaist - 1) / 2; i++)	// Upper half, start with one * and finish 
				RenderRow(maxWaist, i * 2 + 1);				// with byMaxCount minus one on each side

			RenderRow(maxWaist, maxWaist);					// middle row, all *

			for (int i = (maxWaist - 1) / 2; i > 0; i--)	// lower half, start with byMaxCount minus one on each side
				RenderRow(maxWaist, i * 2 - 1);				// and finish with just one in the middle
		}

		private static void RenderRow(int maxWaist, int asteriskCount)
		{
			int iIndex = (maxWaist - asteriskCount) / 2;	// How many spaces before first *

			for (int i = 0; i < iIndex; i++)						// step spaces			
				Console.Write(" ");

			for (int i = iIndex; i < iIndex + asteriskCount; i++)	// draw calculated number of *, given as parameter
				Console.Write("*");

			Console.WriteLine();		// line feed
		}
	}
}
