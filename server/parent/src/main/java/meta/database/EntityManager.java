package meta.database;

import meta.database.cache.AbstractEntityCache;
import meta.database.cache.CacheBuilder;

import java.io.Serializable;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */
public class EntityManager<PK extends Comparable<PK> & Serializable,E extends Entity<PK>> {

    private Class<? extends Entity<PK>> entityClazz;

    private AbstractEntityCache<PK,E> cache;

    @SuppressWarnings("unchecked")
    public EntityManager(Class<? extends Entity<PK>> entityClazz) {
        this.entityClazz = entityClazz;
        cache = CacheBuilder.BuildGuavaCache(entityClazz);
    }
}
