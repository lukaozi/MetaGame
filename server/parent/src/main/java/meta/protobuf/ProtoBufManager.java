package meta.protobuf;

import com.google.protobuf.Message;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoBufManager {

    private static final Map<Integer, Message> MESSAGE_ID = new ConcurrentHashMap<>();

    private static final Map<Class<? extends Message>, Integer> ID_MESSAGE = new ConcurrentHashMap<>();


    public void register(int messageId, Message message) {
        MESSAGE_ID.putIfAbsent(messageId, message);
        ID_MESSAGE.putIfAbsent(message.getClass(), messageId);
    }
}
