using System;
using System.Collections.Generic;
using UnityEngine;


public class ListenerDelegate
{
    public List<int> listenerIds;
    public List<Action<int>> callbacks;

    public void Add(int listenerId, Action<int> callback)
    {
        if (listenerIds == null)
        {
            listenerIds = new List<int>(1);
            callbacks = new List<Action<int>>(1);
        }

        listenerIds.Add(listenerId);
        callbacks.Add(callback);
    }

    public void Remove(int listenerId)
    {
        if (listenerIds == null) return;
        var idx = listenerIds.IndexOf(listenerId);
        if (idx == -1) return;
        listenerIds.RemoveAt(idx);
        callbacks.RemoveAt(idx);
    }

    public void Invoke()
    {
        if (listenerIds == null) return;
        for (int i = listenerIds.Count - 1; i >= 0; --i)
        {
            if (i < callbacks.Count && i < listenerIds.Count)
                callbacks[i](listenerIds[i]);
        }
    }

}

public class ListenerDelegate<T>
{
    public List<int> listenerIds;
    public List<Action<int, T>> callbacks;

    public void Add(int listenerId, Action<int, T> callback)
    {
        if (listenerIds == null)
        {
            listenerIds = new List<int>(1);
            callbacks = new List<Action<int, T>>(1);
        }
        
        listenerIds.Add(listenerId);
        callbacks.Add(callback);
    }
    
    
    public void Remove(int listenerId)
    {
        if (listenerIds == null) return;
        var idx = listenerIds.IndexOf(listenerId);
        if (idx == -1) return;
        listenerIds.RemoveAt(idx);
        callbacks.RemoveAt(idx);
    }

    public void Invoke(T p)
    {
        if (listenerIds == null) return;
        for (int i = listenerIds.Count - 1; i >= 0; --i)
        {
            if (i < callbacks.Count && i < listenerIds.Count)
                callbacks[i](listenerIds[i], p);
        }
    }
}

public class ListenerDelegate<T1, T2>
{
    public List<int> listenerIds;
    public List<Action<int, T1, T2>> callbacks;

    public void Add(int listenerId, Action<int, T1, T2> callback)
    {
        if (listenerIds == null)
        {
            listenerIds = new List<int>(1);
            callbacks = new List<Action<int, T1, T2>>(1);
        }
        
        listenerIds.Add(listenerId);
        callbacks.Add(callback);
    }
    
    
    public void Remove(int listenerId)
    {
        if (listenerIds == null) return;
        var idx = listenerIds.IndexOf(listenerId);
        if (idx == -1) return;
        listenerIds.RemoveAt(idx);
        callbacks.RemoveAt(idx);
    }

    public void Invoke(T1 p1, T2 p2)
    {
        if (listenerIds == null) return;
        for (int i = listenerIds.Count - 1; i >= 0; --i)
        {
            if (i < callbacks.Count && i < listenerIds.Count)
                callbacks[i](listenerIds[i], p1, p2);
        }
    }

}


public class ListenerDelegate<T1, T2, T3>
{
    public List<int> listenerIds;
    public List<Action<int, T1, T2, T3>> callbacks;

    public void Add(int listenerId, Action<int, T1, T2, T3> callback)
    {
        if (listenerIds == null)
        {
            listenerIds = new List<int>(1);
            callbacks = new List<Action<int, T1, T2, T3>>(1);
        }
        
        listenerIds.Add(listenerId);
        callbacks.Add(callback);
    }
    
    
    public void Remove(int listenerId)
    {
        if (listenerIds == null) return;
        var idx = listenerIds.IndexOf(listenerId);
        if (idx == -1) return;
        listenerIds.RemoveAt(idx);
        callbacks.RemoveAt(idx);
    }

    public void Invoke(T1 p1, T2 p2, T3 p3)
    {
        if (listenerIds == null) return;
        for (int i = listenerIds.Count - 1; i >= 0; --i)
        {
            if (i < callbacks.Count && i < listenerIds.Count)
                callbacks[i](listenerIds[i], p1, p2, p3);
        }
    }
}