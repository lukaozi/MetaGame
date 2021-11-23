package meta.protobuf;

import com.google.protobuf.Message;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtoBufManager {

    private final Map<Integer, Message> messageTemplateMap = new ConcurrentHashMap<>();
}
