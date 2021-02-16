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
    private int count;
    public Text countText;
    float invincibleTimer = 1.5f;
    bool isInvincible = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lives = 2;
        SetLivesText();
        count = 0;
        SetCountText();
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

        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer < 0)
        {
            isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        // Use the stored floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(mx * speed, rb.velocity.y);
        rb.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy" && !isInvincible)
        {
            lives = lives - 1;
            SetLivesText();
            isInvincible = true;
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer == 0)
            {
                isInvincible = false;
            }

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
            rb.AddForce(Vector2.up * 600);
        }
        if (collision.gameObject.tag == "Pick Up")
        {
            Destroy(collision.gameObject);
            Debug.Log("You got 1");
            count = count + 1;
            SetCountText();
        }
        if (collision.gameObject.tag == "Power Up")
        {
            Destroy(collision.gameObject);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString ();
        if (count >= 300)
        {
            Debug.Log("You win");
        }
    }
}
