package meta.net.netty.ChannelInitializer;

import com.google.protobuf.Message;
import com.google.protobuf.MessageLite;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelPipeline;
import io.netty.channel.socket.SocketChannel;
import io.netty.handler.codec.protobuf.ProtobufDecoder;
import io.netty.handler.codec.protobuf.ProtobufVarint32FrameDecoder;
import io.netty.handler.codec.protobuf.ProtobufVarint32LengthFieldPrepender;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoBufTcpChannelInitializer extends ChannelInitializer<SocketChannel> {

    @Override
    protected void initChannel(SocketChannel ch) throws Exception {
        ChannelPipeline pipeline = ch.pipeline();
        pipeline.addLast(new ProtobufVarint32FrameDecoder());


//        pipeline.addLast(new ProtobufDecoder(MessageBase.));
        pipeline.addLast(new ProtobufVarint32LengthFieldPrepender());
    }
}
