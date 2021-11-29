using System;

namespace Cfg
{
    public interface ICfg
    {
        int GetId();
    }
    
    [Serializable]
    public class Role:ICfg
    {
        public static string CfgName = "Role";
        
        [CsvInt("id")] public int id;

        [CsvString("name")] public string name;

        [CsvDouble("age")] public double age;

        public override string ToString()
        {
            return id + ", " + name + ", " + age;
        }

        public int GetId()
        {
            return id;
        }
    }

}

