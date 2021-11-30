package meta.springsupport;

/**
 * @author: AK-47
 * @date: 2021/11/24
 * <p>
 * spring容器启动顺序 数值越小越优先
 */
public enum OrderNum {
    CSV(1),
    DB(2),
    EVENT_BUS(3),
    SERVICE(4),
    ;

    int value;

    OrderNum(int value) {
        this.value = value;
    }

    public int getOrder() {
        return Integer.MAX_VALUE - 100 + value;
    }
}
