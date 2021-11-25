package meta.database;

import meta.database.cache.EntityCache;

import java.io.Serializable;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */
public class EntityManager<PK extends Comparable<PK> & Serializable,E extends Entity<PK>> {


    private Class<? extends Entity<PK>> entityClazz;

    private EntityCache<PK,E> cache;

}
