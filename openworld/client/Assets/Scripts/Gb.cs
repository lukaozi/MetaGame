using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gb : MonoBehaviour
{
    public static bool IS_MONO = false;

    public static bool IS_HYBRIDE_ECS = false;

    public static bool IS_PURE_ECS = false;

    public static int particleNum = 10;

    public static int createRadius = 300;

    public static float alignSpeed = 1.8f;

    public static float velocity = 40f;

    public static GameObject player = null;

    public static Transform weaponPos0Transform = null;
    public static Transform weaponPos1Transform = null;
    public static Transform weaponPos2Transform = null;

}
