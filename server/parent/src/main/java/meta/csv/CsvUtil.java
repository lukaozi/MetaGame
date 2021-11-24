package meta.csv;

import com.opencsv.CSVReader;
import meta.util.ReflectionUtils;
import org.apache.commons.lang3.StringUtils;
import org.springframework.util.ResourceUtils;

import java.io.*;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

/**
 * @author: AK-47
 * @date: 2021/11/24
 * 读取csv
 */
@SuppressWarnings({"unchecked", "rawtypes", "ConstantConditions"})
public class CsvUtil {

    private static final String basePath = "classpath:csv/";

    private static final String end = ".csv";

    public static CSVReader getReader(String resourceName) throws IOException {
        String fileName = basePath + resourceName + end;
        File file = ResourceUtils.getFile(fileName);
        FileReader fileReader = new FileReader(file);
        return new CSVReader(fileReader);
    }


    public static void readResource(CsvMap csvMap) throws Exception {
        Field idFiled = csvMap.getIdFiled();
        String idName = idFiled.getName();
        String resourceName = csvMap.getResourceName();
        CSVReader reader = getReader(resourceName);
        String[] single;
        boolean end = false;
        CsvData csvData = new CsvData();
        while ((single = reader.readNext()) != null && !end) {
            if (single[0].equalsIgnoreCase("server")) {
                csvData.initCol(single);
                continue;
            }
            if (!csvData.isInit()) {
                continue;
            }
            if (single[0].equalsIgnoreCase("end")) {
                end = true;
            }
            Object row = createRowData(csvMap, csvData, single);
            Object idValue = getIdValue(row, idName);
            if (idValue == null) {
                continue;
            }
            csvMap.addRow(idValue, row);
        }
        reader.close();
    }

    private static Object getIdValue(Object row, String idName) {
        return ReflectionUtils.getFieldValue(row, idName);
    }

    private static Object createRowData(CsvMap csvMap, CsvData csvData, String[] single) throws Exception {
        Class valueClass = csvMap.getValueClass();
        Object resource = valueClass.getDeclaredConstructor().newInstance();
        Map<String, Integer> name2Col = csvData.name2Col;
        for (Map.Entry<String, Integer> entry : name2Col.entrySet()) {
            String fieldName = entry.getKey();
            Integer index = entry.getValue();
            String valueString = single[index];
            Class<?> type = ReflectionUtils.getFieldType(valueClass, fieldName);
            Object value = SimpleParser.getFieldValueFromJson(type, valueString);
            ReflectionUtils.setFieldValue(resource, fieldName, value);
        }
        return resource;
    }

    public static class CsvData {

        private final Map<String, Integer> name2Col = new ConcurrentHashMap<>();
        private boolean init = false;

        /**
         * 初始化行数据
         */
        public void initCol(String[] single) {
            for (int i = 1; i < single.length; i++) {
                String key = single[i];
                if (StringUtils.isEmpty(key)) {
                    continue;
                }
                name2Col.put(key, i);
            }
            init = true;
        }

        public boolean isInit() {
            return init;
        }
    }


}
