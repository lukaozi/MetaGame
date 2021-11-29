package meta.database.cache;


/**
 * @author: AK-47
 * @date: 2021/11/27
 * 判断一个缓存的好坏最核心的指标就是命中率，影响缓存命中率有很多因素，包括业务场景、淘汰策略、清理策略、缓存容量等等
 * 此处提供  Guava Cache 和 caffeine cache
 * Guava Cache 是一个简单的 lru concurrentHashMap
 * caffeine Cache 是一个lfu的变种 w-tiny-lfu
 * <p>
 * 首先需要明确 LRU 和 LFU 的区别
 * <p>
 * LRU对突发性的稀疏流量（sparse bursts）表现很好，但同时也会产生缓存污染，
 * 举例来说，如果偶然性的要对全量数据进行遍历，那么“历史访问记录”就会被刷走，，突发数据会被留下，造成污染。
 * <p>
 * 如果数据的分布在一段时间内是固定的话，那么LFU可以达到最高的命中率。
 * 但是LFU有两个缺点，
 * 第一，它需要给每个记录项维护频率信息，每次访问都需要更新，这是个巨大的开销；
 * 第二，前期经常访问的记录已经占用了缓存，偶然的流量不太可能会被保留下来，前期大量被访问的记录会留存下来。
 * <p>
 * caffeine Cache 提供的 w-tiny-lfu 使用了布隆计数器和滑动窗口的方式规避了LFU的两个缺点
 * 使支持，1 低内存的频率计数器 2对突发流量的比较留存
 */
public class CacheBuilder {

    public static AbstractEntityCache BuildGuavaCache(Class pkClass) {
        return new GuavaEntityCache(pkClass);
    }

    public static AbstractEntityCache BuildCaffeineCache(Class pkClass) {
        return new CaffeineEntityCache(pkClass);
    }
}
