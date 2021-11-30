package meta.database.cache;

import com.google.common.cache.Cache;
import com.google.common.cache.CacheBuilder;
import meta.database.persister.Entity;

import java.io.Serializable;
import java.util.concurrent.TimeUnit;

/**
 * @author: AK-47
 * @date: 2021/11/25
 *
 * GUAVA LRU实现
 */
public class GuavaEntityCache<PK extends Comparable<PK> & Serializable, E extends Entity<PK>> extends AbstractEntityCache<PK, E> {

    private final Class<? extends Entity<PK>> entityClazz;

    private Cache<PK, E> cache;

    public GuavaEntityCache(Class<? extends Entity<PK>> entityClazz) {
        this.entityClazz = entityClazz;
    }

    @Override
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

    @Override
    public void remove(PK pk) {
        cache.invalidate(pk);
    }

    @Override
    public E get(PK pk) {
        return cache.getIfPresent(pk);
    }

    @Override
    public void put(PK pk, E e) {
        cache.put(pk, e);
    }
}
