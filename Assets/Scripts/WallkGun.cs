using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallkGun : MonoBehaviour
{
    public Animator gun;
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.Mouse0))
        {
            gun.SetBool("Walk", true);
        }
        else
        {
            gun.SetBool("Walk", false);
        }
    }
}
