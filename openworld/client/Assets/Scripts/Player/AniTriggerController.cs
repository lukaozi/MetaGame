using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniTriggerController : MonoBehaviour
{

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void RessetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }

}
