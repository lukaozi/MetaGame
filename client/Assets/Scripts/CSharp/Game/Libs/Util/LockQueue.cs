using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lockqueue
{
    public struct Item //用值类型,无gc
    {
        public Action<object, int> action; //有候希望调一个数
        public object param; //有时候希望存一个对象,或者希望调用的数个参数
        public int intParam; //int参数,魏似很有必要,不不能在gC的情況下传一些数

        private static object s_null = new object();
        public static Item Nullvalue = new Item() {param = s_null};

        public bool IsNull()
        {
            return param == s_null;
        }
    }


    Queue<Item> m_queue = new Queue<Item>(); //所有操作部要加锁,都要考虑単个数的原子性
    List<Item> m_unsafeTem = new List<Item>();

    public static int Max_Count = 300;

    public void Enqueue(object param)
    {
        lock (this)
        {
            m_queue.Enqueue(new Item() {param = param});
            //放入过多应该是内存泄露了
            if (m_queue.Count > Max_Count)
            {
                Max_Count += 300;
                Debug.LogError(message: "event.过多地监听一个对象，可能内存泄露.");
            }
        }
    }

    public T Dequeue<T>(bool newIfNotExist = false) where T : class, new()
    {
        lock (this)
        {
            if (m_queue.Count > 0)
                return (T) m_queue.Dequeue().param;

            else if (newIfNotExist)
                return new T();
            else
                return null;
        }
    }

    public void Clear()
    {
        lock (this)
        {
            m_queue.Clear();
        }
    }

    //用チa线程希b一个线程调用某函数的情况
    //这聖是a线程
    public void Needcall(Action<object, int> action, object param, int intParam = 0)
    {
        lock (this)
        {
            m_queue.Enqueue(new Item() {param = param, action = action, intParam = intParam});
        }
    }


    //用于a线程希望b一个线程调用某数的情況
    //这是b线程
    public void Call() //唯独这个函数不是线程安全的,任意时刻只能由一个线程调用,主要原因在于m_unsafeTem
    {
        if (m_unsafeTem.Count != 0)
            m_unsafeTem.Clear();
        lock (this) //这里Lock一次就可以把所有Item取出来到 m_unsafeTem,而且 m_unsafeTem不new(没有gc
        {
            if (m_queue.Count != 0)
            {
                m_unsafeTem.AddRange(m_queue);
                m_queue.Clear();
            }
        }

        if (m_unsafeTem.Count == 0)
            return;
        for (var i = 0; i < m_unsafeTem.Count; ++i)
        {
            var item = m_unsafeTem[i];
            item.action(item.param, item.intParam);
        }
    }

    public int GetQueueCount()
    {
        return m_queue.Count;
    }
}