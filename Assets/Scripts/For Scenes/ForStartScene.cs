using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForStartScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void TabButtonStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
