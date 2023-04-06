using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Animator aim;
    public GameObject pricel;
    public bool atAim;
    public bool var = true;

    void Update()
    {
        atAim = (Input.GetKeyDown(KeyCode.Mouse1) && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.Mouse0)) ? true : false;
        aim.SetBool("Aim", atAim);
        if (atAim)
        {
            if (var) // to pricel
            {
                pricel.SetActive(false);
                var = false;
            }
            else // from pricel
            {
                pricel.SetActive(true);
                var = true;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && !var)
        {
            aim.SetBool("ShootAtAim", true);
        }
        else
        {
            aim.SetBool("ShootAtAim", false);
        }
    }
}
