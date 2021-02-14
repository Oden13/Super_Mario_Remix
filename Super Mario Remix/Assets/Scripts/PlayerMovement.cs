using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    float mx;
    public bool isGrounded;
    public LayerMask Environment;

    public Rigidbody2D rb;
    private int lives;
    public Text livesText;
    public GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lives = 2;
        SetLivesText();
    }

    // Update is called once per frame
    void Update()
    {
        //Store horizontal input in float mx;

        mx = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 500);
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
        new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), Environment);


    }

    void FixedUpdate()
    {
        // Use the stored floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(mx * speed, rb.velocity.y);
        rb.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
        }
        if(lives == 0)
        {
            //Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "HitBox")
        {
            rb.AddForce(Vector2.up * 800);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }
}
