using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day1
{
	public static class FuelCaluclation
	{
		public static int GetFuelRequirement(int[] input)
		{
			var curFuel = input.Sum(i => GetFuelCost(i));
			return curFuel;
		}

		public static int GetFuelCost(int mass)
		{
			var curCost = ((mass / 3) - 2);
			if (curCost <= 0)
				return 0;
			return curCost + GetFuelCost(curCost);
		}

		public static void Execute()
		{
			Console.WriteLine("Day 1:");
			Console.WriteLine(FuelCaluclation.GetFuelRequirement(Utilz.ParseIntArray("Day1/input.txt")));
			//Console.WriteLine(GetFuelCost(100756));
		}
	}
}
