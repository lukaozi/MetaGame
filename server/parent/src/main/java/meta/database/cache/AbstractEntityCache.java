package meta.database.cache;

import meta.database.persister.Entity;

import java.io.Serializable;

/**
 * @author: AK-47
 * @date: 2021/11/27
 */
public abstract class AbstractEntityCache<PK extends Comparable<PK> & Serializable, E extends Entity<PK>> {

    public final static int DEFAULT_CACHE_SIZE = 3000;

    public final static long DEFAULT_EXPIRE_TIME = 24 * 60 * 60 * 1000L;

    public abstract void initCache();

    public abstract void remove(PK pk);

    public abstract E get(PK pk);

    public abstract void put(PK pk, E e);
}
