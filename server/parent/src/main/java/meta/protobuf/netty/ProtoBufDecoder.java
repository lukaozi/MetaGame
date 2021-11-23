package meta.protobuf.netty;

import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.MessageToMessageDecoder;
import meta.protobuf.ProtobufMessage;

import java.util.List;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoBufDecoder extends MessageToMessageDecoder<ByteBuf> {

    @Override
    protected void decode(ChannelHandlerContext ctx, ByteBuf msg, List<Object> out) throws Exception {
        int id = msg.readInt();
        byte[] bytes = new byte[msg.readableBytes()];
        msg.readBytes(bytes);
        out.add(ProtobufMessage.valueOf(id, bytes));
    }
}
