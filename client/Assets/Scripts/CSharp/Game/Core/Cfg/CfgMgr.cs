using System.Collections.Generic;
using UnityEngine;

public class CfgMgr : Singleton<CfgMgr>
{
    private string PATH = Application.dataPath + "/Scripts/CSharp/Generate/cfg/";
    
    public T GetCfg<T>(string cfgName, int id) where T : Cfg.ICfg, new()
    {
        CsvData<T> csvAsObjects = CsvTool.Read<T>(PATH + cfgName + ".csv");
        foreach (var row in csvAsObjects.rows)
        {
            if (row.GetId() == id)
            {
                return row;
            }
        }

        return new T();
    }
    
    public List<T> GetCfgs<T>(string cfgName, int id) where T : new()
    {
        CsvData<T> csvAsObjects = CsvTool.Read<T>(PATH + cfgName + ".csv");
        return csvAsObjects.rows;
    }

    public void Test()
    {
        var role = CfgMgr.instance.GetCfg<Cfg.Role>(Cfg.Role.CfgName, 1);
        Log.Msg(role.ToString());
    }
}