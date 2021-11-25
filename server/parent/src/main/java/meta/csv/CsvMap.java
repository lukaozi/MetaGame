package meta.csv;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.lang.reflect.Field;
import java.util.*;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentSkipListSet;
import java.util.concurrent.CopyOnWriteArrayList;


/**
 * @author: AK-47
 * @date: 2021/11/24
 */
public class CsvMap<K, V> {

    private static final Logger LOGGER = LoggerFactory.getLogger(CsvMap.class);

    private String resourceName;

    private Field idFiled;

    public CsvMap(Class<?> valueClass) {
        this.valueClass = valueClass;
    }

    private Class<?> valueClass;
    /**
     * 数据集合
     */
    private List<V> dataList = new CopyOnWriteArrayList<>();

    /**
     * 主键索引
     */
    private Map<K, V> dataMap = new ConcurrentHashMap<>();


    public Collection<V> getAll() {
        return Collections.unmodifiableCollection(dataList);
    }

    public V get(K key) {
        return dataMap.get(key);
    }


    public String getResourceName() {
        return resourceName;
    }

    public void setResourceName(String resourceName) {
        this.resourceName = resourceName;
    }

    public Field getIdFiled() {
        return idFiled;
    }

    public void setIdFiled(Field idFiled) {
        this.idFiled = idFiled;
    }

    public void addRow(K idValue, V row) {
        V oldValue = dataMap.put(idValue, row);
        if (oldValue != null) {
            dataList.remove(oldValue);
        }
        dataList.add(row);
    }

    public Class<?> getValueClass() {
        return valueClass;
    }
}
