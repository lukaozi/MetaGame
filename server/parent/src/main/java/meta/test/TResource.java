package meta.test;

import meta.csv.Id;

/**
 * @author: AK-47
 * @date: 2021/11/24
 */
public class TResource {

    @Id
    private int id;
    private String name;

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }
}
