package meta.database;

import meta.csv.CsvManager;
import meta.csv.CsvMap;
import meta.log.LoggerUtil;
import meta.springsupport.OrderNum;
import org.springframework.beans.BeansException;
import org.springframework.beans.factory.config.BeanPostProcessor;
import org.springframework.core.Ordered;
import org.springframework.stereotype.Component;

import java.lang.reflect.Field;

/**
 * @author: AK-47
 * @date: 2021/11/29
 */
@Component
public class EntityDataApplicationContextAware implements BeanPostProcessor, Ordered {

    @Override
    public int getOrder() {
        return OrderNum.DB.getOrder();
    }

    @SuppressWarnings("rawtypes")
    @Override
    public Object postProcessAfterInitialization(Object bean, String beanName) throws BeansException {
        Class<?> clazz = bean.getClass();
        Field[] declaredFields = clazz.getDeclaredFields();
        for (Field field : declaredFields) {
            Class<?> fileClazz = field.getType();
            if (!EntityManager.class.equals(fileClazz)) {
                continue;
            }
            field.setAccessible(true);
            try {
                EntityManager entityManager = new EntityManager(fileClazz);
                field.set(bean,entityManager);
            } catch (Exception e) {
                LoggerUtil.error(e.getMessage());
                System.exit(0);
            }
        }
        return bean;
    }
}
