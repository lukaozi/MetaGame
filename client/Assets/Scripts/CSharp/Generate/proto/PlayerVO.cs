// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: PlayerVO.proto

#pragma warning disable CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
[global::ProtoBuf.ProtoContract()]
public partial class PlayerVO : global::ProtoBuf.IExtensible
{
    private global::ProtoBuf.IExtension __pbn__extensionData;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    [global::ProtoBuf.ProtoMember(1, Name = @"name")]
    [global::System.ComponentModel.DefaultValue("")]
    public string Name { get; set; } = "";

    [global::ProtoBuf.ProtoMember(2, Name = @"id")]
    public int Id { get; set; }

    [global::ProtoBuf.ProtoMember(3, Name = @"data")]
    public global::System.Collections.Generic.List<byte[]> Datas { get; } = new global::System.Collections.Generic.List<byte[]>();

    [global::ProtoBuf.ProtoMember(4, Name = @"type")]
    public Types Type { get; set; }

    [global::ProtoBuf.ProtoContract()]
    public enum Types
    {
        Hight = 0,
        Low = 1,
        Normal = 2,
    }

}

#pragma warning restore CS0612, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
