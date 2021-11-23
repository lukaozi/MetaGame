using System;
using UnityEditor;
using UnityEngine;


public class FileItem
{
    public bool isDirectoryInfo = false;

    public string resName;

    public string hardTotalPath;

    public string assetPath;
}


public class FindObjectWindow : EditorWindow
{
    private static float Width_Window = 600;
    
    private static float Height_Window = 600;

    private Action<FileItem> onSureClickListen;

    public void SetOnWindowSureClickListen(Action<FileItem> func)
    {
        onSureClickListen = func;
    }
    
    public static FindObjectWindow OpenWindow(string findType, string extendName = null)
    {
        FindObjectWindow window = GetWindow(typeof(FindObjectWindow)) as FindObjectWindow;
        window.position = new Rect((Screen.currentResolution.width - Width_Window) * 0.5f, (Screen.currentResolution.height - Height_Window) * 0.5f, Width_Window, Height_Window);
        window.Show();
        window.SetSearchPath(findType, extendName);
        window.minSize = new Vector2(Width_Window, Height_Window);
        return window;
    }

    public void SetSearchPath(string type, string extendName)
    {
        //needtodo
    }
    
}