using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day4
{
	public static class SecureContainer
	{
		public static bool IsValidPassword(this int[] password)
		{
			if (password.Length != 6)
				return false;
			return password.HasRepeating() && password.IsAssending();
		}

		public static bool HasRepeating(this int[] password)
		{
			for (int i = 0; i < 5; i++)
			{
				if (password[i] == password[i + 1])
					return true;
			}
			return false;
		}

		public static bool HasDoubles(this int[] password)
		{

			bool foundDouble = false;
			for (int i = 0; i < 6; i++)
			{
				int c = 0;
				for (int j = 0; j < 6; j++)
				{
					if (password[j] == password[i])
						c++;
					else
					{
						if (c != 0)
						{
							if (c == 2)
								foundDouble = true;
							c = 0;
						}
					}
				}
				if (c == 2)
					foundDouble = true;
			}
			return foundDouble;

		}

		public static bool IsAssending(this int[] password)
		{
			for (int i = 1; i < 6; i++)
			{
				if (password[i] < password[i - 1])
				{
					return false;
				}
			}
			return true;
		}

		public static int ToInt(this int[] intArr)
		{
			int value = 0;
			for (int i = 0; i < intArr.Length; i++)
			{
				value += (int)Math.Pow(10, intArr.Length - i - 1) * intArr[i];
			}
			return value;
		}

		public static int[] ToIntArray(this int number)
		{
			int[] intArr = number.ToString().Select(d => int.Parse(d.ToString())).ToArray();
			return intArr;
		}

		public static int CountPasswords(int lower, int upper)
		{
			int passwordCount = 0;
			int[] curPassword = lower.ToIntArray();
			CleanPassword(ref curPassword);
			while(curPassword.ToInt() <= upper)
			{
				if (curPassword.HasDoubles())
				{
					passwordCount++;
				}
				curPassword[^1]++;
				Propagate(ref curPassword, curPassword.Length - 1);
				CleanPassword(ref curPassword);
			}
			return passwordCount;
		}

		public static void CleanPassword(ref int[] password)
		{
			for (int i = 1; i < 6; i++)
			{
				if(password[i] < password[i-1])
				{
					password[i] += password[i-1] - password[i];
					if (password[i] == 10)
					{
						Propagate(ref password, i);
						password[i] = password[i - 1];
					}
				}
			}
		}

		public static void Propagate(ref int[] password, int digit)
		{
			for (int i = digit; i >= 0; i--)
			{
				if (i == 0 && password[i] == 10)
				{
					password[i] = 9;
					break;
				}

				if (password[i] == 10)
				{
					password[i] = 0;
					password[i - 1]++;
				}
			}
		}


		public static void Execute()
		{
			Console.WriteLine($"Day 4: Secure Container");
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine(CountPasswords(147981, 691423));
			stopwatch.Stop();
			Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
		}
	}
}
