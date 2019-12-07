using AdventOfCode.Day4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Day_5
{
	public class IntCodeV2
	{
		public struct Instruction
		{
			public int opcode;
			public int paramCount;
			public Func<int[], int[], int, int, int, bool> action;

			public Instruction(int opcode, int paramCount, Func<int[], int[], int, int, int, bool> action)
			{
				this.opcode = opcode;
				this.paramCount = paramCount;
				this.action = action;
			}

		}

		private Dictionary<int, Instruction> _instructions;
		private bool _isHalted;
		private int _instructionPointer;

		public IntCodeV2()
		{
			_instructions = new Dictionary<int, Instruction>();
			_isHalted = false;
			//Add
			_instructions.Add(1, new Instruction(1, 3, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				var v2 = mode[1] == 1 ? p2 : mem[p2];
				var v3 = mode[0] == 1 ? mem[p3] : p3;
				mem[v3] = v1 + v2;

				return true;
			}));
			//Multiply
			_instructions.Add(2, new Instruction(2, 3, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				var v2 = mode[1] == 1 ? p2 : mem[p2];
				var v3 = mode[0] == 1 ? mem[p3] : p3;
				mem[v3] = v1 * v2;
				return true;
			}));
			//Halt
			_instructions.Add(99, new Instruction(99, 0, (mem, mode, p1, p2, p3) =>
			{
				_isHalted = true;
				Console.WriteLine("Halt!");
				return false;
			}));
			//Read Input
			_instructions.Add(3, new Instruction(3, 1, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? mem[p1] : p1;
				Console.Write("Input: ");
				var input = int.Parse(Console.ReadLine());
				mem[v1] = input;
				return true;
			}));
			//Write Output
			_instructions.Add(4, new Instruction(4, 1, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				Console.WriteLine(v1);
				return true;
			}));
			//Jump if True
			_instructions.Add(5, new Instruction(5, 2, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				var v2 = mode[1] == 1 ? p2 : mem[p2];
				if (v1 != 0)
				{
					_instructionPointer = v2;
					return false;
				}
				return true;
			}));
			//Jump if False
			_instructions.Add(6, new Instruction(6, 2, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				var v2 = mode[1] == 1 ? p2 : mem[p2];
				if (v1 == 0)
				{
					_instructionPointer = v2;
					return false;
				}
				return true;
			}));
			//Less than
			_instructions.Add(7, new Instruction(7, 3, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				var v2 = mode[1] == 1 ? p2 : mem[p2];
				var v3 = mode[0] == 1 ? mem[p3] : p3;
				if (v1 < v2)
					mem[v3] = 1;
				else
					mem[v3] = 0;
				return true;
			}));
			//Equals
			_instructions.Add(8, new Instruction(8, 3, (mem, mode, p1, p2, p3) =>
			{
				var v1 = mode[2] == 1 ? p1 : mem[p1];
				var v2 = mode[1] == 1 ? p2 : mem[p2];
				var v3 = mode[0] == 1 ? mem[p3] : p3;
				if (v1 == v2)
					mem[v3] = 1;
				else
					mem[v3] = 0;
				return true;
			}));
		}

		public int ExecuteCode(int[] code)
		{
			int[] memory = code;

			while (true)
			{
				var (modes, opcode) = ParseInstruction(memory[_instructionPointer]);
				var curInstruction = _instructions[opcode];
				int[] parameters = new int[3];
				for (int i = 0; i < 3; i++)
				{
					if (i >= curInstruction.paramCount)
						parameters[i] = 0;
					else
						parameters[i] = memory[_instructionPointer + i + 1];
				}
				
				if(curInstruction.action(memory, modes, parameters[0], parameters[1], parameters[2]))
					_instructionPointer += curInstruction.paramCount + 1;

				if (_isHalted)
					return memory[0];
			
			}
		}

		public (int[] opModes, int opcode) ParseInstruction(int instruction)
		{
			var opModes = new int[3];
			var arr = instruction.ToIntArray();
			switch(arr.Length)
			{
				case 1:
					return (opModes, arr[0]);
				case 2:
					return (opModes, (arr[^2] * 10) + arr[^1]);
			}
			var opcode = (arr[^2] * 10) + arr[^1];
			for (int i = 1; i <= 3; i++)
			{
				if (arr.Length < i + 2)
					opModes[^i] = 0;
				else
					opModes[^i] = arr[^(i + 2)];
			}

			return (opModes, opcode);
		}


		public static void Execute()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine("Day 5: INT Code ");
			var baseInput = Utilz.ParseIntCsv("Day5/input.csv");
			var cpu = new IntCodeV2();
			cpu.ExecuteCode(baseInput);

			stopwatch.Stop();
			Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
		}
	}
}
