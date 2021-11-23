using System.Collections.Generic;

public class TimeMgr:SingletonMonoBehaviour<TimeMgr>
{
    public delegate void onInvoke(object param, Timer timer);
    
    public enum EnTimerType
    {
        Loop,
        UnscaledLoop,
        FrameLoop,
        FrameLateLoop,
        Later,
    }

    public class Timer
    {
        public object owner { get; private set; }
        public onInvoke fun { get; private set;}
        
        public EnTimerType timerType { get; private set; }
        public float duration { get; private set; }
        public int loop { get; set; }
        
        public object param { get; set; }
        
        public LinkedListNode<Timer> node { get; set; }
        public LinkedListNode<Timer> node2 { get; set; }//双索引

        public float time;

        public Timer(object owner, onInvoke fun, EnTimerType timerType, float duration, int loop, object param)
        {
            this.owner = owner;
            this.fun = fun;
            this.timerType = timerType;
            this.duration = duration;
            this.loop = loop;
            this.param = param;
            this.node = new LinkedListNode<Timer>(this);
            this.node2 = new LinkedListNode<Timer>(this);
        }


        public void Remove()
        {
            TimeMgr.instance.Remove(this);
        }
        
    }
    
    private Dictionary<object, LinkedList<Timer>> m_timers = new Dictionary<object, LinkedList<Timer>>();
    private LinkedList<Timer> m_timerByList = new LinkedList<Timer>();
        
    private object m_global = new object();

    public Timer Once(object owner, onInvoke fun, float duration, object param = null,
        bool offBefore = true)
    {
        return Loop(owner, fun, duration, 1, param, offBefore);
    }
    
    public Timer Loop(object owner, onInvoke fun, float duration, int loop = -1, object param = null,
        bool offBefore = true)
    {
        return AddTimer(owner, fun, EnTimerType.Loop, duration, loop, param, offBefore);
    }
    
    public Timer FrameLoop(object owner, onInvoke fun, int duration = 1, int loop = -1, object param = null,
        bool offBefore = true)
    {
        return AddTimer(owner, fun, EnTimerType.FrameLoop, duration, loop, param, offBefore);
    }

    public void Remove(object owner, onInvoke fun)
    {
        //needtodo
    }

    public void Remove(Timer t)
    {
        //needtodo
    }

    public Timer AddTimer(object owner, onInvoke fun, EnTimerType timerType, float duration, int loop = -1, object param = null,
        bool offBefore = true)
    {
        //needtodo
        return null;
    }
    
}