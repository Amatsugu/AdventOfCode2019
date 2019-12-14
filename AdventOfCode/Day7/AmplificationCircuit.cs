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

			var ex1 = new int[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 };
			var ex2 = new int[] { 3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,
-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,
53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10};


			int output = int.MinValue;
			int min = 5;
			int max = 10;

			for (int i = min; i < max; i++)
			{
				for (int j = min; j < max; j++)
				{
					for (int k = min; k < max; k++)
					{
						for (int l = min; l < max; l++)
						{
							for (int m = min; m < max; m++)
							{
								var result = RunFeedback(code, new int[] { i, j, k, l, m });
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

		public static int RunFeedback(int[] code, int[] phaseSettings)
		{
			if (phaseSettings.HasDuplicateValues())
				return int.MinValue;
			var ampA = new IntCodeV2(true, true).LoadCode(code);
			var ampB = new IntCodeV2(true, true).LoadCode(code);
			var ampC = new IntCodeV2(true, true).LoadCode(code);
			var ampD = new IntCodeV2(true, true).LoadCode(code);
			var ampE = new IntCodeV2(true, true).LoadCode(code);
			var outputA = new int[] { 273 };
			var outputB = new int[] { 0 };
			var outputC = new int[] { 0 };
			var outputD = new int[] { 0 };
			var outputE = new int[] { 0 };
			var inputA = new int[] { phaseSettings[0], outputE[0] };
			var inputB = new int[] { phaseSettings[1], outputA[0] };
			var inputC = new int[] { phaseSettings[2], outputB[0] };
			var inputD = new int[] { phaseSettings[3], outputC[0] };
			var inputE = new int[] { phaseSettings[4], outputD[0] };
			ampA.SetIO(inputA, outputA);
			ampB.SetIO(inputB, outputB);
			ampC.SetIO(inputC, outputC);
			ampD.SetIO(inputD, outputD);
			ampE.SetIO(inputE, outputE);
			int iter = 0;
			while (!ampE.IsHalted)
			{
				//Console.WriteLine($"Iteration {iter}");
				inputA[1] = outputE[0];

				ampA.Run();
				inputB[1] = outputA[0];
				ampB.Run();
				inputC[1] = outputB[0];
				ampC.Run();
				inputD[1] = outputC[0];
				ampD.Run();
				inputE[1] = outputD[0];
				ampE.Run();


				//Console.WriteLine($"Output {outputE[0]}");
				iter++;
			}

			return outputE[0];
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
