using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject model;
    public Animator modelEnemy;
    public Transform player;
    private Transform point;
    public Transform pointTo;
    public Transform pointFrom;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public GameObject fireBole;
    public Transform bullet;
    public int bulletForce = 5000;
    public float bulletTimer = 1f;
    public float bulletNewTimer = 1f;
    public float range = 15f;
    public AudioClip fire;
    RaycastHit hit;
    private NavMeshAgent agent;
    public bool hitAt;
    public float hp;
    int switch_on;
    bool[] massBool = new bool[4] { true, true , true , true };
    public Text score;
    static int scoreCount;
    public string nameShootPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        point = pointTo;
        hitAt = false;
        hp = 100;
        scoreCount = 0;
    }

    void Update()
    {
        if (bulletTimer < 1.1f)
        {
            fireBole.SetActive(true);
        }
        if (bulletTimer < 1)
        {
            fireBole.SetActive(false);
        }
        if (hp == 0)
        {
            score.text = (++scoreCount).ToString();
            enemy.SetActive(false);
            model.SetActive(true);
            Instantiate(model, transform.position, transform.rotation);
            hp = 100;
            Invoke("Wait", 5f);
        }
        else
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            {
                if (hit.collider.tag == "Player" || hitAt)
                {
                    modelEnemy.SetBool("Shoot", true);
                    transform.LookAt(player.transform.position);
                    agent.destination = Vector3.MoveTowards(transform.position, player.position, 0);
                    hitAt = false;
                    if (bulletTimer <= 0)
                    {
                        Transform bulletInstance = (Transform)Instantiate(bullet, GameObject.Find(nameShootPoint).transform.position, Quaternion.identity);
                        bulletInstance.GetComponent<Rigidbody>().AddForce(transform.forward * bulletForce);
                        GetComponent<AudioSource>().PlayOneShot(fire);
                        Destroy(bulletInstance.gameObject, 0.2f);
                        bulletTimer = bulletNewTimer;
                    }
                    else
                    {
                        bulletTimer -= Time.deltaTime;
                    }
                }
                else
                {
                    modelEnemy.SetBool("Shoot", false);
                    if (Vector3.Distance(agent.transform.position, point.position) < 0.5)
                    {
                        point = (point == pointTo) ? pointFrom : pointTo;
                    }
                    agent.destination = point.position;
                }
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plita")
        {
            hp = 0;
        }
    }

    void Wait()
    {
        float[] mass = new float[4]
            {
                Vector3.Distance(player.position, point1.position),
                Vector3.Distance(player.position, point2.position),
                Vector3.Distance(player.position, point3.position),
                Vector3.Distance(player.position, point4.position)
            };
        double max = -1;
        switch_on = -1;
        for (int i = 3; i > 0; --i)
        {
            if (mass[i] > max && massBool[i])
            {
                max = mass[i];
                switch_on = i;
            }
        }
        massBool[switch_on] = false;
        switch (switch_on)
        {
            case 0:
                enemy.transform.position = point1.transform.position;
                break;
            case 1:
                enemy.transform.position = point2.transform.position;
                break;
            case 2:
                enemy.transform.position = point3.transform.position;
                break;
            case 3:
                enemy.transform.position = point4.transform.position;
                break;
            case -1:
                break;
        }
        enemy.SetActive(true);
        Invoke("WaitPosition", 5f);
    }

    void WaitPosition()
    {
        massBool[switch_on] = true;
    }
}
