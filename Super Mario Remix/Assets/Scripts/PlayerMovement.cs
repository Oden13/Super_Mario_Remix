﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

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
    float invincibleTimer;
    bool isInvincible = false;
    Vector3 characterScale;
    float characterScaleX;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lives = 2;
        SetLivesText();
        count = 0;
        SetCountText();
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Store horizontal input in float mx;

        mx = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 800);
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
        new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), Environment);

        invincibleTimer -= Time.deltaTime;
        if (invincibleTimer < 0)
        {
            isInvincible = false;
        }

        if (Input.GetAxis("Horizontal") < 0) {
            characterScale.x = -characterScaleX;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;

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
            invincibleTimer = 3f;
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
          if (collision.gameObject.tag == "Lvl2:End")
        {
            SceneManager.LoadScene (2);
        }


        if (collision.gameObject.tag == "PitFall")
        {
                        Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Lvl2:Start")
        {
            SceneManager.LoadScene (3);
        }
      
        if (collision.gameObject.tag == "Lvl1:End")
        {
            transform.position = new Vector3(150.0f, 5f,0f); 
        }
        if (collision.gameObject.tag == "HitBox")
        {
            rb.AddForce(Vector2.up * 800);
        }
        if (collision.gameObject.tag == "Pick Up")
        {
            Destroy(collision.gameObject);
            Debug.Log("You got 1");
            count = count + 100;
            SetCountText();
        }
        if (collision.gameObject.tag == "Power Up" && !isInvincible)
        {
            Destroy(collision.gameObject);
            isInvincible = true;
            invincibleTimer = 5f;
            Debug.Log("Can't Touch Me");
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer == 0)
            {
                isInvincible = false;
            }
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
