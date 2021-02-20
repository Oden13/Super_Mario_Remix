using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;
    public float speed;
    float mx;
    public bool isGrounded;
    public LayerMask Environment;

    public Rigidbody2D rb;
    public static int lives;
    public Text livesText;
    public GameObject deathEffect;
    private int count;
    public Text countText;
    float invincibleTimer;
    bool isInvincible = false;
    Vector3 characterScale;
    float characterScaleX;

    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip death;
    public AudioClip pickup;
    public AudioClip invincible;
    public AudioSource soundSource;




    // Start is called before the first frame update
    void Start()
    {
      anim = GetComponent<Animator> ();
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
//animation state controller;
if (Input.GetKeyUp (KeyCode.Space))
{
    anim.SetInteger("State",0);
}


if (Input.GetKeyDown (KeyCode.Space))
{
    anim.SetInteger("State",2);
}

if (Input.GetKeyUp (KeyCode.RightArrow))
{
    anim.SetInteger("State",0);
}
if (Input.GetKeyUp (KeyCode.LeftArrow))
{
    anim.SetInteger("State",0);
}  


if (Input.GetKeyDown (KeyCode.RightArrow))
{
    anim.SetInteger("State",1);
}
if (Input.GetKeyDown (KeyCode.LeftArrow))
{
    anim.SetInteger("State",1);
}        
        //Store horizontal input in float mx;

        mx = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 800);
            if (!soundSource.isPlaying)
            {
                soundSource.clip = jump;
                soundSource.Play();
            }
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.2f, transform.position.y - 0.2f),
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

        if (lives <=0)
        {
            Debug.Log("You Have Died!");
            SceneManager.LoadScene("LoseScreen");
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
            soundSource.clip = hurt;
            soundSource.Play();
            lives = lives - 1;
            SetLivesText();
            //isInvincible = true;
            //invincibleTimer = 2f;
        }
        if (col.gameObject.tag == "Enemy" && isInvincible)
        {
            Destroy(col.gameObject, 0.2f);
            //if (invincibleTimer == 0)
            //{
              //isInvincible = false;
            //}

        }
            if (lives == 0)
        {
            soundSource.clip = death;
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            //Destroy(this.gameObject);
     
            Destroy(gameObject, 0.3f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.gameObject.tag == "Lvl2:End")
        {
            SceneManager.LoadScene (3);
        }



        if (collision.gameObject.tag == "TilemapDeath")
        {
            soundSource.clip = death;
            soundSource.Play();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            lives = 0;
           

           
        }
        if (collision.gameObject.tag == "Lvl2:Start")
        {
            SceneManager.LoadScene (2);
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
           
                soundSource.clip = pickup;
                soundSource.Play();
         
            Destroy(collision.gameObject);
            Debug.Log("You got 1");
            count = count + 100;
            SetCountText();
        }
        if (collision.gameObject.tag == "Power Up" && !isInvincible)
        {
            Destroy(collision.gameObject);
            isInvincible = true;
            invincibleTimer = 5.0f;
        
            Debug.Log("Can't Touch Me");
        }
        if (isInvincible)
        {
            soundSource.clip = invincible;
            invincibleTimer -= Time.deltaTime;
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
            }
            if (invincibleTimer == 0.0f)
            {
                isInvincible = false;
               
            }
        }
    }



    void SetLivesText()
    {
        livesText.text = "Lives " + lives.ToString();
    }

    void SetCountText()
    {
        countText.text = "Score " + count.ToString ();
        if (count >= 300)
        {
            Debug.Log("You win");
        }
    }
}
