using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AdventOfCode.Day6
{
	public class OrbitMap
	{
		public CelestialObject root;
		public Dictionary<string, CelestialObject> objectMap;

		public OrbitMap(string[] orbits)
		{
			objectMap = new Dictionary<string, CelestialObject>();
			GenerateOrbits(orbits);
		}

		public void GenerateOrbits(string[] orbits)
		{
			for (int i = 0; i < orbits.Length; i++)
			{
				var bodies = orbits[i].Split(')');
				if (bodies[0] == "COM")
					root = CreateObject("COM");

				var parent = GetOrCreateObject(bodies[0]);
				var child = GetOrCreateObject(bodies[1]);

				parent.AddChild(child);
			}
		}

		public CelestialObject GetOrCreateObject(string name)
		{
			if (objectMap.ContainsKey(name))
				return objectMap[name];
			else
				return CreateObject(name);
		}

		public CelestialObject CreateObject(string name)
		{
			var o = new CelestialObject(name);
			objectMap.Add(name, o);
			return o;
		}

		public int CalculateOrbits()
		{
			return root.GetOrbitCount();
		}

		public int GetDepthOf(string name) => root.GetDepthOf(name);


		public class CelestialObject
		{
			public string Name { get; set; }
			public int ChildCount => children.Count;

			public List<CelestialObject> children;

			public CelestialObject(string name)
			{
				children = new List<CelestialObject>();
				Name = name;
			}

			public void AddChild(CelestialObject child)
			{
				children.Add(child);
			}

			public int GetOrbitCount(int depth = 0)
			{
				var count = 0;
				for (int i = 0; i < children.Count; i++)
				{
					count += children[i].GetOrbitCount(depth + 1);
				}
				return depth + count;
			}

			public int GetDepthOf(string name, int depth = 0)
			{
				if (Name == name)
					return depth;
				else
				{
					int d = 0;
					for (int i = 0; i < ChildCount; i++)
					{
						d += children[i].GetDepthOf(name, depth + 1);
					}
					return d;
				}
			}
		}


		public static void Execute()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			Console.WriteLine("Day 6: Orbit Maps ");

			var map = new OrbitMap(File.ReadAllLines("Day6/input.txt"));

			Console.WriteLine(map.GetDepthOf("YOU") - map.GetDepthOf("SAN") - 2);

			stopwatch.Stop();
			Console.WriteLine($"{stopwatch.ElapsedMilliseconds}ms Elapsed");
		}
	}
}
