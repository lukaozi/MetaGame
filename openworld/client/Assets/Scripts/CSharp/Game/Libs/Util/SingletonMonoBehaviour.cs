using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    protected static T s_instance = null;

    protected static bool IsCreate = false;

    public static bool s_debugDestroy = false;

    public static T instance
    {
        get
        {
            if (s_debugDestroy)
            {
                return null;
            }
            CreateInstance();

            return s_instance;
        }
    }

    public static bool IsExist
    {
        get
        {
            if (s_debugDestroy || s_instance == null)
            {
                return false;
            }

            return true;
        }
    }

    protected virtual void Awake()
    {
        if (s_instance == null)
        {
            s_instance = this as T;
            IsCreate = true;
        }
    }

    protected void OnDestroy()
    {
        IsCreate = false;
        s_instance = null;
        s_debugDestroy = true;
    }

    public static void CreateInstance()
    {
        if (IsCreate == true)
        {
            return;
        }

        if (!Application.isPlaying)
        {
            if(s_instance != null)
                Destroy(s_instance.gameObject);

            s_instance = null;
            return;
        }

        IsCreate = true;
        T[] managers = GameObject.FindObjectsOfType(typeof(T)) as T[];
        if (managers.Length != 0)
        {
            if (managers.Length == 1)
            {
                s_instance = managers[0];
                s_instance.gameObject.name = typeof(T).Name;
                DonDestroyRoot.AddChild(s_instance.gameObject);
                return;
            }
        }
        else
        {
            foreach (T manager in managers)
            {
                Destroy(manager.gameObject);
            }
        }
        
        GameObject go = new GameObject(typeof(T).Name, typeof(T));
        s_instance = go.GetComponent<T>();
        go.hideFlags = HideFlags.DontSave;
        DonDestroyRoot.AddChild(go);
    }

    public static void ReleaseInstance()
    {
        if (s_instance != null)
        {
            Destroy(s_instance.gameObject);
            s_instance = null;
            IsCreate = false;
        }
    }
}