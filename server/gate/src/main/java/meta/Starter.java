package meta;

import meta.log.LoggerUtil;
import meta.springsupport.Application;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;

import java.util.Map;

@SpringBootApplication
public class Starter {

    @Autowired
    private MongoTemplate template;

    public static void main(String[] args) throws InterruptedException {
        SpringApplication.run(Starter.class, args);

        LoggerUtil.error("test");

        MongoTemplate bean = Application.getApplication().getBean(MongoTemplate.class);
        TestEntity testEntity = new TestEntity();
        testEntity.setId(3);
        testEntity.getChangeValueMap();
        testEntity.setName("wohao");
        testEntity.setAge(555);
        Map<String, Object> map = testEntity.getChangeValueMap();
        Query query = new Query(Criteria.where("id").is(testEntity.getId()));
        Update update = new Update();
        for (Map.Entry<String, Object> entry : map.entrySet()) {
            String key = entry.getKey();
            Object value = entry.getValue();
            update.set(key,value);
        }
        bean.updateFirst(query,update,TestEntity.class);
    }
}
