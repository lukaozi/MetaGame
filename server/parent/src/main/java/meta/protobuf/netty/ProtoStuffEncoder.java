package meta.protobuf.netty;

import io.netty.channel.ChannelHandlerContext;
import io.netty.handler.codec.MessageToMessageEncoder;
import meta.protobuf.ProtobufMessage;

import java.util.List;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoStuffEncoder extends MessageToMessageEncoder<ProtobufMessage> {
    @Override
    protected void encode(ChannelHandlerContext ctx, ProtobufMessage msg, List<Object> out) throws Exception {

    }
}
