using UnityEngine;

public static class ClassExtend
{
    public static T AddComponentIfNoExist<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null)
        {
            return go.AddComponent<T>();
        }
        else
        {
            return comp;
        }
    }
}