using System;
using Core.Managers;
using UnityEngine;


public class GameEnter : MonoBehaviour
{
    public static GameEnter instance;
    
    private void Start()
    {
        CoroutineManager.Init(this);
        new GameEnterSchedule().Run();
    }
}