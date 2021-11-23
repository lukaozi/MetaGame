using System;
using UnityEngine;


public class AutoBeginVertical : IDisposable
{
    public AutoBeginVertical()
    {
        GUILayout.BeginVertical();
    }

    public AutoBeginVertical(params GUILayoutOption[] layoutOptions)
    {
        GUILayout.BeginVertical(layoutOptions);

    }
    
    public AutoBeginVertical(GUIStyle guiStyle, params GUILayoutOption[] layoutOptions)
    {
        GUILayout.BeginVertical(guiStyle, layoutOptions);

    }

    public void Dispose()
    {
        GUILayout.EndVertical();
    }
    
    
    

}