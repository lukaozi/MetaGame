package meta.net.netty.ChannelInitializer;

import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelPipeline;
import io.netty.channel.socket.SocketChannel;
import io.netty.handler.codec.LengthFieldBasedFrameDecoder;
import io.netty.handler.codec.LengthFieldPrepender;
import meta.protobuf.netty.ProtoStuffDecoder;
import meta.protobuf.netty.ProtoStuffEncoder;

/**
 * @author: AK-47
 * @date: 2021/11/24
 *
 * 对 服务间通信
 */
public class InnerTcpChannelInitializer extends ChannelInitializer<SocketChannel> {
    @Override
    protected void initChannel(SocketChannel ch) {
        ChannelPipeline pipeline = ch.pipeline();
        pipeline.addLast(new LengthFieldBasedFrameDecoder(Integer.MAX_VALUE, 0, 4, 0, 4));
        pipeline.addLast(new LengthFieldPrepender(4));
        pipeline.addLast(new ProtoStuffDecoder());
        pipeline.addLast(new ProtoStuffEncoder());
    }
}
