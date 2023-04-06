using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlita : MonoBehaviour
{
    public GameObject door;

    public bool takeButton = false;

    public float point;
    public float point1;

    private bool rot;

    // Start is called before the first frame update
    void Start()
    {
        /*point = (door.transform.position.y > 0f) ? door.transform.position.y + door.transform.localScale.y : door.transform.position.y - door.transform.localScale.y;
        point1 = door.transform.position.x;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (takeButton)
        {
            if (door.transform.position.y > 2.7)
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 1f * Time.deltaTime, door.transform.position.z);
            else
                takeButton = false;
        }
        /*if (takeButton)
        {
            if (rot)
            {
                if (door.transform.position.y < point)
                    door.transform.position = new Vector3(door.transform.position.x + 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                else
                    takeButton = false;
            }
            else
            {
                if (door.transform.position.x > point)
                    door.transform.position = new Vector3(door.transform.position.x - 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                else
                    takeButton = false;
            }
        }*/
    }
}
