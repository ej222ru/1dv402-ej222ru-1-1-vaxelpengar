using System;
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

			int noOfSalaries = 0;
			int[] salaries;
			do
			{
				noOfSalaries = ReadInt(rm.GetString("NoOfSalaries_Prompt"));
				salaries = ReadSalaries(noOfSalaries);
				ViewResult(salaries);

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
			int ret = 0;
			do
			{
				try
				{
					Console.Write(prompt);
					ret = int.Parse(Console.ReadLine());

					if (ret < 3)
					{
						viewMessage(rm.GetString("Error2_Message"), ConsoleColor.Red);
					}
				}
				catch
				{
					viewMessage(string.Format(rm.GetString("Error_Message"), ret), ConsoleColor.Red);
				}

			} while (ret < 3);
			return ret;
		}

		private static int[] ReadSalaries(int count)
		{
			int[] iaSalaries = new int[count];
			for (int i=0; i<count; i++)
			{
				Console.Write(string.Format(rm.GetString("Salary_Prompt"), i));
				iaSalaries[i] = int.Parse(Console.ReadLine());
			}

			return iaSalaries;
		}

		private static void viewMessage(string message, ConsoleColor backgroundColor = ConsoleColor.Blue, ConsoleColor foregroundColor = ConsoleColor.White)
		{
			Console.WriteLine("");
			Console.BackgroundColor = backgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine("");
		}

		private static void ViewResult(int[] salaries)
		{

			int median = salaries.Median();

			Double dAverage = salaries.Average();
	
			
//			int iDispersion = salaries.Dispersion();

		}


	}


	static class MyExtensions
	{

		public static int Dispersion(this int[] source)
		{
			int ret = 0;
			int maxValue = source.Max();
			int minValue = source.Min();
			return ret = maxValue - minValue;
		}

		public static int Median(this int[] source)
		{
			int median = 0;
			int[] sortedSurce = source;
			Array.Sort(sortedSurce);
			int noOfElements = sortedSurce.Length;
			if ((noOfElements % 2) == 0)
			{
				// even numbers, caluculate median as an average of the two
			}
			else
				median = sortedSurce[noOfElements/2];   // index of array starts at 0
			return median;
		}
		
	}

}
