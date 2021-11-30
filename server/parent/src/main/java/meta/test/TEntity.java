package meta.test;

import meta.database.persister.ChangeSupportEntity;

/**
 * @author: AK-47
 * @date: 2021/11/29
 */
public class TEntity extends ChangeSupportEntity<Integer> {

    private int id;

    private String name;

    private int age;

    @Override
    public Integer getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public static void main(String[] args) {
    }
}
