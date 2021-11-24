package meta.csv;

import java.lang.reflect.Field;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 * @author: AK-47
 * @date: 2021/11/24
 */
@SuppressWarnings({"rawtypes", "unchecked"})
public class CsvManager {

    private final static Map<String, CsvMap<?, ?>> csvName2Map = new ConcurrentHashMap<>();

    private final static Map<String, Class<?>> csvName2Clazz = new ConcurrentHashMap<>();

    public static CsvMap createMap(Field field) throws ClassNotFoundException {
        //获取 map 字段的泛型参数
        ParameterizedType mapGenericType = (ParameterizedType) field.getGenericType();
        Type[] mapActualTypeArguments = mapGenericType.getActualTypeArguments();
        Type valueType = mapActualTypeArguments[1];
        String typeName = valueType.getTypeName();
        Class<?> mapValueClass = Class.forName(typeName);
        CsvMap result = new CsvMap(mapValueClass);
        CsvName annotation = mapValueClass.getAnnotation(CsvName.class);
        if (annotation == null) {
            result.setResourceName(mapValueClass.getSimpleName());
        } else {
            result.setResourceName(annotation.name());
        }
        Field idField = getIdField(mapValueClass);
        result.setIdFiled(idField);
        return result;
    }

    private static Field getIdField(Class<?> clazz) {
        Field[] fields = clazz.getDeclaredFields();
        for (Field field : fields) {
            if (field.getAnnotation(Id.class) != null) {
                return field;
            }
        }
        throw new RuntimeException("class " + clazz.getSimpleName() + " has no id");
    }

    public static void initMap(CsvMap map) throws Exception {
        String resourceName = map.getResourceName();
        CsvUtil.readResource(map);
        csvName2Map.putIfAbsent(resourceName, map);
        csvName2Clazz.putIfAbsent(resourceName, map.getClass());
    }
}