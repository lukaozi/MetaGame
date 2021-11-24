package meta.csv;

import meta.log.LoggerUtil;
import meta.springsupport.OrderNum;
import org.springframework.beans.BeansException;
import org.springframework.beans.factory.config.BeanPostProcessor;
import org.springframework.core.Ordered;
import org.springframework.stereotype.Component;

import java.lang.reflect.Field;

/**
 * @author: AK-47
 * @date: 2021/11/24
 */

@SuppressWarnings("rawtypes")
@Component
public class CsvDataApplicationContextAware implements BeanPostProcessor, Ordered {

    @Override
    public int getOrder() {
        return OrderNum.CSV.getOrder();
    }


    @Override
    public Object postProcessAfterInitialization(Object bean, String beanName) throws BeansException {
        Class<?> clazz = bean.getClass();
        Field[] declaredFields = clazz.getDeclaredFields();
        for (Field field : declaredFields) {
            Class<?> fileClazz = field.getType();
            if (!CsvMap.class.equals(fileClazz)) {
                continue;
            }
            field.setAccessible(true);
            try {
                CsvMap map = CsvManager.createMap(field);
                CsvManager.initMap(map);
                field.set(bean,map);
            } catch (Exception e) {
                LoggerUtil.error(e.getMessage());
                System.exit(0);
            }
        }
        return bean;
    }
}
