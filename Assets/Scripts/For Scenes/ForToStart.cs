using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForToStart : MonoBehaviour
{
    public void TabButtonStart()
    {
        SceneManager.LoadScene("Start");
    }
}
