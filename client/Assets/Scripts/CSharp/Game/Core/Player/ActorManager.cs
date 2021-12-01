using System;
using UnityEngine;


public class ActorManager : MonoBehaviour
{
    public BattleComponent battleComponent;
    public ActorController ac;

    private void Awake()
    {
        ac = GetComponent<ActorController>();
        
        GameObject sensor = transform.Find("sensor").gameObject;
        battleComponent = sensor.GetComponent<BattleComponent>();
        if (battleComponent== null)
        {
            battleComponent = sensor.AddComponent<BattleComponent>();
        }

        battleComponent.actorManager = this;
    }

    public void DoDamage()
    {
        ac.anim.SetTrigger("hit");
    }
    
}