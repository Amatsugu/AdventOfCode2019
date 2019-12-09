using AdventOfCode.Day_5;
using AdventOfCode.Day4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Day7
{
	public static class AmplificationCircuit
	{
		public static void Execute()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine("Day 7: Amplification Circuit ");

			var cpu = new IntCodeV2();
			var code = Utilz.ParseIntCsv("Day7/input.csv");

			var ex1 = new int[] { 3, 15, 3, 16, 1002, 16, 10, 16, 1, 16, 15, 15, 4, 15, 99, 0, 0 };
			var ex2 = new int[] { 3,23,3,24,1002,24,10,24,1002,23,-1,23,
101,5,23,23,1,24,23,23,4,23,99,0,0 };
			var ex3 = new int[] { 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0 };


			int output = int.MinValue;

			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					for (int k = 0; k < 5; k++)
					{
						for (int l = 0; l < 5; l++)
						{
							for (int m = 0; m < 5; m++)
							{
								var result = RunPhase(cpu, code, new int[] { i, j, k, l, m });
								if (output < result)
								{
									Console.WriteLine($"{i},{j},{k},{l},{m}");
									output = result;
								}
							}
						}
					}
				}
			}
			Console.WriteLine($"Puzzle {output}");
			stopwatch.Stop();
			Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
		}

		public static int RunPhase(IntCodeV2 cpu, int[] code, int[] phaseSettings)
		{
			if (phaseSettings.HasDuplicateValues())
				return int.MinValue;
			int[] outputBuffer = { 0 };
			int[] inputBuffer;
			//Amp A
			inputBuffer = new int[] { phaseSettings[0], outputBuffer[0] };
			cpu.ExecuteCode(code, inputBuffer, outputBuffer);
			//Amp B
			inputBuffer = new int[] { phaseSettings[1], outputBuffer[0] };
			cpu.ExecuteCode(code, inputBuffer, outputBuffer);
			//Amp C
			inputBuffer = new int[] { phaseSettings[2], outputBuffer[0] };
			cpu.ExecuteCode(code, inputBuffer, outputBuffer);
			//Amp D
			inputBuffer = new int[] { phaseSettings[3], outputBuffer[0] };
			cpu.ExecuteCode(code, inputBuffer, outputBuffer);
			//Amp E
			inputBuffer = new int[] { phaseSettings[4], outputBuffer[0] };
			cpu.ExecuteCode(code, inputBuffer, outputBuffer);
			return outputBuffer[0];
		}

		public static bool HasDuplicateValues(this int[] arr)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				for (int j = 0; j < arr.Length; j++)
				{
					if (i == j)
						continue;
					if (arr[i] == arr[j])
						return true;
				}
			}
			return false;
		}
	}
}
