package meta.net.transport;

import java.net.InetSocketAddress;

/**
 * @author: AK-47
 * @date: 2021/11/23
 * <p>
 * 端口 for client or server
 */
public interface Endpoint {

    /**
     * get local socket address.
     */
    InetSocketAddress getLocalAddress();

    /**
     * get remote socket address
     */
    InetSocketAddress getRemoteAddress();

    /**
     * open this endPoint
     */
    boolean open();

    /**
     * close this endPoint
     */
    boolean close();

    boolean isClosed();

    boolean isAvailable();
}
