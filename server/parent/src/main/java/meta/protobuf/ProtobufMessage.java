package meta.protobuf;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public class ProtobufMessage {

    /**
     * 协议号
     */
    private int messageId;

    private byte[] data;

    public int getMessageId() {
        return messageId;
    }

    public void setMessageId(int messageId) {
        this.messageId = messageId;
    }

    public byte[] getData() {
        return data;
    }

    public void setData(byte[] data) {
        this.data = data;
    }

    public static ProtobufMessage valueOf(int messageId,byte[] data) {
        ProtobufMessage result = new ProtobufMessage();
        result.setData(data);
        result.setMessageId(messageId);
        return result;
    }
}
