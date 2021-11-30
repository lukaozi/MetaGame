package meta.database.cache;

import com.github.benmanes.caffeine.cache.Cache;
import com.github.benmanes.caffeine.cache.Caffeine;
import meta.database.persister.Entity;

import java.io.Serializable;
import java.time.Duration;

/**
 * @author: AK-47
 * @date: 2021/11/27
 * <p>
 * W-TINY-LFU实现
 */
public class CaffeineEntityCache<PK extends Comparable<PK> & Serializable, E extends Entity<PK>> extends AbstractEntityCache<PK, E> {

    private final Class<? extends Entity<PK>> entityClazz;

    private Cache<PK, E> cache;

    @Override
    public void initCache() {
        int cacheSize = DEFAULT_CACHE_SIZE;
        long expire = DEFAULT_EXPIRE_TIME;
        CacheDef annotation = entityClazz.getAnnotation(CacheDef.class);
        if (annotation != null) {
            cacheSize = annotation.size();
            expire = annotation.expire();
        }

        cache = Caffeine.newBuilder()
                .maximumSize(cacheSize)
                .expireAfterWrite(Duration.ofMillis(expire))
                .refreshAfterWrite(Duration.ofMillis(expire))
                .build();
    }

    public CaffeineEntityCache(Class<? extends Entity<PK>> entityClazz) {
        this.entityClazz = entityClazz;
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
