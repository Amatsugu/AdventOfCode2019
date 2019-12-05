using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using static AdventOfCode.Day3.CrossedWires;

namespace UnitTests
{
	[TestClass]
	public class CrossedWiresTests
	{
		[TestMethod]
		public void OnSegmentVert()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(0, 5));
			Assert.IsTrue(segment.ContainsPoint(segment.min), "Start point on segment");
			Assert.IsTrue(segment.ContainsPoint(new Point(0, 3)), "Mid point on segment");
			Assert.IsTrue(segment.ContainsPoint(segment.max), "End point on segment");
		}

		[TestMethod]
		public void OnSegmentHoriz()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(5, 0));
			Assert.IsTrue(segment.ContainsPoint(segment.min), "Start point on segment");
			Assert.IsTrue(segment.ContainsPoint(new Point(3, 0)), "Mid point on segment");
			Assert.IsTrue(segment.ContainsPoint(segment.max), "End point on segment");
		}

		[TestMethod]
		public void CreateRelative()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(0, 5));
			var segment2 = segment.CreateRelative(new Point(5, 0));

			Assert.AreEqual(segment2.min, segment.max, "First Point");
			Assert.AreEqual(new Point(5,5), segment2.max, "Second Point");
		}

		[TestMethod]
		public void Contains()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(0, 5));
			var segment2 = new WireSegment(new Point(0, 3), new Point(0, 5));

			Assert.IsTrue(segment.Contains(segment2));
			Assert.IsFalse(segment2.Contains(segment));
		}

		[TestMethod]
		public void GetOverlapVert()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(0, 5));
			var segment2 = new WireSegment(new Point(0, 3), new Point(0, 8));
			var segment3 = new WireSegment(new Point(0, 3), new Point(0, 5));

			var overlap = segment.GetOverlap(segment2);
			Assert.AreEqual(segment2.min, overlap.min, "Start Point");
			Assert.AreEqual(segment.max, overlap.max, "End Point");
			overlap = segment.GetOverlap(segment3);
			Assert.AreEqual(segment3.min, overlap.min, "Contaiend Start");
			Assert.AreEqual(segment3.max, overlap.max, "Contaiend End");
		}

		[TestMethod]
		public void GetOverlapHoriz()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(5, 0));
			var segment2 = new WireSegment(new Point(3, 0), new Point(8, 0));
			var segment3 = new WireSegment(new Point(3, 0), new Point(5, 0));

			var overlap = segment.GetOverlap(segment2);
			Assert.AreEqual(segment2.min, overlap.min, "Start Point");
			Assert.AreEqual(segment.max, overlap.max, "End Point");
			overlap = segment.GetOverlap(segment3);
			Assert.AreEqual(segment3.min, overlap.min, "Contaiend Start");
			Assert.AreEqual(segment3.max, overlap.max, "Contaiend End");
		}

		[TestMethod]
		public void Intersect()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(5, 0));
			var segment2 = new WireSegment(new Point(3, -2), new Point(3, 5));

			var i = segment.Intersect(segment2, out var intr);
			Assert.IsTrue(i, "Interescted");
			Assert.AreEqual(new Point(3,0), intr, "Interesction Point");

		}

		[TestMethod]
		public void IntersectOverlappedVert()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(0, 5));
			var segment2 = new WireSegment(new Point(0, 3), new Point(0, 7));

			var i = segment.Intersect(segment2, out var intr);
			Assert.IsTrue(i, "Interescted");
			Assert.AreEqual(new Point(0, 3), intr, "Interesction Point");

			i = segment2.Intersect(segment, out intr);
			Assert.IsTrue(i, "Interescted 2");
			Assert.AreEqual(new Point(0, 3), intr, "Interesction Point 2");
		}

		[TestMethod]
		public void IntersectOverlappedHoriz()
		{
			var segment = new WireSegment(new Point(0, 0), new Point(5, 0));
			var segment2 = new WireSegment(new Point(3, 0), new Point(7, 0));

			var i = segment.Intersect(segment2, out var intr);
			Assert.IsTrue(i, "Interescted");
			Assert.AreEqual(new Point(3, 0), intr, "Interesction Point");

			i = segment2.Intersect(segment, out intr);
			Assert.IsTrue(i, "Interescted 2");
			Assert.AreEqual(new Point(3, 0), intr, "Interesction Point 2");
		}
	}
}
