using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
	public static class Utilz
	{
		public static int[] ParseIntArray(string dir)
		{
			return File.ReadAllLines(dir).Select(s => int.Parse(s)).ToArray();
		}

	}
}
