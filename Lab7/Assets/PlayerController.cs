using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed;
    public Rigidbody2D rb;
    private bool facingRight;

    public float jumpForce;
    bool isJumping;

    public float multiplier = 1.4f;
    bool powerUp;


    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * move, rb.velocity.y);
        Flip(move);
        Jump();

    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            rb.velocity = Vector2.zero;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "malo")
        {
            if (powerUp)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(rb.gameObject);
            }

        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            //Instantiate(pickupEffect, transform.position, transform.rotation);
            Pickup(collision);

        }
    }

    void Pickup(Collider2D player)
    {
        rb.transform.localScale *= multiplier;
        Destroy(player.gameObject);
        powerUp = true;
    }
}
