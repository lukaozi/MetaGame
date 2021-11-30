package meta.database.mongo;

import meta.database.persister.ChangeSupportEntity;
import meta.database.persister.ChangeSupportRepository;
import meta.database.persister.RepositoryGetter;
import org.springframework.data.mongodb.core.MongoTemplate;

import java.util.List;
import java.util.function.Predicate;
import java.util.function.Supplier;


/**
 * @author lushengkao vip8
 * <p>
 * mongo 数据库操作
 * 2019/6/15 15:10
 */
public class MongoRepository<PK, E extends ChangeSupportEntity<PK>> implements ChangeSupportRepository<PK, E> {

    private MongoTemplate getTemplate() {
        return RepositoryGetter.getMongoTemplate();
    }

    @Override
    public E add(E obj) {
        return getTemplate().insert(obj);
    }

    @Override
    public E save(E obj) {
        return getTemplate().save(obj);
    }

    @Override
    public E get(PK primaryKey, Class<? extends E> clazz) {
        return getTemplate().findById(primaryKey, clazz);
    }

    @Override
    public E changeSave(E obj) {
        return (E) getTemplate().findById(obj.getId(), obj.getClass());
    }


    @Override
    public E getOrCreate(PK primaryKey, Supplier<E> supplier) {
        return null;
    }

    @Override
    public void remove(E obj) {
        getTemplate().remove(obj);
    }

    @Override
    public List<E> listAll() {
        return null;
    }

    @Override
    public List<E> listAll(Predicate<E> filter) {
        return null;
    }
}
