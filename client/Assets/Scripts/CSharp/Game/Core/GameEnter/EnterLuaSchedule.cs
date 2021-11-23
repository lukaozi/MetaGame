using System;
using System.Collections;
using Core.Managers;
using LuaInterface;
using UnityEngine;

public class EnterLuaSchedule : SimpleSchedule
{
    private static Action m_onGameInitFinish;
    private static LuaTable m_game;
    public static bool gameLuaFinish = false;

    protected override void OnRun()
    {
        m_onGameInitFinish = null;
        m_game = null;
        CoroutineManager.StartCoroutine(StartLua());
    }
    
    //开启lua虚拟机
    IEnumerator StartLua()
    {
        var steptime1 = System.DateTime.Now;
        yield return CoroutineManager.WaitHandler((cb) =>
        {
            m_onGameInitFinish = cb;
            string param = "";
            Debug.Log("start lua");
            LuaMgrBridge.instance.ResetStart("Game/Game.lua", param);

        });

       ///
    }

    public static void OnGameInitFinish(LuaTable game)
    {
        m_game = game;
        if (null != m_onGameInitFinish)
            m_onGameInitFinish();

        gameLuaFinish = true;
        m_onGameInitFinish = null;
    }
}