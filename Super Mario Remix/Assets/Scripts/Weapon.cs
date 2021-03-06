﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public  float offset;
    public GameObject projectile;
    private float timeBtwShots;
    public float startTimeBtwShots;

    public AudioClip shoot;
    public AudioSource soundSource;
    //public Transform shotPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if (timeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Instantiate(projectile, transform.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
                soundSource.clip = shoot;
                soundSource.Play();
            }
        }
        else 
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
