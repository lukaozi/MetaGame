package meta.database.cache;

import com.google.common.cache.Cache;
import com.google.common.cache.CacheBuilder;
import meta.database.Entity;

import java.io.Serializable;
import java.util.concurrent.TimeUnit;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */
public class EntityCache<PK extends Comparable<PK> & Serializable, E extends Entity<PK>> {

    public final static int DEFAULT_CACHE_SIZE = 3000;

    public final static long DEFAULT_EXPIRE_TIME = 24 * 60 * 60 * 1000L;

    private final Class<? extends Entity<PK>> entityClazz;

    private Cache<PK, E> cache;

    public EntityCache(Class<? extends Entity<PK>> entityClazz) {
        this.entityClazz = entityClazz;
    }

    public void initCache() {
        int cacheSize = DEFAULT_CACHE_SIZE;
        long expire = DEFAULT_EXPIRE_TIME;
        CacheDef annotation = entityClazz.getAnnotation(CacheDef.class);
        if (annotation != null) {
            cacheSize = annotation.size();
            expire = annotation.expire();
        }
        cache = CacheBuilder.newBuilder()
                .maximumSize(cacheSize)
                .expireAfterAccess(expire, TimeUnit.MILLISECONDS)
                .build();
    }


    public void remove(PK pk) {
        cache.invalidate(pk);
    }

    public E get(PK pk) {
        return cache.getIfPresent(pk);
    }

    public void put(PK pk, E e) {
        cache.put(pk, e);
    }
}
