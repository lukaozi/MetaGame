using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProtoBuf;
 
public class ProtobufSerializer
{
    /// <summary>  
    /// 序列化  
    /// </summary>  
    /// <typeparam name="T"></typeparam>  
    /// <param name="t"></param>  
    /// <returns></returns>  
    public static string Serialize_b64<T>(T t)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Serializer.Serialize<T>(ms, t);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
 
 
    /// <summary>  
    /// 反序列化  
    /// </summary>  
    /// <typeparam name="T"></typeparam>  
    /// <param name="content"></param>  
    /// <returns></returns>  
    public static T DeSerialize_b64<T>(string content)
    {
        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(content)))
        {
            T t = Serializer.Deserialize<T>(ms);
            return t;
        }
    }
 
 
    /// <summary>  
    /// 序列化  
    /// </summary>  
    /// <typeparam name="T"></typeparam>  
    /// <param name="t"></param>  
    /// <returns></returns>  
    public static string Serialize<T>(T t)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Serializer.Serialize<T>(ms, t);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
    /// <summary>  
    /// 反序列化  
    /// </summary>  
    /// <typeparam name="T"></typeparam>  
    /// <param name="content"></param>  
    /// <returns></returns>  
    public static T DeSerialize<T>(string content)
    {
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(content)))
        {
            T t = Serializer.Deserialize<T>(ms);
            return t;
        }
    }
}