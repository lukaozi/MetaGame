using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleComponent : MonoBehaviour
{
    public ActorManager actorManager;

    private CapsuleCollider defCol;

    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up * 1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.25f;
        defCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            actorManager.DoDamage();
        }
    }
}