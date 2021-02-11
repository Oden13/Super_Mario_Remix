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
    public LayerMask groundLayer;

    public Rigidbody2D rb;
    bool isInvincible;
    float invincibleTimer;
    public float timeInvincible = 2.0f;
    private int count;
    public Text countText;
    public Text winText;
    public Text livesText;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        count = 0;
        SetCountText ();
        winText.text = "";
        lives = 3;
        SetLivesText();

    }

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * 500);
        }

        isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
        new Vector2(transform.position.x + 0.5f, transform.position.y - 0.51f), groundLayer);


    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(mx * speed, rb.velocity.y);
        rb.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PickUp")
        {
            Destroy(col.gameObject);
            Debug.Log("Picked Up");
            count = count + 100;
            SetCountText ();
        }
        if (col.gameObject.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText ();
        }
            if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            lives = lives - 1;
            SetLivesText();
        }
        if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString ();
        if (count >= 300)
        {
            winText.text = "You Win";
        }
    }
    
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString ();
    }
}
