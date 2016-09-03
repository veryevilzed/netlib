using System;

namespace NetLib {
	public interface IProtocol {
		void Receive(ITransport transport, Packet data);
	}
}

