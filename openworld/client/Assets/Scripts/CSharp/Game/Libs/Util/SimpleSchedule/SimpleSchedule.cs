using System;
using System.Timers;
using UnityEditor.PackageManager;
using UnityEngine;

public enum enScheduleOverType
{
    none,
    normalOver,
    error,
    cancel,
    timeout,
}

public abstract class SimpleSchedule
{
    private bool m_hasRun = false;
    enScheduleOverType m_overType = enScheduleOverType.none;
    Action<enScheduleOverType, SimpleSchedule> m_onOver;

    public bool IsRunning
    {
        get { return m_hasRun && m_overType == enScheduleOverType.none; }
    }

    public bool IsOver
    {
        get { return m_overType != enScheduleOverType.none; }
    }

    protected abstract void OnRun();

    protected virtual void OnCancel()
    {
    }

    protected virtual void OnTimeout()
    {
    }

    public static SimpleScheduleSequence CreateSequence()
    {
        return new SimpleScheduleSequence();
    }

    public static SimpleScheduleParallel CreateParallel()
    {
        return new SimpleScheduleParallel();
    }

    public void AddOverListener(Action<enScheduleOverType, SimpleSchedule> onOver)
    {
        if (IsOver)
        {
            throw new Exception("结束了");
        }

        if (m_onOver == null)
        {
            m_onOver = onOver;
        }
        else
        {
            m_onOver += m_onOver;
        }
    }

    public void RemoveOverListener(Action<enScheduleOverType, SimpleSchedule> onOver)
    {
        if (m_onOver != null)
        {
            m_onOver -= onOver;
        }
    }

    public void Run()
    {
        if(m_hasRun)
            throw new Exception("has run");

        if (m_overType == enScheduleOverType.cancel)
        {
            Debug.Log("HAS CANCEL");
            return;
        }
        else if (m_overType != enScheduleOverType.none)
        {
            throw new Exception(" is not none");
        }

        m_hasRun = true;
        try
        {
            OnRun();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            if (m_overType == enScheduleOverType.none)
            {
                Error();
            }
        }
    }

    void OverInternal(enScheduleOverType over)
    {
        if(!m_hasRun)
            throw new Exception("not start");
        if(IsOver)
            throw new Exception("isover = true");

        m_overType = over;

        if (m_onOver != null)
            m_onOver(m_overType, this);
    }

    protected void Over()
    {
        if (IsOver)
        {
            Debug.LogError("has over");
            return;
        }
        OverInternal(enScheduleOverType.normalOver);
    }

    protected void Error()
    {
        if (IsOver)
        {
            Debug.LogError("has over");
            return;
        }
        OverInternal(enScheduleOverType.error);
    }
    
    public void Cancel()
    {
        if (IsOver)
        {
            Debug.LogError("has over");
            return;
        }

        if (!m_hasRun)
        {
            m_overType = enScheduleOverType.cancel;
        }
        else
        {
            OverInternal(enScheduleOverType.cancel);
            OnCancel();
        }
        
    }

    public void SetTimeout(float duration)
    {
        if(m_hasRun)
            throw new Exception("开始后不能调用SetTimeout");
        
        //needtodo
    }

    void OnTimeout(object param, Timer timer)
    {
        if (IsOver)
        {
            return;
        }
        OverInternal(enScheduleOverType.timeout);
        OnTimeout();
    }
}