using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mysterybox : MonoBehaviour
{public GameObject[] items;
    public AudioClip spawn;
    public AudioSource soundSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        soundSource.clip = spawn;
        soundSource.Play();
        if (col.gameObject.tag == "Player")
        {
            int rand = Random.Range(0, items.Length);
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-1,1), UnityEngine.Random.Range(-1,1), 0);
            Instantiate(items[rand], transform.position + Vector3.up, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
