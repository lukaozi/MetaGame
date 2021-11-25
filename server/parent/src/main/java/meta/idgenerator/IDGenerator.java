package meta.idgenerator;


import java.util.Objects;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.atomic.AtomicLong;

/**
 * id生成器
 *
 * @author lushengkao vip8
 * 2018/11/27 12:00
 */
public class IDGenerator {

    private static final ConcurrentHashMap<Integer, SnowFlakeIdWorker> SNOW_FLAKE_ID_WORKER_MAP = new ConcurrentHashMap<>();

    private final static ConcurrentHashMap<Integer, AtomicLong> LOCAL_ID_MAP = new ConcurrentHashMap<>();

    public static Long createDBId(int type) {
        return createIdBySnowFlake(type);
    }

    private static Long createLocalId(int type) {
        AtomicLong atomicLong = LOCAL_ID_MAP.get(type);
        if (atomicLong == null) {
            AtomicLong value = new AtomicLong(0);
            AtomicLong old = LOCAL_ID_MAP.putIfAbsent(type, value);
            atomicLong = Objects.requireNonNullElse(old, value);
        }
        return atomicLong.addAndGet(1L);
    }

    private static Long createIdBySnowFlake(int type) {
        SnowFlakeIdWorker snowFlakeIdWorker = SNOW_FLAKE_ID_WORKER_MAP.get(type);
        if (snowFlakeIdWorker == null) {
            SnowFlakeIdWorker value = new SnowFlakeIdWorker(1);
            SnowFlakeIdWorker old = SNOW_FLAKE_ID_WORKER_MAP.putIfAbsent(type, value);
            snowFlakeIdWorker = Objects.requireNonNullElse(old, value);
        }
        return snowFlakeIdWorker.nextId();
    }
}
