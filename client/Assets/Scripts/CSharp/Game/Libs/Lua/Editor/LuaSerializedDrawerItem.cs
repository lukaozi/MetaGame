using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


public class LuaSerializedDrawerItem
{
    //获取反射类型
    private static Assembly assemble = null;

    public static System.Type GetAssembleType(string typeStr)
    {
        if (assemble ==null)
        {
            assemble = System.Reflection.Assembly.Load("Assembly-CSharp");
        }

        return assemble.GetType(typeStr);
    }

    public static System.Type GetUnityComponentType(string fullTypeName)
    {
        int lastDotIndex = fullTypeName.LastIndexOf(".");
        
        if (lastDotIndex <= 0) return null;
        string namespce = fullTypeName.Substring(0, lastDotIndex);
            
        string resultstr = fullTypeName + "," + namespce;
        return System.Type.GetType(resultstr);
    }

    private Editor _editor;
   
    #region
    private SerializedProperty m_goFields;
    private SerializedProperty m_intFields;
    private SerializedProperty m_boolFields;
    private SerializedProperty m_floatFields;
    private SerializedProperty m_v2Fields;
    private SerializedProperty m_v3Fields;
    private SerializedProperty m_v4Fields;
    private SerializedProperty m_quaternionFields;
    private SerializedProperty m_assetFields;
    private SerializedProperty  m_stringFields;
    private SerializedProperty m_clrFields;
    #endregion
    
    List<CLuaSeriazablePropertyItem> _propertyItemList = new List<CLuaSeriazablePropertyItem>();
    public int PropertyCount { get { return _propertyItemList.Count; } }


    public List<CLuaSeriazablePropertyItem> GetAllPropertyItemList()
    {
        return _propertyItemList;
        
    }
    
    Dictionary<string, int> _checkNameRepeatList = new Dictionary<string, int>();

    public bool isCanRepeatName = true;

    private int curPage = 0;
    private int pageCount = 15;

    private GenericMenu _winMoreMenu;

    private CLuaSeriazablePropertyItem _menuWinSelectItem = null;

    void RefreshMoreMenuWindow()
    {
        _winMoreMenu = new GenericMenu();
        _winMoreMenu.DropDown(new Rect(100, 100, 100, 100));
        if (_editor != null && _editor is LuaBehaviourBridgeEditor)
        {
            InitLuaBehaviourWinMoreMenu(_winMoreMenu);
        }
        
        //needtodo
    }

    public void ShowMoreMenuWindow(CLuaSeriazablePropertyItem menuWinSeIectItem)
    {
        _winMoreMenu = new GenericMenu();
        _winMoreMenu.DropDown(new Rect(100, 100, 100, 100));
        if (_editor != null && _editor is LuaBehaviourBridgeEditor)
        {
            InitLuaBehaviourWinMoreMenu(_winMoreMenu);
        }
        
        //needtodo
    }

