using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CLuaSeriazablePropertyItem
{
        public SerializedProperty parentProperty;

        public int arrayIndex = -1;

        private LuaSerializedDrawerItem _luaSerializedItem;

        public int idx = -1;
        public EnLuaSerializebleType propertyType;

        private SerializedProperty _luaProperty;
        public SerializedProperty GetProperty
        {
                get { return _luaProperty; }
        }

        private SerializedProperty _valueProperty;

        public SerializedProperty ValueProperty => _valueProperty;
        private SerializedProperty _idxProperty;
        private SerializedProperty _nameProperty;
        private SerializedProperty _descProperty;
        
        public bool IsDefaultProperty
        {
                get { return _luaNativeItem != null; }
        }
        public LuaNativePropertyItem _luaNativeItem = null;

        public bool isSubPrefabDefaultProperty = false;
        
        public string Name
        {
                get { return _nameProperty.stringValue; }
        }

        public static CLuaSeriazablePropertyItem CreateOnePropertyItem(LuaSerializedDrawerItem luaSerializedItem,
                SerializedProperty property, SerializedProperty parnt, int arrayIndex)
        {
                CLuaSeriazablePropertyItem propertyItem = new CLuaSeriazablePropertyItem();
                propertyItem.SetProperty(luaSerializedItem, property, parnt, arrayIndex);
                return propertyItem;
        }

        public static void CopyCLuaSerializedProperty(CLuaSeriazablePropertyItem copyItem,
                CLuaSeriazablePropertyItem needCopyItem)
        {
                if (copyItem == null || needCopyItem == null)
                {
                        return;
                }

                if (copyItem.propertyType != needCopyItem.propertyType)
                {
                        Debug.LogError("拷贝类型错误");
                        return;
                }

                switch (copyItem.propertyType)
                {
                        case EnLuaSerializebleType.LuaSerializebleField:
                                break;
                        case EnLuaSerializebleType.GoField:
                                needCopyItem.ValueProperty.objectReferenceValue =
                                        copyItem.ValueProperty.objectReferenceValue;
                                break;
                        case EnLuaSerializebleType.BoolField:
                                needCopyItem.ValueProperty.boolValue = copyItem.ValueProperty.boolValue;
                                break;
                        case EnLuaSerializebleType.IntField:
                                needCopyItem.ValueProperty.intValue = copyItem.ValueProperty.intValue;
                                break;
                        case EnLuaSerializebleType.FloatField:
                                needCopyItem.ValueProperty.floatValue = copyItem.ValueProperty.floatValue;
                                break;
                        case EnLuaSerializebleType.Vector2Field:
                                needCopyItem.ValueProperty.vector2Value = copyItem.ValueProperty.vector2Value;
                                break;
                        case EnLuaSerializebleType.Vector3Field:
                                needCopyItem.ValueProperty.vector3Value = copyItem.ValueProperty.vector3Value;
                                break;
                        case EnLuaSerializebleType.Vector4Field:
                                needCopyItem.ValueProperty.vector4Value = copyItem.ValueProperty.vector4Value;
                                break;
                        case EnLuaSerializebleType.QuaternionField:
                                needCopyItem.ValueProperty.objectReferenceValue = copyItem.ValueProperty.objectReferenceValue;
                                break;
                        case EnLuaSerializebleType.AssetField:
                                needCopyItem.ValueProperty.objectReferenceValue = copyItem.ValueProperty.objectReferenceValue;
                                break;
                        case EnLuaSerializebleType.StringField:
                                needCopyItem.ValueProperty.stringValue = copyItem.ValueProperty.stringValue;
                                break;
                        default:
                                break;
                                
                }
        }


        public void SetDefaultItem(LuaNativePropertyItem item)
        {
                string errorStr = string.Empty;
                EnLuaSerializebleType property = EnLuaSerializebleType.GoField;
                if (System.Enum.TryParse(item.fieldType, out propertyType))
                {
                        string str = StringHelper.MatchSplit(item.value);
                        switch (propertyType)
                        {
                                case EnLuaSerializebleType.LuaSerializebleField:
                                        break;
                                case EnLuaSerializebleType.GoField:
                                        _valueProperty.objectReferenceValue = null;
                                        break;
                                case EnLuaSerializebleType.BoolField:
                                        bool tempBool = false;
                                        if (bool.TryParse(item.value, out tempBool))
                                        {
                                                _valueProperty.boolValue = tempBool;
                                        }
                                        else
                                        {
                                                errorStr = "设置bool默认属性出错";
                                        }
                                        break;
                                case EnLuaSerializebleType.IntField:
                                        int tempInt = 0;
                                        if (int.TryParse(item.value, out tempInt))
                                        {
                                                _valueProperty.intValue = tempInt;
                                        }
                                        else
                                        {
                                                errorStr = "设置int默认属性出错";
                                        }
                                        break;
                                case EnLuaSerializebleType.Vector2Field:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.Vector3Field:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.Vector4Field:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.QuaternionField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.ColorField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.AssetField:
                                        _valueProperty.objectReferenceValue = null;
                                        break;
                                case EnLuaSerializebleType.StringField:
                                        _valueProperty.stringValue = item.value;
                                        break;
                                default:
                                        break;

                        }
                }
                else
                {
                        errorStr = "设置默认属性出错";
                }

                if (!string.IsNullOrEmpty(errorStr))
                {
                        ShowMsgWindow(errorStr);
                        Debug.LogError(errorStr);
                }
        }

        public void ShowMsgWindow(string message)
        {
                EditorUtility.DisplayDialog("消息提示", message, "ok");
        }

        public void SetProperty(LuaSerializedDrawerItem luaSerializedItem, SerializedProperty property,
                SerializedProperty parentProperty, int arrayIndex)
        {
                propertyType = (EnLuaSerializebleType) System.Enum.Parse(typeof(EnLuaSerializebleType), property.type);
                this.parentProperty = parentProperty;
                this.arrayIndex = arrayIndex;
                this._luaProperty = property;
                this._luaSerializedItem = luaSerializedItem;
                _idxProperty = property.FindPropertyRelative("idx");
                idx = _idxProperty.intValue;
                _nameProperty = property.FindPropertyRelative("name");
                _descProperty = property.FindPropertyRelative("desc");
                _valueProperty = property.FindPropertyRelative("value");
        }

        public void DrawProperty(bool isRepeateName, bool isShowDesc)
        {
                using (new AutoChangeBkColor(isRepeateName?Color.yellow:(IsDefaultProperty?Color.green:GUI.backgroundColor)))
                {
                        DrawLuaSerializedbleFieldProperty(isRepeateName, isShowDesc);
                }
                
        }

        public string GetPropertyTypeString()
        {
                if (propertyType == EnLuaSerializebleType.GoField)
                {
                        return GetGoFileTypeString();
                }

                if (propertyType == EnLuaSerializebleType.AssetField)
                {
                        return "UnityEngine.Object";
                }

                return GetNoGoFileTypeString();
        }

        public void MoveUpIndex()
        {
                SetIdx(--idx);
        }

        public void MoveDownIndex()
        {
                SetIdx(++idx);
        }

        public void SetIdx(int Id)
        {
                _idxProperty.intValue = Id;
                idx = _idxProperty.intValue;
        }

        public void SetNameProperty(string name)
        {
                _nameProperty.stringValue = name;
        }

        public string Desc
        {
                get { return _descProperty.stringValue; }
                set { _descProperty.stringValue = value; }
        }

        string GetGoFileTypeString()
        {
                var type = this._valueProperty.objectReferenceValue == null
                        ? typeof(GameObject)
                        : this._valueProperty.objectReferenceValue.GetType();
                if (type == typeof(LuaBehaviourBridge))
                {
                        var luabehaviour = this._valueProperty.objectReferenceValue as LuaBehaviourBridge;
                        return GetLuaBehabiourLuaName(luabehaviour);
                }
                else if (type == typeof(SubPrefabBridge))
                {
                        var luabehaviour = this._valueProperty.objectReferenceValue as SubPrefabBridge;
                        string path = luabehaviour.subPrefabPath;
                        if (string.IsNullOrEmpty(path))
                                return type.ToString();
                        if (!path.Contains(".prefab"))
                        {
                                path = path + ".prefab";
                        }

                        GameObject subPrefab =
                                AssetDatabase.LoadAssetAtPath<GameObject>(SubPrefabBridge.subPrefabRootPath + path);
                        if (subPrefab == null)
                        {
                                return "找不到嵌套预制体：" + path;
                        }

                        return GetLuaBehabiourLuaName(subPrefab.GetComponent<LuaBehaviourBridge>());
                }
                else
                {
                        return type.ToString();
                }
        }

        string GetLuaBehabiourLuaName(LuaBehaviourBridge luabehaviour)
        {
                if (luabehaviour == null) return "LuaBehaviourBridge";
                if (string.IsNullOrEmpty(luabehaviour.m_luaFileName))
                        return "LuaBehaviourBridge";
                var idx = luabehaviour.m_luaFileName.LastIndexOf("/");
                if (idx == -1)
                {
                        return luabehaviour.m_luaFileName;
                }
                else
                {
                        return luabehaviour.m_luaFileName.Substring(idx + 1);
                }
        }

        bool CheckCanFilterComponent(Type type)
        {
                if (CustomSettings.customTypeList == null || CustomSettings.customTypeList.Length == 0) return false;
                for (int i = 0; i < CustomSettings.customTypeList.Length; i++)
                {
                        if (CustomSettings.customTypeList[i].type == type)
                        {
                                return false;
                        }
                }

                return true;
        }

        string GetNoGoFileTypeString()
        {
                string typeStr = _valueProperty.type.ToString();
                if (typeStr == "bool")
                {
                        typeStr = "boolean";
                }

                if (typeStr == "int")
                {
                        typeStr = "number";
                }
                if (typeStr == "float")
                {
                        typeStr = "number";
                }

                return typeStr;
        }
        
        GUIContent moreTitlr = new GUIContent();

        void DrawMoreMenuWindowTip(Rect rect)
        {
                if (EditorGUI.DropdownButton(rect, moreTitlr, FocusType.Keyboard) && _luaSerializedItem != null)
                {
                        _luaSerializedItem.ShowMoreMenuWindow(this);
                }
        }
        
        Rect _tempRect = new Rect(0, 0,0,0);
        Rect _CurRect = new Rect(0, 0,0,0);
        private float _showDescWidthPercentage = 0.25f;
        private float _hideDescWidthPrentage = 0.333f;

        void DrawLuaSerializedbleFieldProperty(bool isRepeateName, bool isShowDesc)
        {
                _CurRect = GUILayoutUtility.GetRect(GUIContent.none, EditorStyles.objectField);
                float widthPercentage = isShowDesc ? _showDescWidthPercentage : _hideDescWidthPrentage;
                _tempRect.Set(_CurRect.x, _CurRect.y, _CurRect.width * (widthPercentage - 0.038f), _CurRect.height);
                if (IsDefaultProperty)
                {
                        EditorGUI.LabelField(_tempRect, _nameProperty.stringValue, EditorStyles.label);
                }
                else
                {
                        _nameProperty.stringValue = EditorGUI.TextField(_tempRect, _nameProperty.stringValue);
                }

                _tempRect.x = _CurRect.width * widthPercentage;
                _tempRect.width = _CurRect.width * widthPercentage;

                if (!isShowDesc && propertyType != EnLuaSerializebleType.GoField)
                        _tempRect.width = _CurRect.width * widthPercentage * 2;
                DrawFieldItem(_tempRect, _CurRect, widthPercentage);

                if (isShowDesc)
                {
                        if (propertyType == EnLuaSerializebleType.GoField)
                                _tempRect.x = _CurRect.width * widthPercentage * 3;
                        else
                        {
                                _tempRect.width *= 2;
                                _tempRect.x = _CurRect.width * widthPercentage * 2;
                        }

                        if (!IsDefaultProperty)
                                _descProperty.stringValue = EditorGUI.TextField(_tempRect, _descProperty.stringValue);
                        else
                        {
                                EditorGUI.LabelField(_tempRect, _descProperty.stringValue, EditorStyles.textField);
                        }
                }

                _tempRect.x = _CurRect.width;
                _tempRect.width = 20;
                DrawMoreMenuWindowTip(_tempRect);
        }

        void DrawFieldItem(Rect rect, Rect insperctRect, float widPercentage)
        {
                if (_valueProperty != null)
                {
                        switch (propertyType)
                        {
                                case EnLuaSerializebleType.LuaSerializebleField:
                                        break;
                                case EnLuaSerializebleType.GoField:
                                        DrawGoFieldProperty(_tempRect, _CurRect, widPercentage);
                                        break;
                                case EnLuaSerializebleType.BoolField:
                                        _valueProperty.boolValue =
                                                EditorGUI.Toggle(_tempRect, _valueProperty.boolValue);
                                        break;
                                case EnLuaSerializebleType.IntField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.FloatField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.Vector2Field:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.Vector3Field:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.Vector4Field:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.QuaternionField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.ColorField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.AssetField:
                                        //needtodo
                                        break;
                                case EnLuaSerializebleType.StringField:
                                        _valueProperty.stringValue =
                                                EditorGUI.TextField(_tempRect, _valueProperty.stringValue);
                                        break;
                                default:
                                        break;
                        }
                }
        }

        private string _tempTypeString = string.Empty;
        private int _selected = 0;
        List<Type> _componentList = new List<Type>();

        void DrawGoFieldProperty(Rect rect, Rect insperctRect, float widthPercentage)
        {
                _componentList.Clear();
                Type t = _valueProperty.objectReferenceValue ==
                        null ? typeof(GameObject) : _valueProperty.objectReferenceValue.GetType();

                GameObject go = null;
                Component component = null;

                if (IsDefaultProperty)
                {
                        Type componentType = GetDefaultGoFileComponentType(_luaNativeItem);
                        if (componentType == null)
                        {
                                Debug.LogError("默认属性出错");
                                return;
                        }

                        if (t != componentType)
                                _valueProperty.objectReferenceValue = null;
                        t = componentType;
                        _componentList.Add(componentType);

                        UnityEngine.Object obj =
                                EditorGUI.ObjectField(rect, _valueProperty.objectReferenceValue, t, true);

                        if (_valueProperty.objectReferenceValue != obj)
                        {
                                if (obj != null && _valueProperty.objectReferenceValue == null && !IsDefaultProperty)
                                {
                                        _nameProperty.stringValue = obj.name;
                                }

                                _valueProperty.objectReferenceValue = obj;
                        }

                        if ((!IsDefaultProperty && obj != null))
                        {
                                _componentList.Add(typeof(GameObject));
                                _componentList.Add(typeof(Transform));
                                GetAllComponentList(_componentList, obj, out component, out go);
                        }

                        _selected = 0;
                        string[] typeStrs = new string[_componentList.Count];

                        for (int i = 0; i < _componentList.Count; i++)
                        {
                                if (t == _componentList[i])
                                {
                                        _selected = i;
                                }

                                typeStrs[i] = _componentList[i].Name;
                        }

                        rect.x = insperctRect.width * widthPercentage * 2;
                        int tempSelected = EditorGUI.Popup(rect, _selected, typeStrs);
                        if (go != null && (tempSelected != _selected || IsDefaultProperty))
                        {
                                if (_componentList[tempSelected] == typeof(GameObject))
                                {
                                        _valueProperty.objectReferenceValue = component.gameObject;
                                }
                                else
                                {
                                        _valueProperty.objectReferenceValue =
                                                go.GetComponent(_componentList[tempSelected]);
                                }

                                _selected = tempSelected;
                        }
                }
        }

        Type GetDefaultGoFileComponentType(LuaNativePropertyItem nativeItem)
        {
                Type componentType = typeof(GameObject);
                if (!string.IsNullOrEmpty(_luaNativeItem.addtiveStr))
                {
                        string luaTypeStr = _luaNativeItem.addtiveStr.Trim();
                        componentType = LuaSerializedDrawerItem.GetUnityComponentType(luaTypeStr);
                        if (componentType == null)
                                componentType = LuaSerializedDrawerItem.GetAssembleType(luaTypeStr);
                        if (componentType == null)
                                return null;
                }

                return componentType;
        }

        void GetAllComponentList(List<Type> componentList, UnityEngine.Object obj, out Component component, out GameObject go)
        {
                //needtodo
                Component[] components = null;
                component = null;
                go = null;
                if (obj is GameObject)
                {
                        go = obj as GameObject;
                        components = go.GetComponents(typeof(Component));
                }
                else
                {
                        component = obj as Component;
                        go = component.gameObject;
                        components = go.GetComponents(typeof(Component));
                }

                for (int i = 0; i < components.Length; i++)
                {
                        Type type = components[i].GetType();
                        if (CheckCanFilterComponent(type)) continue;
                        componentList.Add(type);
                }
        }

        void DrawVector4FieldProperty(Rect rect)
        {
                //needtodo
        }
        
        


}

public class LuaNativePropertyItem
{
        public string name;

        public string desc;

        public string fieldType;

        public string addtiveStr;

        public string value;

        public bool isHasAdd;


}












