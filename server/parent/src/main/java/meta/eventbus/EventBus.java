package meta.eventbus;

import meta.util.ThreadPoolUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.stereotype.Component;
import org.springframework.util.CollectionUtils;

import javax.annotation.PostConstruct;
import java.lang.reflect.Method;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.CopyOnWriteArrayList;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.ScheduledThreadPoolExecutor;

/**
 * @author lushengkao vip8
 * Dispatches events to listeners, and provides ways for listeners to register themselves.
 *
 * 2018/10/26 15:10
 */
@Component
public class EventBus {

    private final Logger logger =  LoggerFactory.getLogger(EventBus.class);

    private static EventBus instance;

    public static EventBus getInstance() {
        return instance;
    }

    @PostConstruct
    private void init() {
        instance = this;
    }

    private final int THREAD_SIZE = 3;

    //所有监听者 event--监听者列表
    private final Map<Class<?>, List<Subscriber>> subscribersMap = new ConcurrentHashMap<>();

    private final ScheduledThreadPoolExecutor[] executorServices = ThreadPoolUtils.createSingleExecutorServiceArray("event-bus",THREAD_SIZE);

    //注册监听者
    void registerSubscriber(Object bean) {
        Class<?> clazz = bean.getClass();
        Method[] declaredMethods = clazz.getDeclaredMethods();
        for (Method method : declaredMethods) {
            Subscribe annotation = method.getAnnotation(Subscribe.class);
            if (annotation != null) {
                Class<?>[] parameterTypes = method.getParameterTypes();
                if (parameterTypes.length != 1) {
                    throw new IllegalArgumentException("监听者注册失败，应只有一个参数，类名:" + clazz.getName());
                }
                Class<?> parameterType = parameterTypes[0];
                if (!IEvent.class.isAssignableFrom(parameterType)) {
                    throw new IllegalArgumentException("监听者注册失败，必须继承IEvent接口，类名:" + clazz.getName());
                }
                registerSubscriber0(bean,method, parameterType);
            }
        }
    }

    private void registerSubscriber0(Object bean, Method method, Class<?> event) {
        Subscriber subscriber = new Subscriber(bean,method);
        CopyOnWriteArrayList<Subscriber> value = new CopyOnWriteArrayList<>();
        List<Subscriber> subscribers = subscribersMap.putIfAbsent(event, value);
        if (subscribers != null) {
            subscribers.add(subscriber);
        }else {
            value.add(subscriber);
        }
    }

    public void syncSubmit(IEvent event) {
        List<Subscriber> subscribers = subscribersMap.get(event.getClass());
        if (CollectionUtils.isEmpty(subscribers)) {
            return;
        }
        update(event, subscribers);
    }

    public void asyncSubmit(final IEvent event) {
        final List<Subscriber> subscribers = subscribersMap.get(event.getClass());
        if (CollectionUtils.isEmpty(subscribers)) {
            return;
        }
        ExecutorService executorService = selectExecutorService(event);
        executorService.submit(() -> update(event,subscribers));
    }

    private void update(IEvent event, List<Subscriber> subscribers) {
        for (Subscriber subscriber : subscribers) {
            Method method = subscriber.getMethod();
            Object bean = subscriber.getBean();
            try {
                method.invoke(bean,event);
            } catch (Exception e) {
                logger.info(e.getMessage());
            }
        }
    }

    private ExecutorService selectExecutorService(IEvent event) {
        return executorServices[(int) (event.getId() % THREAD_SIZE)];
    }
}
