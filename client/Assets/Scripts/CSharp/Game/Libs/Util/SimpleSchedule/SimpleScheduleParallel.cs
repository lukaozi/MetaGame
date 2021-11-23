using System.Collections.Generic;
using UnityEngine;


public class SimpleScheduleParallel : SimpleSchedule
{
    List<SimpleSchedule> m_items = new List<SimpleSchedule>();

    public SimpleSchedule Add(SimpleSchedule item)
    {
        if (IsOver)
        {
            Debug.LogError("is over");
            return null;
        }

        if (IsRunning)
        {
            Debug.LogError("is running");
            Error();
            return null;
        }
        m_items.Add(item);
        return item;
    }
    
    protected override void OnRun()
    {
        foreach (var item in m_items)
        {
            item.AddOverListener(OnItemOver);
            item.Run();
            if(this.IsOver)
                break;
        }
    }

    void CancelAllItems()
    {
        foreach (var item in m_items)
        {
            item.Cancel();
        }
    }

    void OnItemOver(enScheduleOverType overType, SimpleSchedule item)
    {
        if (IsOver)
        {
            return;
        }

        if (overType != enScheduleOverType.normalOver)
        {
            Debug.Log("子节点非正常结束");
            Cancel();
            CancelAllItems();
            return;
        }

        var overCount = 0;
        foreach (var item2 in m_items)
        {
            if (item2.IsOver)
                ++overCount;
        }
        if(m_items.Count == overCount)
            Over();
    }

    protected override void OnCancel()
    {
        CancelAllItems();
    }

    protected override void OnTimeout()
    {
        CancelAllItems();
    }
    
}