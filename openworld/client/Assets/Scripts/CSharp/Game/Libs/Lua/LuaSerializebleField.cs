using UnityEngine;
using System;
using LuaInterface;
using Object = UnityEngine.Object;

[Serializable]
public abstract class LuaSerializebleField
{
    public string name = "";
    public string desc = "";
    public int idx = 0;
    public abstract object GetValue();
    public abstract void SetValue(LuaTable t);
}

[Serializable]
public class GoField : LuaSerializebleField
{
    public Object value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class BoolField : LuaSerializebleField
{
    public bool value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class IntField : LuaSerializebleField
{
    public int value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}


[Serializable]
public class StringField : LuaSerializebleField
{
    public string value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class FloatField : LuaSerializebleField
{
    public float value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class Vector2Field : LuaSerializebleField
{
    public Vector2 value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
 public class Vector3Field : LuaSerializebleField
 {
     public Vector3 value;
     
     public override object GetValue()
     {
         return value;
     }
 
     public override void SetValue(LuaTable t)
     {
         t.SetTable(this.name, this.value);
     }
 }
 
[Serializable]
public class Vector4Field : LuaSerializebleField
{
    public Vector4 value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class QuaternionField : LuaSerializebleField
{
    public Quaternion value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class ColorField : LuaSerializebleField
{
    public Color value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}

[Serializable]
public class AssetField : LuaSerializebleField
{
    public Object value;
    
    public override object GetValue()
    {
        return value;
    }

    public override void SetValue(LuaTable t)
    {
        t.SetTable(this.name, this.value);
    }
}