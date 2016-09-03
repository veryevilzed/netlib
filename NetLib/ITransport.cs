using System;

namespace NetLib {
	public interface ITransport {
		void Send(Packet data);
		bool IsReady { get; }
		IProtocol Protocol {get;set;}
	}
}

