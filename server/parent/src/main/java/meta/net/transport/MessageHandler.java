package meta.net.transport;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public interface MessageHandler {

    Object handle(Endpoint channel, Object message);

}
