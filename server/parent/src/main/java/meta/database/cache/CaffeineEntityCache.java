package meta.database.cache;

import com.github.benmanes.caffeine.cache.Cache;
import com.github.benmanes.caffeine.cache.Caffeine;
import com.github.benmanes.caffeine.cache.LoadingCache;
import com.google.common.graph.Graph;
import meta.database.Entity;

import java.io.Serializable;
import java.time.Duration;
import java.util.concurrent.TimeUnit;

/**
 * @author: AK-47
 * @date: 2021/11/27
 * <p>
 * W-TINY-LFU实现
 */
public class CaffeineEntityCache<PK extends Comparable<PK> & Serializable, E extends Entity<PK>> extends AbstractEntityCache<PK, E> {


    private Cache<PK, E> cache;

    @Override
    public void initCache() {
        cache = Caffeine.newBuilder()
                .maximumSize(DEFAULT_CACHE_SIZE)
                .expireAfterWrite(Duration.ofMillis(DEFAULT_EXPIRE_TIME))
                .refreshAfterWrite(Duration.ofMillis(DEFAULT_EXPIRE_TIME))
                .build();
    }

    @Override
    public void remove(PK pk) {

    }

    @Override
    public E get(PK pk) {
        return null;
    }

    @Override
    public void put(PK pk, E e) {

    }
}
