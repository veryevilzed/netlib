using System;
using System.Collections.Generic;
using System.Reflection;

namespace NetLib {
	public abstract class ProtocolBase : IProtocol {

		protected Packet noOp = null;

		Dictionary<Packet,  NetCommandAttribute> commands;

		public void Receive(ITransport transport, Packet data) {

			if (this.commands.ContainsKey(data)) {
				
			} else if (noOp == null)
				transport.Send(noOp);
		}


		public ProtocolBase() {
			commands = new Dictionary<Packet, NetCommandAttribute>();
			foreach (MethodInfo mi in this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) ) {
				foreach (object o in mi.GetCustomAttributes(false)) {
					if (o is NetCommandAttribute) {
						NetCommandAttribute nca = (NetCommandAttribute)o;
						nca.TargetMethod = mi;
						nca.TargetInstance = this;
						commands.Add(nca.GetPacket(), nca);
					}
				}
			}
		}
	}


	[AttributeUsage(AttributeTargets.Method)]
	public class NetCommandAttribute : System.Attribute  {
		public readonly string Hex;

		public MethodInfo TargetMethod { get; set; }
		public object TargetInstance { get; set; }

		public Packet GetPacket() {
			return new Packet(Hex);
		}

		public NetCommandAttribute(string hex) {
			this.Hex = hex;
		}
	}
}

