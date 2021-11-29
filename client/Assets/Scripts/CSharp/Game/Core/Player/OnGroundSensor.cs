using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capcol;
    public CharacterController characterController;
    public float offset = 0.1f;

    private Vector3 point1;
    private Vector3 point2;
    private float radius;

    // Start is called before the first frame update
    void Awake()
    {
        if (capcol)
        {
            radius = capcol.radius - 0.05f;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var IsGround = false;
        if (capcol)
        {
            point1 = transform.position + transform.up * (radius - offset);
            point2 = transform.position + transform.up * (capcol.height - offset) - transform.up * radius;

            Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));
            IsGround = outputCols.Length > 0;
            
        }
        else
        {
            IsGround = characterController.isGrounded;
        }
        
        if (IsGround)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
        
    }
}
