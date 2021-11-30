package meta.database.persister;

/**
 * @author: AK-47
 * @date: 2021/11/29
 */
public interface ChangeSupportRepository<PK, E extends ChangeSupportEntity<PK>> extends Repository<PK, E> {

    E changeSave(E obj);
}
