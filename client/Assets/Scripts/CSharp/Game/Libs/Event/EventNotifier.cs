using System;

public class EventNotifier
{

    public void Invoke(Enum e, object invokeParam = null)
    {
        //needtodo
    }

    public EventDispatcher.Listener On(Enum e, EventDispatcher.onInvoke onInvoke)
    {
        return On(string.Format("{0}_{1}", e.GetType(), e), onInvoke);
    }
    
    public EventDispatcher.Listener On(Enum e, EventDispatcher.onInvoke2 onInvoke)
    {
        return On(string.Format("{0}_{1}", e.GetType(), e), onInvoke);
    }

    public EventDispatcher.Listener On(string eventName, EventDispatcher.onInvoke onInvoke, bool offBefore = true)
    {
        //needtodo
        return null;
    }
    
    public EventDispatcher.Listener On(string eventName, EventDispatcher.onInvoke2 onInvoke, bool offBefore = true)
    {
        //needtodo
        return null;
    }
    
}