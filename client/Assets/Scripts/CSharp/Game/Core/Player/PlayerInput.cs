using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [Header("===== KEY SETTING =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;

    [Header("===== OUTPUT SIGNALS =====")]
    public float Dup;
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
    private bool lastJump;

    private float targetDup;
    private float targetDright;

    private float velocityDup;
    private float velocityDright;

    public bool attack;
    private bool lastAttack;

    [Header("===== others =====")]
    public bool inputEnabled = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);

        if(inputEnabled == false)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(Dright, Dup);

        Dright = tempDAxis.x;
        Dup = tempDAxis.y;

        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);
        Dvec = transform.right * Dright + transform.forward * Dup;

        run = Input.GetKey(keyA);

        bool newJump = Input.GetKey(keyB);
        if(newJump == true && lastJump != jump)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        bool newAttack = Input.GetKey(keyC);
        if (newAttack == true && lastAttack != attack)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;
    }

    private Vector2 SquareToCircle(float x, float y)
    {
        Vector2 output = Vector2.zero;
        output.x = x * Mathf.Sqrt(1 - (y * y) / 2.0f);
        output.y = y * Mathf.Sqrt(1 - (x * x) / 2.0f);
        return output;
    }

}
