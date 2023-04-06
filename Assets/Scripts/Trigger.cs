using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    static int hpCount;
    public Text hp;
    public static int isIt;
    public List<Collider> helpers;

    public Animator gun;
    public Animator walkShake;

    private bool onWall = false;
    private bool isJump = false;
    private bool isUp = false;
    public CharacterController controller;

    public bool chek = false;
    public Vector3 chec;

    public GameObject gam;
    public GameObject cam;

    public float h;

    public AudioClip shotSFXp;
    public AudioSource sourceAudP;

    void Start()
    {
        hp.text = "100";
        hpCount = 100;
    }
    Collider colMove;
    Vector3 newPoint;
    void Update()
    {
        if (hpCount <= 0)
        {
            hpCount = 100;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("TheEnd");
        }
        if(hpCount <= 50)
        {
            hp.color = Color.red;
        }
        if (!isJump && !isUp)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if(onWall && !Physics.Raycast(transform.position + Vector3.up * 2.2f, cam.transform.position, 1f))
                {
                    isUp = true;
                    newPoint = transform.position + Vector3.up * .7f + transform.forward * .5f;
                }
                else
                {
                    Collider col = null;
                    for (int i = 0; i < helpers.Count; i++)
                    {
                        if (Quaternion.Angle(transform.rotation, helpers[i].transform.rotation) < 50)
                        {
                            if (col == null) col = helpers[i];
                            else if (helpers[i].bounds.max.y > col.bounds.max.y) col = helpers[i];
                        }
                    }
                    if (col != null)
                    {
                        Ray ray = new Ray(
                            new Vector3(transform.position.x, col.bounds.max.y - .1f, transform.position.z),
                            new Vector3(col.bounds.center.x, col.bounds.max.y - .1f, col.bounds.center.z) - new Vector3(transform.position.x, col.bounds.max.y - .1f, transform.position.z)
                            );
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, 2.5f))
                        {
                            colMove = col;
                            newPoint = new Vector3(hit.point.x, col.bounds.max.y, hit.point.z) - col.transform.forward * .14f + Vector3.up * -.2f;
                            isJump = true;
                            controller.enabled = false;
                            if (!onWall)
                            {
                                onWall = true;
                                gun.SetBool("Walk", false);
                                walkShake.SetBool("walk", false);
                                sourceAudP.Stop();
                                //gun.SetBool("toUp", true);
                            }
                        }
                    }
                }
            }
        }

        if (isJump && !isUp)
        {
            if (Vector3.Distance(transform.position, newPoint) > .02f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, colMove.transform.rotation, 5f * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, newPoint, 5f * Time.deltaTime);
            }
            else
            {
                isJump = false;
            }
        }
        else if (onWall && isUp)
        {
            if (Vector3.Distance(transform.position, newPoint) > .02f)
            {
                if(transform.position.y < newPoint.y - .1f)
                {
                    transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, newPoint.y, transform.position.z), 10f * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.Slerp(transform.position, newPoint, 5f * Time.deltaTime);
                }
            }
            else
            {
                controller.enabled = true;
                isJump = false;
                onWall = false;
                isUp = false;
                //gun.SetBool("fromUp", true);
            }
        }

        if (onWall && !isJump && !isUp)
        {
            h = Input.GetAxis("Horizontal");
            if(h != 0)
            {
                if(colMove != null)
                {
                    float dir = (h < 0) ? .1f : -.1f;
                    RaycastHit hit;
                    chec = transform.position + transform.up * .15f + transform.forward * dir;
                    gam.transform.position = chec;

                    if (Physics.Raycast(chec, cam.transform.position, out hit, .1f))
                    {
                        chek = true;
                        if (hit.collider == colMove)
                        {
                            transform.position += transform.forward * -h * Time.deltaTime;
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NextLevel" && other.isTrigger)
        {
            SceneManager.LoadScene("level2");
        }
        if (other.tag == "Helpers" && other.isTrigger)
        {
            int index = helpers.FindIndex(x => x.gameObject == other.gameObject);
            if (index == -1) helpers.Add(other);
        }
        if (other.tag == "Bullet")
        {
            hpCount -= 5;
            hp.text = hpCount.ToString();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Helpers" && other.isTrigger)
        {
            int index = helpers.FindIndex(x => x.gameObject == other.gameObject);
            if (index != -1) helpers.RemoveAt(index);
        }
    }

    public bool getOnWall()
    {
        return onWall;
    }
}
