using System;
using System.Collections.Generic;
using UnityEngine;

public class SimpleScheduleSequence : SimpleSchedule
{
    List<SimpleSchedule> m_sequence = new List<SimpleSchedule>();

    private SimpleSchedule m_curItem = null;

    public int CurItemIdx
    {
        get { return m_curItem == null ? -1 : m_sequence.IndexOf(m_curItem); }
    }

    public SimpleSchedule Add(SimpleSchedule item)
    {
        return Insert(m_sequence.Count, item);
    }

    public SimpleSchedule Insert(int idx, SimpleSchedule item)
    {
        if (IsOver)
        {
            Debug.LogError("isover");
            return null;
        }

        if (IsRunning && idx <= CurItemIdx)
        {
            Debug.LogError("不能插入太前");
            Error();
            return null;
        }
        m_sequence.Insert(idx, item);
        return item;
    }

    public void RemoveAt(int idx)
    {
        if (IsOver)
        {
            Debug.LogError("isover");
            return;
        }
        if (IsRunning && idx <= CurItemIdx)
        {
            Debug.LogError("不能移除正在做的任务");
            Error();
            return;
        }
        m_sequence.RemoveAt(idx);
    }

    protected override void OnRun()
    {
        OnPreOver(enScheduleOverType.normalOver, null);
    }

    void OnPreOver(enScheduleOverType overType, SimpleSchedule item)
    {
        if (IsOver)
        {
            return;
        }

        if (m_curItem != null && m_curItem != item)
        {
            throw new Exception("逻辑错误");
        }

        if (overType != enScheduleOverType.normalOver)
        {
            Debug.Log("errror");
            return;
        }

        var idx = m_sequence.IndexOf(m_curItem);
        if (m_curItem != null && idx == -1)
        {
            throw new Exception("errror");
        }

        if ((idx + 1) < m_sequence.Count)
        {
            m_curItem = m_sequence[idx + 1];
            m_curItem.AddOverListener(OnPreOver);
            m_curItem.Run();
        }
        else
        {
            this.Over();
            return;
        }
        
    }


    protected override void OnCancel()
    {
        if (m_curItem!= null && m_curItem.IsRunning)
        {
            m_curItem.Cancel();
        }
    }

    protected override void OnTimeout()
    {
        if (m_curItem!= null && m_curItem.IsRunning)
        {
            m_curItem.Cancel();
        }
    }
}