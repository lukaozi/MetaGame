using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;

    public float walkSpeed = 2.4f;

    public float runMuti = 2.5f;

    public float jumpVelocity = 2.0f;
    public float rollVelocity = 3.0f;

    [Space(10)]
    [Header("===== Friction Settings =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;


    private Animator anim;
    private PlayerInput playerInput;
    private Rigidbody rigid;
    private CapsuleCollider col;
    private float lerpTarget;

    /// <summary>
    /// 平面速度
    /// </summary>
    private Vector3 planarVec;
    
    // 推力
    private Vector3 thrustVec;

    private bool lockPlanar = false;

    private bool canAttack = true;

    private Vector3 deltaPos;

    [Header("===== weaponPos =====")]
    public Transform weaponPos0Transform = null;
    public Transform weaponPos1Transform = null;
    public Transform weaponPos2Transform = null;

    void Awake()
    {
        anim = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        Gb.weaponPos0Transform = weaponPos0Transform;
        Gb.weaponPos1Transform = weaponPos1Transform;
        Gb.weaponPos2Transform = weaponPos2Transform;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (playerInput.attack && CheckState("ground") && canAttack)
        {
            anim.SetTrigger("attack");
        }

        if (playerInput.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if(rigid.velocity.magnitude > 1.0f)
        {
            anim.SetTrigger("roll");
        }

        float targetMuti = playerInput.run ? 2.0f : 1.0f;
        anim.SetFloat("forward", playerInput.Dmag * (Mathf.Lerp(anim.GetFloat("forward"), targetMuti, 0.5f)));
        if(playerInput.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, playerInput.Dvec, 0.2f);
            //model.transform.forward = playerInput.Dvec;
        }

        if(lockPlanar == false)
        {
            planarVec = playerInput.Dmag * model.transform.forward * walkSpeed * (playerInput.run ? runMuti : 1.0f);
        }
    }

    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        //rigid.position += movingVec * Time.fixedDeltaTime;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        int index = anim.GetLayerIndex(layerName);
        return anim.GetCurrentAnimatorStateInfo(index).IsName(stateName);
    }

    private void DoLockPlanar()
    {
        playerInput.inputEnabled = false;
        lockPlanar = true;
    }

    private void DonotLockPlanar()
    {
        playerInput.inputEnabled = true;
        lockPlanar = false;
    }

    /// 
    /// Message processing block
    /// 

    public void onJumpEnter()
    {
        DoLockPlanar();
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    //public void onJumpExit()
    //{
    //    playerInput.inputEnabled = true;
    //    lockPlanar = false;
    //}

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        DonotLockPlanar();
        canAttack = true;
        col.material = frictionOne;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        DoLockPlanar();
        thrustVec = new Vector3(0, jumpVelocity, 0);
    }

    public void OnRollEnter()
    {
        DoLockPlanar();
        thrustVec = new Vector3(0, rollVelocity, 0);
    }

    public void OnJabEnter()
    {
        DoLockPlanar();
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnAttack1hAEnter()
    {
        playerInput.inputEnabled = false;
        lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        float curWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        curWeight = Mathf.Lerp(curWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), curWeight);
    }

    public void OnAttackIdleEnter()
    {
        playerInput.inputEnabled = true;
        lerpTarget = 0f;
    }

    public void OnAttackIdleUpdate()
    {
        float curWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        curWeight = Mathf.Lerp(curWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), curWeight);
    }

    public void OnUdateRM(object _deltaPos)
    {
        if(CheckState("attack1hC", "attack"))
        {
            deltaPos += (Vector3)_deltaPos;
        }
    }

}
