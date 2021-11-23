public class Singleton<T> where T : new()
{
    static protected T mInstance;
    static protected bool IsCreate = false;

    public static T instance
    {
        get
        {
            if (IsCreate == false)
            {
                CreateInstance();
            }

            return mInstance;
        }
    }

    public static void CreateInstance()
    {
        if (IsCreate == true)
        {
            return;
        }

        IsCreate = true;
        mInstance = new T();
    }

    public static void ReleaseInstance()
    {
        mInstance = default(T);
        IsCreate = false;
    }
    
}