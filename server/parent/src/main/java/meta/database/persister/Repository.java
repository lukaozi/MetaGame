package meta.database.persister;

import java.util.List;
import java.util.function.Predicate;
import java.util.function.Supplier;

public interface Repository<PK, E> {

    E add(E obj);

    E save(E obj);

    E get(PK primaryKey,Class<? extends E> clazz);

    E getOrCreate(PK primaryKey, Supplier<E> supplier);

    void remove(E obj);

    List<E> listAll();

    List<E> listAll(Predicate<E> filter);
}
