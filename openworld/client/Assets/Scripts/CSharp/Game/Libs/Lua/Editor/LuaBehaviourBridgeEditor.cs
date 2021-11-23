using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public enum EnLuaSerializebleType
{
    LuaSerializebleField,
    GoField,
    BoolField,
    IntField,
    FloatField,
    StringField,
    Vector2Field,
    Vector3Field,
    Vector4Field,
    QuaternionField,
    ColorField,
    AssetField,
}


[CustomEditor(typeof(LuaBehaviourBridge))]
public class LuaBehaviourBridgeEditor : Editor
{
    private Object _luaFileAsset;
    
    public Object LuaFileAsset
    {
        get { return _luaFileAsset; }
    }

    private SerializedProperty _luaNameProperty;

    private string _luaFileRootPath = "Assets/Scripts/Lua/";

    private string _luaAssertPath = string.Empty;

    private List<string> _checkNameRepeatList = new List<string>();


    #region 查找lua脚本窗口

    private FindObjectWindow _findLuaWindow = null;

    #endregion

    private LuaSerializedDrawerItem _luaEditorItem;

    public void ShowMoreMenuWindow(CLuaSeriazablePropertyItem item)
    {
        _luaEditorItem.ShowMoreMenuWindow(item);
    }

    void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        if (_findLuaWindow != null)
        {
            _findLuaWindow.Close();
            _findLuaWindow = null;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawLuaScript();
        DrawBinder();
        serializedObject.ApplyModifiedProperties();
    }

    public void ShowMsgWindow(string message)
    {
        EditorUtility.DisplayDialog("message tip", message, "confirm");
    }

    public List<CLuaSeriazablePropertyItem> GetLuaserializedPropertyList()
    {
        if (_luaEditorItem != null)
        {
            return  _luaEditorItem.GetAllPropertyItemList( ) ;

        }
        return null;
    }



    public CLuaSeriazablePropertyItem GetCLuaPropertyItemByName(string name)
    {
        return  _luaEditorItem.GetPropertyItemByName(name) ;
    }

    void Init()
    {
        serializedObject.Update();
        _luaNameProperty = serializedObject.FindProperty("m_luaFileName");

        RefreshLuaFileBind();
        
        _luaEditorItem = new LuaSerializedDrawerItem(this, _luaAssertPath);
    }

    void CombineLuaPath(string luaFileName)
    {
        if (!luaFileName.StartsWith(_luaFileRootPath))
        {
            _luaAssertPath = _luaFileRootPath + luaFileName;
        }

        if (!luaFileName.EndsWith(_luaExtendStr))
        {
            _luaAssertPath += _luaExtendStr;
        }
    }

    private string _tempLuaName = string.Empty;
    private Object _tempLuaAsset = null;
    private string _luaExtendStr = ".lua";

    void DrawLuaScript()
    {
        Rect CurRect = EditorGUILayout.GetControlRect();
        Rect tempRect = new Rect(CurRect.x, CurRect.y, 0, 15);
        tempRect.width = CurRect.width * 0.4f;
        EditorGUI.LabelField(tempRect, "Lua Script");

        tempRect.x = CurRect.width * 0.43f;
        tempRect.width = CurRect.width * 0.47f;
        _tempLuaAsset = EditorGUI.ObjectField(tempRect, _luaFileAsset, typeof(DefaultAsset), false);
        if (Event.current.clickCount == 2)
        {
            AssetDatabase.OpenAsset(_luaFileAsset);
            GUIUtility.ExitGUI();
            return;
        }

        tempRect.x = CurRect.width * 0.90f;
        tempRect.width = CurRect.width * 0.1f;
        if (GUI.Button(tempRect, "选择", EditorStyles.toolbarButton))
        {
            if (_findLuaWindow == null)
            {
                _findLuaWindow = FindObjectWindow.OpenWindow("lua", ".lua");
                _findLuaWindow.SetOnWindowSureClickListen(OnClickFindLuaScripts);
            }
        }
        GUILayout.Space(5f);

        CurRect = EditorGUILayout.GetControlRect();
        tempRect = new Rect(CurRect.x, CurRect.y, 0, 15);
        tempRect.width = CurRect.width * 0.4f;
        EditorGUI.LabelField(tempRect, "Lua文件名");
        tempRect.x = CurRect.width * 0.43f;
        tempRect.width = CurRect.width * 0.57f;
        _tempLuaName = EditorGUI.TextField(tempRect, _luaNameProperty.stringValue);

        if (GUI.changed)
        {
            if (_tempLuaAsset != _luaFileAsset)
            {
                _tempLuaName = AssetDatabase.GetAssetPath(_tempLuaAsset);
                if (!_tempLuaName.StartsWith(_luaFileRootPath))
                {
                    Debug.LogError("错误");
                    return;
                }

                if (!_tempLuaName.EndsWith(_luaExtendStr))
                {
                    Debug.LogError("错误");
                    return;
                }

                _tempLuaName = _tempLuaName.Replace(_luaFileRootPath, string.Empty);
            }

            if (_tempLuaName != _luaNameProperty.stringValue)
            {
                OnLuaScriptsChange(_tempLuaName);
            }
        }
        GUILayout.Space(5f);
    }

