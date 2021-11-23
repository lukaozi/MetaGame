package meta.net.transport;


import java.net.InetSocketAddress;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class AbstractServer implements Endpoint {

    protected InetSocketAddress localAddress;

    protected InetSocketAddress remoteAddress;


    @Override
    public InetSocketAddress getLocalAddress() {
        return null;
    }

    @Override
    public InetSocketAddress getRemoteAddress() {
        return null;
    }

    @Override
    public boolean open() {
        return false;
    }

    @Override
    public boolean close() {
        return false;
    }

    @Override
    public boolean isClosed() {
        return false;
    }

    @Override
    public boolean isAvailable() {
        return false;
    }
}
