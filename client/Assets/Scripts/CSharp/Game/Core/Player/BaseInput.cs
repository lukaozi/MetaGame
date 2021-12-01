using UnityEngine;
using System.Collections;

public class BaseInput : MonoBehaviour
{
    [Header("===== OUTPUT SIGNALS =====")] public float Dup;
    public float Dright;
    public float Dmag;

    public float Jup;
    public float Jright;

    /// <summary>
    /// 方向
    /// </summary>
    public Vector3 Dvec;

    public bool run;
    public bool jump;
    protected bool lastJump;

    protected float targetDup;
    protected float targetDright;

    protected float velocityDup;
    protected float velocityDright;

    public bool attack;
    protected bool lastAttack;

    [Header("===== others =====")] public bool inputEnabled = true;

    public void CalDmag()
    {
        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);
    }

    public void CalDvec()
    {
        Dvec = transform.right * Dright + transform.forward * Dup;
    }
}