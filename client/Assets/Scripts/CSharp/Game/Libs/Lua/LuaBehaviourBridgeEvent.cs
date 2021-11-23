using System.Collections.Generic;
using UnityEngine;


public partial class LuaBehaviourBridge : MonoBehaviour
{
    public class InnerListener
    {
        public int listenerId;

        public int eventType;

        public GameObject target;
    }
    
    private Dictionary<int , InnerListener> m_listeners = new Dictionary<int, InnerListener>();

    //供lua调用
    public void AddListener(int listenerId, int eventType, UnityEngine.Object target)
    {
        if (target == null)
        {
            Debug.LogError("target 不能为空");
            return;
        }

        GameObject targetGo = null;
        if (target is GameObject) targetGo = (GameObject) target;
        else
        {
            targetGo = ((Component) target).gameObject;
        }
        
        m_listeners[listenerId] = new InnerListener()
        {
            listenerId =  listenerId,
            eventType = eventType,
            target = targetGo
        };

        switch (eventType)
        {
            case LuaBehaviourBridgeEventType.OnEnable:
                targetGo.AddComponentIfNoExist<EnableListener>().onEnableEvent.Add(listenerId, OnEvent);
                break;
        }
    }
    
    public void RemoveAllListener()
    {
        //needtodo
    }

    void OnEvent(int listenerId)
    {
        if (m_luaBehavior.GetLuaState() == null)
        {
            return;
        }
        m_luaBehavior.Call("OnCSharpEvent", m_luaBehavior, listenerId);
    }
}