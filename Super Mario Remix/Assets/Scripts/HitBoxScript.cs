using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    public GameObject deathEffect;
    public AudioClip death;
    public AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            soundSource.clip = death;
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            Destroy(GameObject.FindWithTag("Enemy"),01f);
            Destroy(gameObject,0.1f);
            Debug.Log ("Enemy down");
          
            
        }
    }
}
