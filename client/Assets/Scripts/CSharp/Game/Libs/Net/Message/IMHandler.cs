using System;

namespace MetaGame
{
	public interface IMHandler
	{
		ETVoid Handle(Session session, object message);
		Type GetMessageType();
	}
}