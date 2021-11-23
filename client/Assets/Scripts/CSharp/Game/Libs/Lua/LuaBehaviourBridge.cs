using System;
using System.Collections.Generic;
using LuaInterface;
using UnityEngine;


public partial class LuaBehaviourBridge : MonoBehaviour
{
    [NoToLua] public GoField[] m_goFields;
    [NoToLua] public BoolField[] m_boolFields;
    [NoToLua] public IntField[] m_intFields;
    [NoToLua] public StringField[] m_stringFields;
    [NoToLua] public FloatField[] m_floatFields;
    [NoToLua] public Vector2Field[] m_v2Fields;
    [NoToLua] public Vector3Field[] m_v3Fields;
    [NoToLua] public Vector4Field[] m_v4Fields;
    [NoToLua] public QuaternionField[] m_quaternionFields;
    [NoToLua] public ColorField[] m_colorFields;
    [NoToLua] public AssetField[] m_assetFields;

    [NoToLua] public string m_luaFileName = "";


    private LuaTable m_luaBehavior;
    
    public LuaTable LuaBehavior
    {
        get { 
            this.Awake();
            return m_luaBehavior;
        }
    }

    private bool m_isFirstEnble = true;

    public static LuaTable RuntimerCreate(string fileName = null, string name = null)
    {
        if (string.IsNullOrEmpty(fileName))
            fileName = "Game/Libs/Lua/LuaBehaviour";
        if (string.IsNullOrEmpty(name))
            name = fileName;
        var go = new GameObject(name, typeof(LuaBehaviourBridge));
        var luaBehaviourBridge = go.GetComponent<LuaBehaviourBridge>();
        luaBehaviourBridge.m_luaFileName = fileName;
        luaBehaviourBridge.Awake();
        return luaBehaviourBridge.LuaBehavior;
    }


    [NoToLua]
    public void Awake()
    {
        bool isAlreadyClose = LuaMgrBridge.s_debugDestroy || !LuaMgrBridge.IsExist ||
                              LuaMgrBridge.instance.LuaState == null;

        if (string.IsNullOrEmpty(m_luaFileName) || isAlreadyClose || m_luaBehavior != null)
            return;

        m_luaBehavior = LuaMgrBridge.instance.NewByPath(m_luaFileName);
        m_luaBehavior.AddRef();


        Dictionary<string, LuaSerializebleField> rewrites = null;
        var p = transform.parent;
        if (p != null)
        {
            SubPrefabBridge parentSunPrefab;
            if (p.TryGetComponent<SubPrefabBridge>(out parentSunPrefab))
            {
                parentSunPrefab.DoAllField(f =>
                {
                    if (rewrites == null)
                        rewrites = new Dictionary<string, LuaSerializebleField>();

                    rewrites.Add(f.name, f);
                });
            }

        }

        m_luaBehavior.SetTable("_luaBehaviourBridge", this);
        DoAllField(f =>
        {
            object value = null;
            LuaSerializebleField f2 = null;
            if (rewrites != null && rewrites.TryGetValue(f.name, out f2))
            {
                f = f2;
            }

            value = f.GetValue();

            if (value is LuaBehaviourBridge)
            {
                var bridge = value as LuaBehaviourBridge;
                bridge.Awake();
                m_luaBehavior.SetTable(f.name, (bridge).LuaBehavior);
            }
            else if (value is SubPrefabBridge)
            {
                var bridge = value as SubPrefabBridge;
                bridge.Awake();
                if (bridge.SubGo != null)
                {
                    var tem = bridge.SubGo.GetComponent<LuaBehaviourBridge>();
                    if (tem != null)
                    {
                        tem.Awake();
                        m_luaBehavior.SetTable(f.name, tem.LuaBehavior);
                    }

                }
            }
            else
            {
                f.SetValue(m_luaBehavior);
            }


        });
        //回调lua
        m_luaBehavior.Call("OnSerial", m_luaBehavior);
    }

    void OnEnable()
    {
        if(m_luaBehavior != null)
            m_luaBehavior.Call("OnCSharpEnable", m_luaBehavior);
    }

    private void OnDisable()
    {
        if(m_luaBehavior != null)
            m_luaBehavior.Call("OnCSharpDisable", m_luaBehavior);
    }
    
    private void OnDestroy()
    {
        if (m_luaBehavior != null)
        {
            m_luaBehavior.Call("OnGameObjectDestroy", m_luaBehavior);
            if(m_luaBehavior["_luaBehaviourBridge"] != null)
                m_luaBehavior.SetTable("_luaBehaviourBridge", 0);
            
            m_luaBehavior.Dispose();
            m_luaBehavior = null;
        }

        RemoveAllListener();

    }

    LuaSerializebleField FindField(Predicate<LuaSerializebleField> onDo)
    {
        //needtodo
        return null;
    }


    [NoToLua]
    public void DoAllField(Action<LuaSerializebleField> onDo)
    {
        if (m_goFields != null){for (int i = 0; i < m_goFields.Length; i++){onDo(m_goFields[i]);}}
        
        if (m_intFields != null){for (int i = 0; i < m_intFields.Length; i++){onDo(m_intFields[i]);}}
        if (m_stringFields != null)
        {
            for (int i = 0; i < m_stringFields.Length; i++)
            {
                onDo(m_stringFields[i]);
            }
        }
        if (m_floatFields != null)
        {
            for (int i = 0; i < m_floatFields.Length; i++)
            {
                onDo(m_floatFields[i]);
            }
        }
        
        if (m_boolFields != null)
        {
            for (int i = 0; i < m_boolFields.Length; i++)
            {
                onDo(m_boolFields[i]);
            }
        }
        
        if (m_v2Fields != null)
        {
            for (int i = 0; i < m_v2Fields.Length; i++)
            {
                onDo(m_v2Fields[i]);
            }
        }
        if (m_v3Fields != null)
        {
            for (int i = 0; i < m_v3Fields.Length; i++)
            {
                onDo(m_v3Fields[i]);
            }
        }
        if (m_v4Fields != null)
        {
            for (int i = 0; i < m_v4Fields.Length; i++)
            {
                onDo(m_v4Fields[i]);
            }
        }
        if (m_quaternionFields != null)
        {
            for (int i = 0; i < m_quaternionFields.Length; i++)
            {
                onDo(m_quaternionFields[i]);
            }
        }
        if (m_colorFields != null)
        {
            for (int i = 0; i < m_colorFields.Length; i++)
            {
                onDo(m_colorFields[i]);
            }
        }
        if (m_assetFields != null)
        {
            for (int i = 0; i < m_assetFields.Length; i++)
            {
                onDo(m_assetFields[i]);
            }
        }
            
    }




}