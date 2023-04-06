using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;

    public bool takeButton = false;

    public float point;
    public float point1;

    public bool isDoor;
    public bool isPlita;

    private bool rot;

    private float dist;
    private bool readyRet = true;
    private bool doRet = false;
    private bool ret = true;

    public int steps = 1;
    private float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        point = (door.transform.position.x > 0f) ? door.transform.position.x + door.transform.localScale.x * .9f  : door.transform.position.x - door.transform.localScale.x * .9f;
        point1 = door.transform.position.x;
        rot = (point > 0f) ? true : false;
        dist = transform.position.y - .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(doRet)
        {
            if (ret)
            {
                if (transform.position.y > dist)
                    transform.position = new Vector3(transform.position.x, transform.position.y - .3f * Time.deltaTime, transform.position.z);
                else
                    ret = false;
            }
            else
            {
                if (transform.position.y < dist + .1f)
                    transform.position = new Vector3(transform.position.x, transform.position.y + .3f * Time.deltaTime, transform.position.z);
                else
                {
                    doRet = false;
                    ret = true;
                }
            }
        }
        if (takeButton)
        {
            if (readyRet)
            {
                doRet = true;
                readyRet = false;
            }
            /*if(ret)
            {
                if (transform.position.y > dist)
                    transform.position = new Vector3(transform.position.x, transform.position.y - .2f * Time.deltaTime, transform.position.z);
                else
                    ret = false;
            }
            /*else
            {
                if (transform.position.y <= dist + 1f)
                    transform.position = new Vector3(transform.position.x, transform.position.y + .2f * Time.deltaTime, transform.position.z);
            }*/
            if (isDoor)
            {
                if (rot)
                {
                    switch(steps)
                    {
                        case 1:
                            {
                                if (door.transform.position.x < point)
                                    door.transform.position = new Vector3(door.transform.position.x + 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                                else
                                    steps = 2;
                            }
                            break;
                        case 2:
                            {
                                counter += .5f * Time.deltaTime;
                                if (counter > 1f)
                                {
                                    steps = 3;
                                    counter = 0f;
                                }
                            }
                            break;
                        case 3:
                            {
                                if (door.transform.position.x > point - door.transform.localScale.x * .9f)
                                    door.transform.position = new Vector3(door.transform.position.x - 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                                else
                                {
                                    takeButton = false;
                                    readyRet = true;
                                    steps = 1;
                                }
                            }
                            break;
                    }
                    /*if (door.transform.position.x < point)
                        door.transform.position = new Vector3(door.transform.position.x + 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                    else
                    {
                        takeButton = false;
                        readyRet = true;
                    }*/
                }
                else
                {
                    switch (steps)
                    {
                        case 1:
                            {
                                if (door.transform.position.x > point)
                                    door.transform.position = new Vector3(door.transform.position.x - 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                                else
                                    steps = 2;
                            }
                            break;
                        case 2:
                            {
                                counter += .5f * Time.deltaTime;
                                if (counter > 1f)
                                {
                                    steps = 3;
                                    counter = 0f;
                                }
                            }
                            break;
                        case 3:
                            {
                                if (door.transform.position.x < point + door.transform.localScale.x * .9f)
                                    door.transform.position = new Vector3(door.transform.position.x + 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                                else
                                {
                                    takeButton = false;
                                    readyRet = true;
                                    steps = 1;
                                }
                            }
                            break;
                    }
                    /*if (door.transform.position.x > point)
                        door.transform.position = new Vector3(door.transform.position.x - 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
                    else
                    {
                        takeButton = false;
                        readyRet = true;
                    }*/
                }
            }
            else if(isPlita)
            {
                if (door.transform.position.y > 3.5)
                    door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 1f * Time.deltaTime, door.transform.position.z);
                else
                {
                    takeButton = false;
                    readyRet = true;
                }
            }
        }
    }
}
