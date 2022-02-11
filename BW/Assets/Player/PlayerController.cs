using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 30f;
    private float jump = 10f;
    private int move=0;
    private bool grounding = false;
    private float grip = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < 10)
        {
            float currentSpeed = speed - rb.velocity.magnitude;
            rb.AddForce(new Vector2(currentSpeed*move,0));
        }
        if (move == 0)
        {
            Debug.Log(rb.velocity.x);
            rb.AddForce(new Vector2(rb.velocity.x * -grip,0));
        }
    }
    // Update is called once per frame
    void Update()
    {
        move = 0;
        if (Input.GetKey(KeyCode.A))
        {
            move += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounding==true)
        {
            rb.AddForce(new Vector2(0,jump),  ForceMode2D.Impulse);
        }
        if (grounding == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounding = false;
        }
    }
}
