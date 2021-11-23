package meta.protobuf.netty;

import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.MessageToMessageDecoder;
import meta.protobuf.ProtostuffCodec;

import java.util.List;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoStuffDecoder extends MessageToMessageDecoder<ByteBuf> {
    @Override
    protected void decode(ChannelHandlerContext ctx, ByteBuf msg, List<Object> out) {
        if (msg.readableBytes() < 4) {
            return;
        }
        msg.markReaderIndex();
        int dataLength = msg.readInt();
        if (msg.readableBytes() < dataLength) {
            msg.resetReaderIndex();
            return;
        }
        byte[] data = new byte[dataLength];
        msg.readBytes(data);
        Object obj = ProtostuffCodec.deserialize(data, null);
        out.add(obj);
    }
}
