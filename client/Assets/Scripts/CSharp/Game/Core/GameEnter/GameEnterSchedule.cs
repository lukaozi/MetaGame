using UnityEngine;

public class GameEnterSchedule : SimpleScheduleParallel
{
    public GameEnterSchedule()
    {
        HotUpdateSchedule hotUpdateSchedule = new HotUpdateSchedule();
        this.Add(hotUpdateSchedule);
        
        EnterLuaSchedule luaSchedule = new EnterLuaSchedule();
        hotUpdateSchedule.Add(luaSchedule);
    }
        
}