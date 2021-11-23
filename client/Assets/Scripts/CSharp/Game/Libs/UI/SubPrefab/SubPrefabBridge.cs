using System;
using LuaInterface;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class SubPrefabBridge : MonoBehaviour
{
    public string subPrefabPath;
    
    private GameObject _subGo;

    public GameObject SubGo
    {
        get
        {
            if (_subGo == null)
            {
                Awake();
            }

            return _subGo;
        }
    }

    [NoToLua]
    public void Awake()
    {
        
    }
    
    
    [NoToLua]
    public void DoAllField(Action<LuaSerializebleField> onDo)
    {
        //needtodo
    }

    [NoToLua] public const string subPrefabRootPath = "Assets/Resources/UI/SubPrefabs/";

}