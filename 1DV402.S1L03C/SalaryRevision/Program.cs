﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace SalaryRevision
{
	class Program
	{
		static ResourceManager rm;

		static void Main(string[] args)
		{
			rm = new ResourceManager("SalaryRevision.Strings", Assembly.GetExecutingAssembly());
			int iNoOfSalaries = 0;
			int[] iaSalaries;
			do
			{
				iNoOfSalaries = ReadInt(rm.GetString("NoOfSalaries_Prompt"));
				iaSalaries = ReadSalaries(iNoOfSalaries);
				ViewResult(iaSalaries);

			} while (IsContinuing());

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
			int iRet = 0;
			do
			{
				try
				{
					Console.Write(prompt);
					iRet = int.Parse(Console.ReadLine());

					if (iRet < 3)
					{
						viewMessage(rm.GetString("Error2_Message"));
					}
				}
				catch
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), iRet));
				}

			} while (iRet < 3);
			return iRet;
		}

		private static int[] ReadSalaries(int count)
		{
			int[] iaSalaries = 0;


			return iaSalaries;
		}
		private static void viewMessage(ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
		{
		}
		private static void ViewResult(int[] salaries)
		{
		}
		private static int ReadInt(string prompt)
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
