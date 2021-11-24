package meta.csv;

import meta.util.FastJsonUtils;

/**
 * @author: AK-47
 * @date: 2021/11/24
 */
public class SimpleParser {

    public static Object getFieldValueFromJson(Class<?> type, String valueString) {
        if (type.equals(Long.class) || type.equals(long.class)) {
            return Long.parseLong(valueString);
        }else if (type.equals(Integer.class) || type.equals(int.class)) {
            return Integer.parseInt(valueString);
        }else if (type.equals(Short.class) || type.equals(short.class)) {
            return Short.parseShort(valueString);
        }else if (type.equals(Byte.class) || type.equals(byte.class)) {
            return Byte.parseByte(valueString);
        }else if (type.equals(String.class)) {
            return valueString;
        }
        return FastJsonUtils.fromJson(valueString,type);
    }
}
