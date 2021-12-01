using System;
using System.Collections;
using UnityEngine;

public class EnemyInput : BaseInput
{
    IEnumerator Start()
    {
        while (true)
        {
            Dup = 1.0f;
            Dright = 0.0f;
            Jright = 1.0f;
            Jup = 0f;
            yield return new WaitForSeconds(3.0f);
            Dup = 0.0f;
            Dright = 0.0f;
            Jright = 0.0f;
            Jup = 0f;
            yield return new WaitForSeconds(1.0f);
        }
        
//        Dup = 1.0f;
//        Dright = 0.0f;
//        Jright = 1.0f;
//        Jup = 0f;
    }

    private void Update()
    {
        CalDmag();
        CalDvec();
    }
}