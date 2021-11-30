package meta.database.persister;

import meta.springsupport.Application;
import org.springframework.data.mongodb.core.MongoTemplate;

/**
 * @author: AK-47
 * @date: 2021/11/29
 */
public class RepositoryGetter {

    private static volatile MongoTemplate template;

    public static MongoTemplate getMongoTemplate() {
        if (template == null) {
            synchronized (RepositoryGetter.class) {
                if (template == null) {
                    template = Application.getApplication().getBean(MongoTemplate.class);
                }
            }
        }
        return template;
    }
}
