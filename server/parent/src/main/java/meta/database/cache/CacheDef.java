package meta.database.cache;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */

@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
public @interface CacheDef {

    int size() default EntityCache.DEFAULT_CACHE_SIZE;

    long expire() default EntityCache.DEFAULT_EXPIRE_TIME;
}
