using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    ButtonCustom[] buttons;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private bool grounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        buttons = FindObjectsOfType<ButtonCustom>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            grounded = true;
        }
    }
    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].pressed)
            {
                switch (buttons[i].tag)
                {
                    case "Down":
                        break;
                    case "Up":
                        if (grounded)
                        {
                            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                            grounded = false;
                        }
                        break;
                    case "Left":
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                        break;
                    case "Right":
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                        break;
                }
            } 
        }
    }
}

