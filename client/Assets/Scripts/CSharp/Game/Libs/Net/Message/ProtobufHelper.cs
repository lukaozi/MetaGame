using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;

namespace MetaGame
{
	public static class ProtobufHelper
	{
		public static object FromBytes(Type type, byte[] bytes, int index, int count)
		{
			using (MemoryStream stream = new MemoryStream(bytes, index, count))
			{
				object o = RuntimeTypeModel.Default.Deserialize(stream, null, type);
				if (o is ISupportInitialize supportInitialize)
				{
					supportInitialize.EndInit();
				}

				return o;
			}
		}

		public static byte[] ToBytes(object message)
		{
			using (MemoryStream stream = new MemoryStream())
			{
				ProtoBuf.Serializer.Serialize(stream, message);
				return stream.ToArray();
			}
		}

		public static void ToStream(object message, MemoryStream stream)
		{
			ProtoBuf.Serializer.Serialize(stream, message);
		}

		public static object FromStream(Type type, MemoryStream stream)
		{
			object o = RuntimeTypeModel.Default.Deserialize(stream, null, type);
			if (o is ISupportInitialize supportInitialize)
			{
				supportInitialize.EndInit();
			}
			return o;
		}

		public static object FromBytes(object instance, byte[] bytes, int index, int count)
		{
			Type type = instance.GetType();
			using (MemoryStream stream = new MemoryStream(bytes, index, count))
			{
				object o = RuntimeTypeModel.Default.Deserialize(stream, instance, type);
				if (o is ISupportInitialize supportInitialize)
				{
					supportInitialize.EndInit();
				}

				return o;
			}
//			((Google.Protobuf.IMessage)message).MergeFrom(bytes, index, count);
//			ISupportInitialize iSupportInitialize = message as ISupportInitialize;
//			if (iSupportInitialize == null)
//			{
//				return message;
//			}
//			iSupportInitialize.EndInit();
//			return message;
		}

		public static object FromStream(object message, MemoryStream stream)
		{
			Type type = message.GetType();
			object o = RuntimeTypeModel.Default.Deserialize(stream, message, type);
			if (o is ISupportInitialize supportInitialize)
			{
				supportInitialize.EndInit();
			}
			return o;
			
//			// 这个message可以从池中获取，减少gc
//			((Google.Protobuf.IMessage)message).MergeFrom(stream.GetBuffer(), (int)stream.Position, (int)stream.Length);
//			ISupportInitialize iSupportInitialize = message as ISupportInitialize;
//			if (iSupportInitialize == null)
//			{
//				return message;
//			}
//			iSupportInitialize.EndInit();
//			return message;
		}
	}
}