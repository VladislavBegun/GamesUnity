using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDied : MonoBehaviour
{
    public float hp = 100;
    public GameObject thisEnemy;

    void Update()
    {
        StartCoroutine(DiedEnemy());
    }

    IEnumerator DiedEnemy()
    {
        yield return new WaitForSeconds(3);
        Destroy(thisEnemy);
    }
}