    void OnClickFindLuaScripts(FileItem file)
    {
        if (_findLuaWindow == null)
        {
            Debug.LogError("error");
            return;
        }

        if (file.isDirectoryInfo)
        {
            ShowMsgWindow("该文件是目录" + file.resName);
            return;
        }

        if (!file.resName.EndsWith(_luaExtendStr))
        {
            ShowMsgWindow("该文件不是lua文件");
            return;
        }

        string assetPath = file.assetPath;
        if (assetPath.Contains(_luaFileRootPath))
        {
            assetPath = assetPath.Replace(_luaFileRootPath, string.Empty);
        }
        OnLuaScriptsChange(assetPath);
        _findLuaWindow.Close();
        _findLuaWindow = null;

    }

    void OnLuaScriptsChange(string changeLuaName)
    {
        if (changeLuaName.EndsWith(_luaExtendStr))
        {
            changeLuaName = changeLuaName.Replace(_luaExtendStr, string.Empty);
        }

        if (changeLuaName != _luaNameProperty.stringValue)
        {
            _luaNameProperty.stringValue = changeLuaName;
        }

        RefreshLuaFileBind();
        serializedObject.ApplyModifiedProperties();
        Init();
    }

    void RefreshLuaFileBind()
    {
        CombineLuaPath(_luaNameProperty.stringValue);
        _luaFileAsset = AssetDatabase.LoadAssetAtPath(_luaAssertPath, typeof(Object));
    }

    private int curPage = 1;
    private int pageCount = 50;

    void DrawBinder()
    {
        if (DrawHeader("属性绑定", "LuaBehaviourPropertyKey"))
        {
            using (new AutoBeginVertical(EditorStyles.helpBox))
            {
                _luaEditorItem.OnInspectorGUI(false, curPage - 1, pageCount);
                int count = _luaEditorItem.PropertyCount;
                if (count > pageCount)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("上一页", EditorStyles.toolbarButton, GUILayout.MaxWidth(60)))
                    {
                        curPage--;
                    }

                    curPage = EditorGUILayout.IntField(curPage, GUILayout.MaxWidth(40));
                    if(GUILayout.Button("下一页", EditorStyles.toolbarButton, GUILayout.MaxWidth(60)))
                    {
                        curPage++;
                    }

                    if (curPage < 1) curPage = 1;
                    else if (curPage * pageCount == count)
                    {
                        curPage = count / pageCount;
                    }else if (curPage * pageCount > count)
                    {
                        curPage = count / pageCount + 1;
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }
    }

    private bool _isShowDesc = false;

    public bool DrawHeader(string text, string key)
    {
        bool state = EditorPrefs.GetBool(key, true);
        
        if(!state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
        GUILayout.BeginHorizontal(EditorStyles.textArea);
        GUI.changed = false;
        if (state) text = "\u25BC" + text;
        else text = "\u25BA" + text;
        if (!GUILayout.Toggle(true, text, EditorStyles.label, GUILayout.MinWidth(20f))) state = !state;
        
        if(GUI.changed) EditorPrefs.SetBool(key, state);
        GUILayout.Space(2f);

        _isShowDesc = GUILayout.Toggle(_isShowDesc, "显示描述", EditorStyles.toggle, GUILayout.MaxWidth(66));

        if (GUILayout.Button("生成属性", EditorStyles.toolbarButton, GUILayout.MaxWidth(60)))
        {
            OnClickAutoToGenLuaPropertyCode();
            GUIUtility.ExitGUI();
        }
        GUILayout.EndHorizontal();
        GUI.backgroundColor = Color.white;
        if (!state)
        {
            GUILayout.Space(3f);
        }

        return state;
    }



    void OnClickAutoToGenLuaPropertyCode()
    {
        _luaEditorItem.GetLuaCodeAllProperty(_luaAssertPath);
        AssetDatabase.OpenAsset(_luaFileAsset);
    }

    void RefreshLuaFieldBind()
    {
        CombineLuaPath(_luaNameProperty.stringValue);
        _luaFileAsset = AssetDatabase.LoadAssetAtPath(_luaAssertPath, typeof(Object));
    }











}