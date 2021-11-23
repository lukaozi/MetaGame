using System;
using System.Collections.Generic;

public class EventDispatcher
{
    public delegate void onInvoke();
    public delegate void onInvoke2(object invokerParam);
    public delegate void onInvoke3(Listener listener, object invokerParam);
    
    public enum EnFunType
    {
        Empty,
        InvokeParam,
        InvokeListenerParam,
    }

    public class Listener
    {
        public Delegate fun;
        public EnFunType funType;
        public object listenerParam;
        public bool once;
        public EventDispatcher e;
        public LinkedListNode<Listener> node;

        public void Off()
        {
            //needtodo
        }
    }
    
}