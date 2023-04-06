using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Trigger
{
    public float damage = 21f;
    public float fireRate = 10f;
    public float range = 15f;
    public ParticleSystem muzzleFlash;
    public Animator recoil;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioSource sourceAud;
    private Enemy enemy;
    private Button button;
    public GameObject hitEffect;

    public Camera camera;
    private float nextFire = 0f;

    public GameObject r;

    void Update()
    {
        /*if(takeButton)
        {
            if (door.transform.position.x > -6f)
                door.transform.position = new Vector3(door.transform.position.x - 1f * Time.deltaTime, door.transform.position.y, door.transform.position.z);
            else
                takeButton = false;
        }*/

        if (!r.GetComponent<Trigger>().getOnWall())
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + 1f / fireRate;
                    Shoot();
                }
                recoil.SetBool("Shoot", true);
            }
            else
            {
                recoil.SetBool("Shoot", false);
            }

            if (Input.GetKey(KeyCode.E))
            {
                OpenDoor();
            }
        }
    }
    void Shoot ()
    {
        sourceAud.PlayOneShot(shotSFX);
        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            //if (hit.collider.CompareTag("Enemy"))
            //{
            //    GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //    Destroy(impact, 2f);
            //}

            enemy = hit.collider.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.hitAt = true;
                enemy.hp -= 10;
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 155f);
            }
        }
    }

    void OpenDoor()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            /*if (hit.collider.CompareTag("Button") && !takeButton)
            {
                takeButton = true;
                //GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                //Destroy(impact, 2f);
            }*/

            button = hit.collider.gameObject.GetComponent<Button>();
            if (button)
            {
                button.takeButton = true;
            }
        }
    }
}
