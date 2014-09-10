using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace _1DV402.S1L02C
{
	class DrawDiamond	
	{
		// Declare a resource manager to retrieve resources in all class methods.
		static ResourceManager rm;
		const int MAX_ASTERISK = 79;
	

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
		/// <summary>
		/// Prints a message and expects a key respons from the user.
		/// </summary>
		/// <returns>Returns true if user input is anything but ESC</returns>
		private static bool IsContinuing()
		{ 
			Console.WriteLine("");
			Console.BackgroundColor = ConsoleColor.Green;
			Console.WriteLine(rm.GetString("Continue_Prompt"));
			Console.BackgroundColor = ConsoleColor.Black; 
			ConsoleKeyInfo cki = Console.ReadKey(true);
			Console.WriteLine("");
			Console.Clear();
			return (cki.Key != ConsoleKey.Escape);
		}
		/// <summary>
		/// Read a number from user/screen which must be odd.
		/// </summary>
		/// <param name="prompt"></param>
		/// <param name="maxWaist"></param>
		/// <returns>The odd number entered by the user.</returns>
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
						ViewMessage(string.Format(DrawDiamond.rm.GetString("Error_Message"), maxWaist), true);
					}
				}
				catch
				{
					ViewMessage(string.Format(DrawDiamond.rm.GetString("Error_Message"), maxWaist), true);
				}
			}
			while ((waist > maxWaist) || ((waist % 2) != 1));
			return waist;	
		}
		/// <summary>
		/// Calculates and draws a diamond shaped figure on screen.
		/// </summary>
		/// <param name="maxWaist"></param>
		private static void RenderDiamond(byte maxWaist)
		{
			for (int i = 0; i < (maxWaist - 1) / 2; i++)	// Upper half, start with one * and finish 
				RenderRow(maxWaist, i * 2 + 1);				// with byMaxCount minus one on each side

			RenderRow(maxWaist, maxWaist);					// middle row, all *

			for (int i = (maxWaist - 1) / 2; i > 0; i--)	// lower half, start with byMaxCount minus one on each side
				RenderRow(maxWaist, i * 2 - 1);				// and finish with just one in the middle
		}
		/// <summary>
		/// Calculate and draw row of asterisks defined by the two parameters.
		/// asteriskCount determines how many asterisks that should be drawn 
		/// centered in relation to parameter maxWaist i.e. if maxWaist=7 and astriskCount=3 
		/// the row starts with two spaces, followed by three asterisks and ends with two more spaces.
		/// </summary>
		/// <param name="maxWaist"></param>
		/// <param name="asteriskCount"></param>
		private static void RenderRow(int maxWaist, int asteriskCount)
		{
			int index = (maxWaist - asteriskCount) / 2;	// How many spaces before first *

			for (int i = 0; i < index; i++)						// step spaces			
				Console.Write(" ");

			for (int i = index; i < index + asteriskCount; i++)	// draw calculated number of *, given as parameter
				Console.Write("*");

			Console.WriteLine();		// line feed
		}
		/// <summary>
		/// Prints a message on the screen. 
		/// Backgorund color is red if determined to be an error (second parameter), otherwise gree background color.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="isError"></param>
		private static void ViewMessage(string message, bool isError = false)
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

	}
}
