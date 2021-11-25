using System;
using UnityEngine;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;

//定义一个序列化与反序列化对象
//[ProtoBuf.ProtoContract]
//class Person
//{
//    [ProtoBuf.ProtoMember(1)]
//    public string name;
//    [ProtoBuf.ProtoMember(2)]
//    public int age;
//}
public class ProtobufTest : MonoBehaviour
{
    void Start()
    {
        Person f = new Person();
        f.Datas.Add(new byte[] {0x1, 0x2, 0x3});
        f.Id = 12345678;
        f.Name = "proto-net";
        f.Type = Person.Types.Hight;

        byte[] data;
        Debug.Log("开始序列化数据.");
        using (var stream = new MemoryStream())
        {
            Serializer.Serialize(stream, f);
            data = stream.ToArray();
        }

        Debug.Log("开始反序列化数据.");
        using (var stream = new MemoryStream(data))
        {
            var _f = Serializer.Deserialize<Person>(stream);
            Debug.Log(_f.Name);
        }
    }
}