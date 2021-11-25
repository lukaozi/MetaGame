package meta.database;


import org.springframework.data.annotation.Transient;

import java.util.Map;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */
public abstract class ChangeSupportEntity<PK> implements Entity<PK> {

    /**
     * 增量更新支持
     */
    @Transient
    private EntityUpdateSupport support = new EntityUpdateSupport(this.getClass());


    public Map<String,Object> getChangeValueMap() {
        return support.getUpdateValueMap(this);
    }
}
