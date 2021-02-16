using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    float timer;
    public float changeTime = 2.0f;
    int direction = 1;
    public int health;
    public GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        if (health <= 0) 
        {
            //play death effect.
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;
        position.x = position.x + Time.deltaTime * speed * direction;
        rb.MovePosition(position);
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;
    }
}
