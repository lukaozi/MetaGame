import meta.Starter;
import meta.TestEntity;
import meta.springsupport.Application;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.context.ApplicationContext;
import org.springframework.data.mongodb.core.MongoTemplate;
import org.springframework.data.mongodb.core.query.Criteria;
import org.springframework.data.mongodb.core.query.Query;
import org.springframework.data.mongodb.core.query.Update;
import org.springframework.test.context.junit4.SpringRunner;

import java.util.Map;


/**
 * @author: AK-47
 * @date: 2021/11/25
 */
@RunWith(SpringRunner.class)
@SpringBootTest(classes= Starter.class)
public class TestApplication {

    @Autowired
    MongoTemplate template;

    @Test
    public void dbTest() {
        ApplicationContext application = Application.getApplication();
        MongoTemplate template = application.getBean(MongoTemplate.class);
        TestEntity testEntity = new TestEntity();
        testEntity.setId(2);
        testEntity.setName("nihao");
        testEntity.setAge(22);
        template.save(testEntity);
        testEntity.getChangeValueMap();
        testEntity.setName("wohao");
        testEntity.setAge(223);
        Map<String, Object> map = testEntity.getChangeValueMap();
        Query query = new Query(Criteria.where("id").is(testEntity.getId()));
        Update update = new Update();
        for (Map.Entry<String, Object> entry : map.entrySet()) {
            String key = entry.getKey();
            Object value = entry.getValue();
            update.set(key,value);
        }
        template.updateFirst(query,update,TestEntity.class);
    }
}
