using System.Collections.Generic;
using LuaInterface;

public partial class CLuaBuffer
{
    private const int Max_Item_Size = 20000;
    public List<long> integers = new List<long>();
    public List<double> numbers = new List<double>();
    public List<string> strs = new List<string>();

    public ByteArray buffer;

    public int readInt;
    public int readNum;
    public int readStr;

    public int id;
    public int len;

    public void Reset()
    {
        integers.Clear();
        numbers.Clear();
        strs.Clear();
        buffer = null;
        readInt = 0;
        readNum = 0;
        readStr = 0;
        id = 0;
        len = 0;
    }

    public void WriteToLua(bool useOptimize, LuaTable luaBufferInts, LuaTable luaBufferNums,LuaTable luaBufferStrs)
    {
        //needtodo
    }
}