    void InitLuaBehaviourWinMoreMenu(GenericMenu genericMenu)
    {
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.GoField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.GoField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.AssetField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.AssetField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.BoolField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.BoolField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.ColorField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.ColorField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.FloatField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.FloatField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.QuaternionField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.QuaternionField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.IntField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.IntField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.StringField.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.StringField);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.Vector2Field.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.Vector2Field);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.Vector3Field.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.Vector3Field);});
        _winMoreMenu.AddItem(new GUIContent("添加/" + EnLuaSerializebleType.Vector4Field.ToString()), 
            false, () => { Insert_AddProperty(EnLuaSerializebleType.Vector4Field);});
        
        
        _winMoreMenu.AddItem(new GUIContent("移除"),
            false, () => { OnClickMenu_Remove();});
        
        _winMoreMenu.AddItem(new GUIContent("上移"),
            false, () => { OnClickMenu_MoveUp();});
        
        _winMoreMenu.AddItem(new GUIContent("下移"),
            false, () => { OnClickMenu_MoveDown();});
    }
    
    List<NeedAddItem> needAddList = new List<NeedAddItem>();
    List<CLuaSeriazablePropertyItem> needRemoveList = new List<CLuaSeriazablePropertyItem>();

    struct NeedAddItem
    {
        public int idx;
        public EnLuaSerializebleType type;
    }


    void Insert_AddProperty(EnLuaSerializebleType propertyType)
    {
        if (_menuWinSelectItem == null)
        {
            return;
        }
        needAddList.Add(new NeedAddItem(){type = propertyType, idx = _menuWinSelectItem.idx});
        _menuWinSelectItem = null;
    }

    void OnClickMenu_Remove()
    {
        if (_menuWinSelectItem==null)
        {
            return;
        }

        if (_menuWinSelectItem.IsDefaultProperty)
        {
            _menuWinSelectItem = null;
            return;
        }
        needRemoveList.Add(_menuWinSelectItem);
    }

    void CheckAddOrRemove()
    {
        while (needRemoveList.Count > 0)
        {
            CLuaSeriazablePropertyItem removeItem = needRemoveList[0];
            needRemoveList.Remove(removeItem);
            if(!DelOneProperyItem(removeItem)) continue;
            _editor.serializedObject.ApplyModifiedProperties();
            _propertyItemList.Sort((a, b) => { return a.idx - b.idx;});

            Refresh();
        }
        
        needRemoveList.Clear();
        
        while (needAddList.Count > 0)
        {
            NeedAddItem addItem = needAddList[0];
            CLuaSeriazablePropertyItem property = AddField(_editor, addItem.type);
            int index = addItem.idx;
            int newIdx = index + 1;
            foreach (var item in _propertyItemList)
            {
                if (item.idx >= newIdx)
                {
                    item.MoveDownIndex();
                }
            }
            property.SetIdx(newIdx);
            _propertyItemList.Sort((a, b) => { return a.idx - b.idx;});
            _editor.serializedObject.ApplyModifiedProperties();
            needAddList.Remove(addItem);
        }
        needAddList.Clear();
    }
    
    void OnClickMenu_MoveUp()
    {
        if (_menuWinSelectItem == null) return;
        MoveUpItem(_menuWinSelectItem);
        _menuWinSelectItem = null;
        _propertyItemList.Sort((a, b) => { return a.idx - b.idx;});
    }

    void MoveUpItem(CLuaSeriazablePropertyItem propertyItem)
    {
        int itemIdx = propertyItem.idx;
        if (itemIdx <= 0) return;
        CLuaSeriazablePropertyItem shortterItem = GetProperyItem(itemIdx - 1);
        if (shortterItem == null)
        {
            Debug.LogError("没有id更小的item");
            return;
        }
        shortterItem.MoveDownIndex();
        _menuWinSelectItem.MoveUpIndex();
        _editor.serializedObject.ApplyModifiedProperties();
    }

    CLuaSeriazablePropertyItem GetProperyItem(int itemIndex)
    {
        foreach (var item in _propertyItemList)
        {
            if (item.idx == itemIndex)
                return item;
        }

        return null;
    }
    
    void OnClickMenu_MoveDown()
    {
        if (_menuWinSelectItem == null) return;
        MoveDownItem(_menuWinSelectItem);
        _propertyItemList.Sort((a, b) => { return a.idx - b.idx;});
        _menuWinSelectItem = null;
    }

    void MoveDownItem(CLuaSeriazablePropertyItem propertyItem)
    {
        int itemIdx = propertyItem.idx;
        if (itemIdx >= _propertyItemList.Count - 1) return;
        CLuaSeriazablePropertyItem biggerItem = GetProperyItem(itemIdx + 1);
        if (biggerItem == null)
        {
            Debug.LogError("没有id更大的Item");
            return;
        }
        biggerItem.MoveUpIndex();
        _menuWinSelectItem.MoveDownIndex();
        _editor.serializedObject.ApplyModifiedProperties();
    }

    public GenericMenu winPropertyMenu;

    void RefreshPropertyTypeMenuWindow()
    {
        winPropertyMenu = new GenericMenu();
        winPropertyMenu.DropDown(new Rect(100, 100, 100, 100));
        if (_editor != null && _editor is LuaBehaviourBridgeEditor)
        {
            AddLuaBehaviourAddWindow(_editor, winPropertyMenu);
        }
        //needtodo
    }

    void AddLuaBehaviourAddWindow(Editor editor, GenericMenu winMenu)
    {
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.GoField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.GoField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.AssetField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.AssetField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.BoolField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.BoolField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.ColorField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.ColorField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.FloatField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.FloatField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.StringField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.StringField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.IntField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.IntField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.QuaternionField.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.QuaternionField);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.Vector2Field.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.Vector2Field);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.Vector3Field.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.Vector3Field);} );
        winMenu.AddItem(new GUIContent(EnLuaSerializebleType.Vector4Field.ToString()), false,
            () => { AddField(editor, EnLuaSerializebleType.Vector4Field);} );
    }

    private string _luaScriptPath = string.Empty;

    public LuaSerializedDrawerItem(Editor editor, string luaScriptPath)
    {
        this._editor = editor;
        _luaScriptPath = luaScriptPath;
        Refresh();
    }

    void UpdateVariable(bool isShowDesc)
    {
        CheckAddOrRemove();
        UpdateRepeateNameDic();
        if (curPage < 0 || pageCount <= 0)
        {
            foreach (var item in _propertyItemList)
            {
                bool isRepeatName = IsCheckItemRepeateName(item);
                item.DrawProperty(isRepeatName, isShowDesc);
            }
        }
        else
        {
            for (int i = curPage* pageCount; i < (curPage+1)*pageCount; i++)
            {
                if (i >= _propertyItemList.Count) break;
                _propertyItemList[i].DrawProperty(IsCheckItemRepeateName(_propertyItemList[i]), isShowDesc);
            }
        }

        if (GUI.changed)
        {
            isCanRepeatName = true;
        }
    }
    
    GUIContent attriContent = new GUIContent("添加属性");

    public CLuaSeriazablePropertyItem AddSerializedProperty(Editor editor, CLuaSeriazablePropertyItem item,
        bool isSubprefabDefaultName)
    {
        CLuaSeriazablePropertyItem newItem = AddField(editor, item.propertyType);
        newItem.SetNameProperty(item.Name);
        newItem.Desc = item.Desc;
        
        CLuaSeriazablePropertyItem.CopyCLuaSerializedProperty(item, newItem);
        newItem.isSubPrefabDefaultProperty = isSubprefabDefaultName;
        editor.serializedObject.ApplyModifiedProperties();
        return newItem;
    }

    public void OnInspectorGUI(bool isShowDesc, int page = -1, int pageCount = 0)
    {
        curPage = page;
        this.pageCount = pageCount;
        UpdateVariable(isShowDesc);

        if (_propertyItemList.Count == 0)
        {
            Rect r = GUILayoutUtility.GetRect(attriContent, EditorStyles.toolbarButton);
            r.x = r.width * 0.5f - 30;
            r.width = r.width * 0.3f;
            if (EditorGUI.DropdownButton(r, attriContent, FocusType.Keyboard))
            {
                winPropertyMenu.ShowAsContext();
            }
        }
    }

    private void OnDisable()
    {
        _propertyItemList.Clear();
        _checkNameRepeatList.Clear();
        _menuWinSelectItem = null;
        needAddList.Clear();
        needRemoveList.Clear();
    }

    public void Refresh()
    {
        _editor.serializedObject.Update();
        m_goFields = _editor.serializedObject.FindProperty("m_goFields");
        m_boolFields = _editor.serializedObject.FindProperty("m_boolFields");
        m_intFields = _editor.serializedObject.FindProperty("m_intFields");
        m_stringFields = _editor.serializedObject.FindProperty("m_stringFields");
        m_floatFields = _editor.serializedObject.FindProperty("m_floatFields");
        m_v2Fields = _editor.serializedObject.FindProperty("m_v2Fields");
        m_v3Fields = _editor.serializedObject.FindProperty("m_v3Fields");
        m_v4Fields = _editor.serializedObject.FindProperty("m_v4Fields");
        m_quaternionFields = _editor.serializedObject.FindProperty("m_quaternionFields");
        m_clrFields = _editor.serializedObject.FindProperty("m_clrFields");
        m_assetFields = _editor.serializedObject.FindProperty("m_assetFields");

       //needtodo
    }

    public void RefreshMoreWindow()
    {
        RefreshProperty();
        RefreshMoreMenuWindow();
    }

    void RefreshProperty()
    {
        needAddList.Clear();
        needRemoveList.Clear();
        _checkNameRepeatList.Clear();

        if (!string.IsNullOrEmpty(_luaScriptPath))
            ReadLuaNativeProperty(_luaScriptPath);

        _propertyItemList.Clear();
        
        AddVarialblesList(m_goFields);
        AddVarialblesList(m_intFields);
        AddVarialblesList(m_stringFields);
        AddVarialblesList(m_boolFields);
        AddVarialblesList(m_floatFields);
        AddVarialblesList(m_v2Fields);
        AddVarialblesList(m_v3Fields);
        AddVarialblesList(m_v4Fields);
        AddVarialblesList(m_quaternionFields);
        AddVarialblesList(m_clrFields);
        AddVarialblesList(m_assetFields);

        foreach (var item in luaNativePropertyItemList)
        {
            if(item.isHasAdd)continue;

            EnLuaSerializebleType propertyType = EnLuaSerializebleType.GoField;
            if (System.Enum.TryParse<EnLuaSerializebleType>(item.fieldType, out propertyType))
            {
                CLuaSeriazablePropertyItem propertyItem = AddField(_editor, propertyType);
                propertyItem.SetNameProperty(item.name);
                propertyItem.Desc = item.desc;
                item.isHasAdd = true;
                propertyItem._luaNativeItem = item;
                propertyItem.SetDefaultItem(item);
                _editor.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogError("类型错误");
            }
        }
        
        //needtodo
        
        _propertyItemList.Sort((a, b) => { return a.idx - b.idx;});
    }
    
    List<LuaNativePropertyItem> luaNativePropertyItemList = new List<LuaNativePropertyItem>();

    void AddVarialblesList(SerializedProperty property)
    {
        if (property == null) return;
        for (int i = 0; i < property.arraySize; i++)
        {
            SerializedProperty item = property.GetArrayElementAtIndex(i);
            CLuaSeriazablePropertyItem propertyItem =
                CLuaSeriazablePropertyItem.CreateOnePropertyItem(this, item, property, i);
            _propertyItemList.Add(propertyItem);
            LuaNativePropertyItem nativePropertyItem = GetLuaNativePropertyItemByName(propertyItem.Name);
            if (nativePropertyItem != null)
            {
                propertyItem._luaNativeItem = nativePropertyItem;
                nativePropertyItem.isHasAdd = true;
            }
        }
    }

    public CLuaSeriazablePropertyItem FindSerializedPropertyByName(string key)
    {
        foreach (var item in _propertyItemList)
        {
            if (item.Name == key)
                return item;
        }

        return null;
    }

    public void UpdateRepeateNameDic()
    {
        if (isCanRepeatName)
        {
            _checkNameRepeatList.Clear();
            foreach (var item in _propertyItemList)
            {
                if (!_checkNameRepeatList.ContainsKey(item.Name))
                {
                    _checkNameRepeatList.Add(item.Name, 1);
                }
                else
                {
                    _checkNameRepeatList[item.Name]++;
                }
            }

            isCanRepeatName = false;
        }
    }

    public bool IsCheckItemRepeateName(CLuaSeriazablePropertyItem propertyItem)
    {
        return _checkNameRepeatList.ContainsKey(propertyItem.Name) && _checkNameRepeatList[propertyItem.Name] > 1;
    }

    public LuaNativePropertyItem GetLuaNativePropertyItemByName(string name)
    {
        foreach (var item in luaNativePropertyItemList)
        {
            if (item.name == name)
                return item;
        }

        return null;
    }

    bool DelOneProperyItem(CLuaSeriazablePropertyItem item, bool isForceDeleted = false)
    {
        if (!isForceDeleted && item.isSubPrefabDefaultProperty)
        {
            Debug.LogError("默认不能删除");
            return false;
        }

        int index = item.idx;
        foreach (var temp in _propertyItemList)
        {
            if (temp.idx > index)
            {
                temp.MoveUpIndex();
            }
        }

        int arrayIndex = item.arrayIndex;
        _propertyItemList.Remove(item);
        item.parentProperty.DeleteArrayElementAtIndex(arrayIndex);
        return true;
    }

    public void DeleteAll()
    {
        for (int i = _propertyItemList.Count - 1; i >= 0; i--)
        {
            DelOneProperyItem(_propertyItemList[i], true);
        }

        _editor.serializedObject.ApplyModifiedProperties();
    }

    CLuaSeriazablePropertyItem AddField(Editor editor, EnLuaSerializebleType propertyType)
    {
        SerializedProperty parentProperty = null;
        switch (propertyType)
        {
            case EnLuaSerializebleType.LuaSerializebleField:
                break;
            case EnLuaSerializebleType.GoField:
                parentProperty = m_goFields;
                break;
            case EnLuaSerializebleType.IntField:
                parentProperty = m_intFields;
                break;
            case EnLuaSerializebleType.FloatField:
                parentProperty = m_floatFields;
                break;
            case EnLuaSerializebleType.Vector2Field:
                parentProperty = m_v2Fields;
                break;
            case EnLuaSerializebleType.Vector3Field:
                parentProperty = m_v3Fields;
                break;
            case EnLuaSerializebleType.Vector4Field:
                parentProperty = m_v4Fields;
                break;
            case EnLuaSerializebleType.QuaternionField:
                parentProperty = m_quaternionFields;
                break;
            case EnLuaSerializebleType.ColorField:
                parentProperty = m_clrFields;
                break;
            case EnLuaSerializebleType.AssetField:
                parentProperty = m_assetFields;
                break;
            case EnLuaSerializebleType.StringField:
                parentProperty = m_stringFields;
                break;
        }
        
        parentProperty.InsertArrayElementAtIndex(parentProperty.arraySize);
        SerializedProperty property = parentProperty.GetArrayElementAtIndex(parentProperty.arraySize - 1);
        if(property == null) Debug.LogError("添加了一个新的属性， 怎么没有了");
        CLuaSeriazablePropertyItem item =
            CLuaSeriazablePropertyItem.CreateOnePropertyItem(this, property, parentProperty,
                parentProperty.arraySize - 1);
        if (propertyType == EnLuaSerializebleType.GoField || propertyType == EnLuaSerializebleType.AssetField)
        {
            item.ValueProperty.objectReferenceValue = null;
        }
        _propertyItemList.Add(item);
        item.SetIdx(_propertyItemList.Count - 1);
        int id = _propertyItemList.Count - 1;
        string newName = propertyType + "_" + id;
        while (true)
        {
            if (!_checkNameRepeatList.ContainsKey(newName)) break;
            newName = propertyType + "_" + ++id;
        }
        item.SetNameProperty(newName);
        editor.serializedObject.ApplyModifiedProperties();
        return item;
    }

    public CLuaSeriazablePropertyItem GetPropertyItemByName(string name)
    {
        foreach (var item in _propertyItemList)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    void ReadLuaNativeProperty(string luaAssetPath)
    {
        if (!File.Exists(luaAssetPath)) return;
        string[] lines = File.ReadAllLines(luaAssetPath, Encoding.UTF8);
        luaNativePropertyItemList.Clear();
        int startLine = -1;
        int endLine = -1;
        string propertiesStr = "Properties";
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains(propertiesStr))
                startLine = i;
            if (i != -1 && lines[i].Contains("}"))
            {
                endLine = i;
                break;
            }
        }

        if (startLine == -1 || endLine == -1)
        {
            return;
        }

        for (int i = startLine; i <= endLine; i++)
        {
            string str = lines[i].Trim();
            if(string.IsNullOrEmpty(str) || !str.Contains("(") || !str.Contains(")")) continue;
            string desc = StringHelper.MatchSplit(str.Split('=')[0], '(', ')');
            string name = MatchName(str).Trim();
            if (string.IsNullOrEmpty(desc))
            {
                Debug.LogError("lua属性描述数据为空："+ str);
                continue;
            }

            string[] descArray = MatchStringDesc(desc);
            if (descArray.Length < 2)
            {
                Debug.LogError("属性描述出错:" + str);
                continue;
            }

            desc = descArray[0].Trim('\"');
            string fieldType = descArray[1];
            string addtiveStr = string.Empty;
            if (descArray.Length > 2) addtiveStr = descArray[2];
            string value = MatchValueStr(str).Trim();
            luaNativePropertyItemList.Add(new LuaNativePropertyItem()
            {
                fieldType =   fieldType,
                name = name,
                addtiveStr =  addtiveStr,
                desc = desc,
                value = value
            });
        }
    }

    public void GetLuaCodeAllProperty(string luaFileAssetPath)
    {
        if (!File.Exists(luaFileAssetPath))
        {
            Debug.LogError("不存在绑定的脚本");
            return;
        }

        string[] FileRead = File.ReadAllLines(luaFileAssetPath, Encoding.UTF8);
        int newLine = -1;
        for (int i = 0; i < FileRead.Length; i++)
        {
            if (FileRead.Contains("function _M.New(self)") && !FileRead[i].Contains("--") &&
                !FileRead[i].Contains("[["))
            {
                if (newLine != -1)
                {
                    Debug.LogError("找不到function _M.New(self)， 没法开始插入");
                    return;
                }

                newLine = i;
            }
        }

        if (newLine == -1)
        {
            return;
        }
        
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= newLine; i++)
        {
            sb.AppendLine(FileRead[i]);
        }

        string classStr = "\t---@type {0} {1}";
        string selfText = "\tself.{0}=nil";
        sb.AppendLine("\t--region StartAutoGenProperty");

        foreach (var item in _propertyItemList)
        {
            if(GetLuaNativePropertyItemByName(item.Name) != null) continue;

            sb.AppendLine(string.Format(classStr, item.GetPropertyTypeString(), item.Desc));
            sb.AppendLine(string.Format(selfText, item.Name));
        }

        sb.AppendLine("\t--endregion EndAutoGenProperty");
        bool isFiltering = false;

        for (int i = newLine + 1; i < FileRead.Length; i++)
        {
            if (i > FileRead.Length - 1) break;
            if (FileRead[i].Contains("StartAutoGenProperty"))
            {
                isFiltering = true;
                continue;
            }

            if (FileRead[i].Contains("EndAutoGenProperty"))
            {
                if (!isFiltering)
                {
                    Debug.LogError("旧标记错误1");
                    return;
                }
                isFiltering = false;
                continue;
            }
            if(isFiltering) continue;
            sb.AppendLine(FileRead[i]);
        }

        if (isFiltering)
        {
            Debug.LogError("旧标记错误2");
            return;
        }

        using (StreamWriter sw = new StreamWriter(luaFileAssetPath))
        {
            sw.Write(sb.ToString());
        }
    }

    string MatchName(string str)
    {
        string format = @"\w+";
        Match match = Regex.Match(str, format);
        if (match == null) return string.Empty;
        string name = match.Value;
        return name.Replace("_", string.Empty);
    }

    string[] MatchStringDesc(string str)
    {
        str = str.Trim();
        string[] strs = str.Split(',');

        if (strs.Length > 3)
        {
            int index = str.LastIndexOf("Range");
            if (index > 0)
            {
                string[] newStrs = new string[3];
                newStrs[0] = strs[0];
                newStrs[1] = strs[1];
                string subString = str.Substring(index);
                newStrs[2] = subString;
                return newStrs;
            }
        }

        return strs;
    }

    string MatchValueStr(string str)
    {
        string[] strs = str.Split('=');
        if (strs.Length < 2)
        {
            return string.Empty;
        }

        return strs[1];
    }
   




}