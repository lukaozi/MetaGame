package meta.database;


import meta.util.ReflectionUtils;
import meta.util.SimpleParser;

import java.beans.Transient;
import java.lang.reflect.Field;
import java.util.HashMap;
import java.util.Map;

/**
 * @author: AK-47
 * @date: 2021/11/25
 * <p>
 * 增量更新支持器
 */
public class EntityUpdateSupport {

    private Class<? extends Entity> entityClazz;

    public EntityUpdateSupport(Class<? extends Entity> entityClazz) {
        this.entityClazz = entityClazz;
    }

    private Map<String, Object> oldValueMap = new HashMap<>();

    public Map<String, Object> getUpdateValueMap(Entity entity) {
        Map<String, Object> result = new HashMap<>();
        Field[] declaredFields = entityClazz.getDeclaredFields();
        for (Field field : declaredFields) {
            if (!ReflectionUtils.isBaseType(field)) {
                continue;
            }
            if (field.getAnnotation(Transient.class) != null) {
                continue;
            }
            String fieldName = field.getName();
            Object fieldValue = ReflectionUtils.getFieldValue(entity, fieldName);
            if (fieldValue == null) {
                continue;
            }
            Object oldValue = oldValueMap.put(fieldName, fieldValue);
            if (oldValue == null || !oldValue.equals(fieldValue)) {
                result.put(fieldName, fieldValue);
            }
        }
        return result;
    }
}
