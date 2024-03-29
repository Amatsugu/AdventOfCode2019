﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine("Day 1: Fuel Caluclation");
			Console.WriteLine(GetFuelRequirement(Utilz.ParseIntArray("Day1/input.txt")));
			stopwatch.Stop();
			Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
		}
	}
}
