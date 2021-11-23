package meta.net.serialize;

import com.google.protobuf.Descriptors;
import com.google.protobuf.ProtocolMessageEnum;

/**
 * @author: AK-47
 * @date: 2021/11/23
 */
public enum CommonType implements ProtocolMessageEnum {
    ;


    @Override
    public int getNumber() {
        return 0;
    }

    @Override
    public Descriptors.EnumValueDescriptor getValueDescriptor() {
        return null;
    }

    @Override
    public Descriptors.EnumDescriptor getDescriptorForType() {
        return null;
    }
}
