using AdventOfCode.Day_5;
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
			//var code = new int[] { 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0 };

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
								int[] outputBuffer = { 0 };
								int[] inputBuffer;
								//Amp A
								inputBuffer = new int[] { i, outputBuffer[0] };
								cpu.ExecuteCode(code, inputBuffer, outputBuffer);
								//Amp B
								inputBuffer = new int[] { j, outputBuffer[0] };
								cpu.ExecuteCode(code, inputBuffer, outputBuffer);
								//Amp C
								inputBuffer = new int[] { k, outputBuffer[0] };
								cpu.ExecuteCode(code, inputBuffer, outputBuffer);
								//Amp D
								inputBuffer = new int[] { l, outputBuffer[0] };
								cpu.ExecuteCode(code, inputBuffer, outputBuffer);
								//Amp E
								inputBuffer = new int[] { m, outputBuffer[0] };
								cpu.ExecuteCode(code, inputBuffer, outputBuffer);
								if (output < outputBuffer[0])
									output = outputBuffer[0];
							}
						}
					}
				}
			}
			Console.WriteLine(output);
			stopwatch.Stop();
			Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
		}
	}
}
