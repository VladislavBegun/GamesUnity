using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMove : Trigger
{
    /*static int hpCount;
    public Text hp;*/

    //public CharacterController controller;
    public float Speed = 2f;
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    //public AudioClip shotSFX;
    //public AudioSource sourceAud;
    //public Animator gun;
    //public Animator walkShake;

    Vector3 velocity;
    bool isGrounded;
    bool walkAud = true;
    public int n = 0;

    //public List<Collider> helpers;
    public GameObject r;
    //private bool onWall = false;
    //private bool isJump = false;

    void Start()
    {
        helpers = new List<Collider>();
        //isIt = false;
        /*hp.text = "100";
        hpCount = 100;*/
    }

    //Collider colMove;
    //Vector3 newPoint;
    // Update is called once per frame
    void Update()
    {

        /*if (!isJump)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Collider col = null;
                for (int i = 0; i < helpers.Count; i++)
                {
                    if(Quaternion.Angle(transform.rotation, helpers[i].transform.rotation) < 50)
                    {
                        if (col == null) col = helpers[i];
                        else if (helpers[i].bounds.max.y > col.bounds.max.y) col = helpers[i];
                    }
                }
                if(col != null)
                {
                    Ray ray = new Ray(
                        new Vector3(transform.position.x, col.bounds.max.y - .1f, transform.position.z),
                        new Vector3(col.bounds.center.x, col.bounds.max.y - .1f, col.bounds.center.z) - new Vector3(transform.position.x, col.bounds.max.y - .1f, transform.position.z)
                        );
                    RaycastHit hit;
                    if(Physics.Raycast(ray, out hit, 2.5f))
                    {
                        colMove = col;
                        newPoint = new Vector3(hit.point.x, col.bounds.max.y, hit.point.z) - col.transform.forward * .3f + Vector3.up * -2.17f;
                        isJump = true;
                        controller.enabled = false;
                        if(!onWall)
                        {
                            onWall = true;
                        }
                    }
                }
            }
        }

        if(isJump)
        {
            if(Vector3.Distance(transform.position, newPoint) > .02f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, colMove.transform.rotation, 5f * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, newPoint, 5f * Time.deltaTime);
            }
            else
            {
                isJump = false;
            }
        }*/

        // Walk
        if (!r.GetComponent<Trigger>().getOnWall())
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.Mouse0))
            {
                gun.SetBool("Walk", true);
                walkShake.SetBool("walk", true);
            }
            else
            {
                gun.SetBool("Walk", false);
                walkShake.SetBool("walk", false);
            }
            //


            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Walk

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (x != 0f || z != 0)
            {
                if (walkAud || !sourceAudP.isPlaying)
                {
                    sourceAudP.PlayOneShot(shotSFXp);
                    walkAud = false;
                }
            }
            else
            {
                walkAud = true;
                sourceAudP.Stop();
            }

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * Speed * Time.deltaTime);

            //

            // Прыжок

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            //

            // гравитация
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
            //
        }
    }
}
