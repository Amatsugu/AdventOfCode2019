using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public static class Utilz
	{
		public static int[] ParseIntArray(string file)
		{
			return File.ReadAllLines(file).Select(s => int.Parse(s)).ToArray();
		}

		public static int[] ParseIntCsv(string file)
		{
			return File.ReadAllText(file).Split(',').Select(s => int.Parse(s)).ToArray();
		}

	}
}
