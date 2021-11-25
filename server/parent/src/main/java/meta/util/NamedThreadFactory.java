package meta.util;

import java.util.concurrent.ThreadFactory;
import java.util.concurrent.atomic.AtomicInteger;

/**
 * @author lushengkao vip8
 * 参考 dubbo
 * 2018/10/11 20:17
 */
public class NamedThreadFactory implements ThreadFactory {

    private static final AtomicInteger poolNum = new AtomicInteger(1);
    private final AtomicInteger threadNum = new AtomicInteger(1);
    private final String name;
    private final boolean daemon;
    private final ThreadGroup threadGroup;

    public Thread newThread(Runnable runnable) {
        String name = this.name + threadNum.getAndIncrement();
        Thread result = new Thread(threadGroup, runnable, name, 0L);
        result.setDaemon(daemon);
        return result;
    }

    public NamedThreadFactory(String name) {
        this(name, false);
    }

    public NamedThreadFactory(String name, boolean daemon) {
        this.name = name + "-thread-";
        this.daemon = daemon;
        SecurityManager s = System.getSecurityManager();
        this.threadGroup = s == null ? Thread.currentThread().getThreadGroup() : s.getThreadGroup();
        poolNum.incrementAndGet();
    }
}
