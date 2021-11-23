using System;
using UnityEngine;


public class EnableListener : MonoBehaviour
{
    public ListenerDelegate onEnableEvent = new ListenerDelegate();
    public ListenerDelegate onDisableEvent = new ListenerDelegate();

    void OnEnable()
    {
        #if UNITY_EDITOR
        this.hideFlags = HideFlags.DontSave;
        #endif
        onEnableEvent.Invoke();
    }

    private void OnDisable()
    {
        onDisableEvent.Invoke();
    }
}