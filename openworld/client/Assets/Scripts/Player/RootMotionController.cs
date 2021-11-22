using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        //print(anim.deltaPosition);
        SendMessageUpwards("OnUdateRM", (object)anim.deltaPosition);
    }
}
