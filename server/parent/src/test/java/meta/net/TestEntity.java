package meta.net;

import meta.database.ChangeSupportEntity;

import java.util.Map;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */
public class TestEntity extends ChangeSupportEntity<Integer> {

    private int id;

    private String name;

    private int age;

    @Override
    public Integer getId() {
        return id;
    }


    public static void main(String[] args) {
        TestEntity testEntity = new TestEntity();
        Map<String, String> changeValueMap = testEntity.getChangeValueMap();
        testEntity.id = 2;
        Map<String, String> changeValueMap2 = testEntity.getChangeValueMap();
        Map<String, String> changeValueMap3 = testEntity.getChangeValueMap();
        testEntity.name = "nihao";
        Map<String, String> changeValueMap4 = testEntity.getChangeValueMap();
    }
}
