using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkShake : MonoBehaviour
{
    public Animator walkShake;
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.Mouse0))
        {
            walkShake.SetBool("walk", true);
        }
        else
        {
            walkShake.SetBool("walk", false);
        }
    }
}
