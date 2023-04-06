using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundDied : MonoBehaviour
{
    public AudioClip shotSFX;
    public AudioSource sourceAud;
    // Start is called before the first frame update
    void Start()
    {
        sourceAud.PlayOneShot(shotSFX);
    }
}
