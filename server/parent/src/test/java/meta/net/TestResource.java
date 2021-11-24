package meta.net;

import meta.csv.CsvName;
import meta.csv.Id;

/**
 * @author: AK-47
 * @date: 2021/11/24
 */
@CsvName
public class TestResource {

    @Id
    private int id;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
