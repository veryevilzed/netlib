using System;
using System.Collections.Generic;
using NetLib;

namespace TestNetLib {
	class MainClass {
		public static void Main(string[] args) {
			Console.WriteLine("---");
			Packet p = new Packet(new byte[] { 1, 255 });
			Console.WriteLine(p.ToString());
			Packet p2 = new Packet(p.ToString());
			Console.WriteLine(p2.ToString());
			Packet p3 = new Packet(new byte[] { 3, 15 });
			Console.WriteLine(p + p2 + p3);

			Dictionary<Packet, string> pd = new Dictionary<Packet, string>();
			pd.Add(p, "p0");
			pd.Add(p3, "p3");

			Console.WriteLine(new Packet(new byte[] { 1, 2 }).GetHashCode());
			Console.WriteLine(new Packet(new byte[] { 1, 2 }).GetHashCode());

			Console.WriteLine(pd.ContainsKey(new Packet(new byte[] {1, 255})));
			Console.WriteLine(pd[new Packet(new byte[] {1, 255})]);


		}
	}
}
