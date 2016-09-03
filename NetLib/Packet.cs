using System;
using System.Linq;
using System.Collections.Generic;

namespace NetLib {
	/// <summary>
	/// Структура пакета, просто для удобства
	/// </summary>
	public class Packet { 

		public byte[] Data { get; set; }

		public static explicit operator byte[](Packet pck) {
			return pck.Data;
		}

		public static implicit operator Packet(byte[] data) {
			return new Packet(data);
		}

		public static bool operator ==(Packet a, Packet b) {
			return a.ToString() == b.ToString();
		}

		public static bool operator !=(Packet a, Packet b) {
			return a.ToString() != b.ToString();
		}

		public int Length {
			get { return Data.Length; }
		}


		public static Packet operator +(Packet a, Packet b) {
			byte[] resData = new byte[a.Data.Length + b.Data.Length];
			System.Buffer.BlockCopy(a.Data, 0, resData, 0, a.Length);
			System.Buffer.BlockCopy(b.Data, 0, resData, a.Length, b.Length);
			return new Packet(resData);
		}

		public override string ToString() {
			return Data.Select(i => i.ToString("X2")).Aggregate((a, b) => a + " " + b).ToUpper();
		}

		public override bool Equals(object obj) {
			if (obj.GetType() != typeof(Packet))
				return false;
			return ((Packet)obj) == this;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var result = 0;
				foreach (byte b in this.Data)
					result = (result*31) ^ b;
				return result;
			}
		}


		public Packet(byte[] data) {
			this.Data = data;
		}

		public Packet(String hex) {
			hex = hex.Replace(" ", "");
			this.Data = Enumerable.Range(0, hex.Length)
				.Where(x => x % 2 == 0)
				.Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
				.ToArray();
		}
	}
}

