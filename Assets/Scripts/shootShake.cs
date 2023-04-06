using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootShake : MonoBehaviour
{
    public Animator shake;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            shake.SetBool("shake", true);
        }
        else
        {
            shake.SetBool("shake", false);
        }
    }
}
