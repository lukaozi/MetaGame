package meta.net.netty;

import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;
import meta.log.LoggerUtil;

import java.net.InetSocketAddress;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

/**
 * @author: AK-47
 * @date: 2021/11/23
 *
 * channel 最大连接数管理
 */
public class NettyChannelCountManage extends ChannelInboundHandlerAdapter {

    private ConcurrentMap<String, Channel> channels = new ConcurrentHashMap<>();

    private int maxChannel;

    public NettyChannelCountManage(int maxChannel) {
        super();
        this.maxChannel = maxChannel;
    }

    @Override
    public void channelRegistered(ChannelHandlerContext ctx) throws Exception {
        Channel channel = ctx.channel();
        if (channels.size() >= maxChannel) {
            // 超过最大连接数限制，直接close连接
//            LoggerUtil.warn("NettyServerChannelManage channelConnected channel size out of limit: limit={} current={}", maxChannel, channels.size());
            channel.close();
        } else {
//            String channelKey = getChannelKey((InetSocketAddress) channel.localAddress(), (InetSocketAddress) channel.remoteAddress());
//            channels.put(channelKey, channel);
//            ctx.fireChannelRegistered();
        }
    }
}
