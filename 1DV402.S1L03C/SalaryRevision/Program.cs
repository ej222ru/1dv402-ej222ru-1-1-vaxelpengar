using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryRevision
{
	class Program
	{
		static void Main(string[] args)
		{






		}


		private static bool IsContinuing()
		{
			Console.WriteLine("");
			Console.BackgroundColor = ConsoleColor.Blue;
			Console.WriteLine(rm.GetString("Continue_Prompt"));
			Console.BackgroundColor = ConsoleColor.Black;
			ConsoleKeyInfo cki = Console.ReadKey(true);
			Console.WriteLine("");
			// or should the screen be cleared???   Console.Clear();
			return (cki.Key != ConsoleKey.Escape);

		}

		private static int ReadInt(string prompt)
		{
		}
		private static int[] ReadSalaries(int count)
		{
		}
		private static void viewMessage(ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
		{
		}
		private static void ViewResult(string prompt)
		{
		}
		private static int ReadInt(int[] salaries)
		{
		}

	}

	class MyExtensions
	{

		public static int Dispersion(this int[] source)
		{
		}

		public static int Median(this int[] source)
		{
		}
		
	}
}
