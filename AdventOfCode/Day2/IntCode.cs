using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day2
{
	public static class IntCode
	{

		public static int ExecuteCode(int[] code, int noun, int verb)
		{
			int[] memory = code;
			memory[1] = noun;
			memory[2] = verb;
			var curAddr = 0;

			while(true)
			{
				var opCode = memory[curAddr];

				if (opCode == 99) //Halt
					return memory[0];

				//Working Adresses
				int a = memory[curAddr + 1], b = memory[curAddr + 2], c = memory[curAddr + 3];

				if (a > memory.Length || b > memory.Length || c > memory.Length)
				{
					Console.WriteLine("ERROR: Out of Bounds");
					return 0;
				}

				if (opCode == 1) //Add
					memory[c] = memory[a] + memory[b];
				if (opCode == 2) //Multiply
					memory[c] = memory[a] * memory[b];

				curAddr += 4;
			}
		}

		public static void Execute()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine("Day 2:");
			var baseInput = Utilz.ParseIntCsv("Day2/input.csv");

			int targetOutput = 19690720;
			for (int n = 0; n < 100; n++)
			{
				for (int v = 0; v < 100; v++)
				{
					var curInput = new int[baseInput.Length];
					Array.Copy(baseInput, curInput, baseInput.Length);
					if (ExecuteCode(curInput, n, v) == targetOutput)
					{
						Console.WriteLine(100 * n + v);
						stopwatch.Stop();
						Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
						return;
					}
				}
			}
		}
	}
}
