using System;
using UnityEngine;

public class AutoChangeBkColor : IDisposable
{
    private Color PreviousColor { get; set; }

    public AutoChangeBkColor(Color newColor)
    {
        PreviousColor = GUI.backgroundColor;
        GUI.backgroundColor = newColor;
    }
    
    public void Dispose()
    {
        GUI.backgroundColor = PreviousColor;
    }
}