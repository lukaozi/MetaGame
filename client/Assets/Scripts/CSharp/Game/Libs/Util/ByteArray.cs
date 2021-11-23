using System;

public partial class ByteArray
{
    public const int Max_Buffer_Size = 1024 * 1024 * 1;//1m很大了

    public enum enEnsureMoveToFirst
    {
        auto,
        forceEnsureMove,
        forceNoEnsureMove
    }

    public bool isLittleEndian = BitConverter.IsLittleEndian;
    public bool isFixedCompress = false;

    public bool autoMoveFirst = true;

    public byte[] Buffer;
    private int readPos;
    private int writePos;

    public ByteArray(int capacity, bool isLittleEndian = false, bool isFixedCompress = true, bool autoMoveFirst = true)
    {
        this.Buffer = new byte[capacity];
        this.isLittleEndian = isLittleEndian;
        this.isFixedCompress = isFixedCompress;
        this.autoMoveFirst = autoMoveFirst;
    }

    //保证有足够空间可以write
    public void EnsureCapacity(int len, enEnsureMoveToFirst moveToFirstType = enEnsureMoveToFirst.auto)
    {
        //needtodo
    }

    public int Remain
    {
        get { return writePos - readPos; }
    }

    public int ReadPos
    {
        get
        {
            if(autoMoveFirst)
                throw new Exception("autoMoveFirst 为 true, 不可读");
            return readPos;
        }
        set
        {
            if(autoMoveFirst)
                throw new Exception("autoMoveFirst = true, 不可写");
            readPos = value;
        }
    }

    public int WritePos
    {
        get
        {
            if(autoMoveFirst)
                throw new Exception("autoMoveFirst = true, 不可读");
            return writePos;
        }
        set
        {
            if(autoMoveFirst)
                throw new Exception("autoMoveFirst = true, 不可写");
            writePos = value;
        }
    }

    public int ReadInt(bool forceNoCompress = false)
    {
        if (this.isFixedCompress && !forceNoCompress)//变长int
        {
            long x;
            sbyte y;
            //needtodo
        }

        return 0;
    }

    public byte[] ReadBytes(int len)
    {
        CheckRead(len);
        byte[] data = new byte[len];
        Array.Copy(Buffer, readPos, data, 0, len);
        readPos += len;
        return data;
    }

    public void CheckRead(int len)
    {
        if(Remain < len)
            throw new Exception("CheckRead");
    }
    
}