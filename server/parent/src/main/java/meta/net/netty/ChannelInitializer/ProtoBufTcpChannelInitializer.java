package meta.net.netty.ChannelInitializer;

import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelPipeline;
import io.netty.channel.socket.SocketChannel;
import io.netty.handler.codec.LengthFieldBasedFrameDecoder;
import io.netty.handler.codec.LengthFieldPrepender;
import meta.protobuf.netty.ProtoBufDecoder;
import meta.protobuf.netty.ProtoBufEncoder;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoBufTcpChannelInitializer extends ChannelInitializer<SocketChannel> {

    @Override
    protected void initChannel(SocketChannel ch) throws Exception {
        ChannelPipeline pipeline = ch.pipeline();
        pipeline.addLast(new LengthFieldBasedFrameDecoder(Integer.MAX_VALUE, 0, 4, 0, 4));
        pipeline.addLast(new LengthFieldPrepender(4));
        pipeline.addLast(new ProtoBufDecoder());
        pipeline.addLast(new ProtoBufEncoder());

    }
}
