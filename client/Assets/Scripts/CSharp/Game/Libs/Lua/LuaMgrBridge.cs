using LuaInterface;
using UnityEngine;

public class LuaMgrBridge : SingletonMonoBehaviour<LuaMgrBridge>
{
    private LuaState m_lua;

    private LuaFunction m_newByPath;
    private LuaFunction m_getSubPrefab;

    public LuaState LuaState
    {
        get { return m_lua; }
    }
    private bool isStarted = false;
    
    public LuaTable ResetStart(string fileName, string param = null)
    {
        new LuaResLoader();
        
        m_lua = new LuaState();
        m_lua.LuaSetTop(0);
        LuaBinder.Bind(m_lua);
        DelegateFactory.Register();
        m_lua.Start();

        if (!string.IsNullOrEmpty(param))
        {
//            m_lua["LuaSingletonParam"] = param;
        }

        LuaTable module = null;

        if (!string.IsNullOrEmpty(fileName))
        {
            if (fileName.EndsWith(".lua"))
                fileName = fileName.Substring(0, fileName.Length - 4);

            fileName = fileName.Replace("/", ".");
            module = m_lua.Require<LuaTable>(fileName);

        }

        isStarted = true;

        return module;
    }


    public LuaTable NewByPath(string path)
    {
        if (m_newByPath == null)
        {
            m_newByPath = this.LuaState.GetFunction("newByPath");
        }

        return m_newByPath.Invoke<string, LuaTable>(path);
    }
}