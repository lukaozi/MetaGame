package meta.util;

import java.io.File;
import java.lang.reflect.Field;

/**
 * @author: AK-47
 * @date: 2021/11/24
 */
public class ReflectionUtils {

    public static Object getFieldValue(Object object, String fieldName) {
        Field[] declaredFields = object.getClass().getDeclaredFields();
        for (Field field : declaredFields) {
            if (field.getName().equals(fieldName)) {
                field.setAccessible(true);
                Object value;
                try {
                    value = field.get(object);
                } catch (IllegalAccessException e) {
                    return null;
                }
                return value;
            }
        }
        return null;
    }

    public static Class<?> getFieldType(Class<?> valueClass, String fieldName) {
        Field[] declaredFields = valueClass.getDeclaredFields();
        for (Field field : declaredFields) {
            if (field.getName().equals(fieldName)) {
                return field.getType();
            }
        }
        return null;
    }

    public static void setFieldValue(Object object, String fieldName, Object fieldValue)
            throws NoSuchFieldException, IllegalAccessException {
        Class<?> aClass = object.getClass();
        Field field = aClass.getDeclaredField(fieldName);
        field.setAccessible(true);
        field.set(object, fieldValue);
    }

    /**
     * @return 是否基础数据类型
     */
    public static boolean isBaseType(Field field) {
        Class<?> type = field.getType();
        return type.equals(Long.class) || type.equals(long.class)
                || type.equals(Integer.class) || type.equals(int.class)
                || type.equals(Short.class) || type.equals(short.class)
                || type.equals(Byte.class) || type.equals(byte.class)
                || type.equals(String.class);
    }
}
