package meta.net;

import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.beans.PropertyChangeSupport;

/**
 * @author: AK-47
 * @date: 2021/11/25
 */
public class TestDao {

    protected final PropertyChangeSupport support;

    private String str;

    public TestDao(String str) {
        this.str = str;
        support = new PropertyChangeSupport(this);
    }

    public String getStr() {
        return str;
    }

    public void setStr(String str) {
        support.firePropertyChange("str", this.str, str);
        this.str = str;
    }

    public void addPropertyChangeListener(PropertyChangeListener listener) {
        support.addPropertyChangeListener(listener);
    }


    public static void main(String[] args) {
        TestDao test = new TestDao("old");
        test.addPropertyChangeListener(new PropertyChangeListener1());
        test.setStr("new");
    }

    public static class PropertyChangeListener1 implements PropertyChangeListener{

        @Override
        public void propertyChange(PropertyChangeEvent evt) {
            System.out.println( "1: " + evt.getPropertyName() + "  " + evt.getNewValue() + "  " + evt.getOldValue());
        }
    }
}
