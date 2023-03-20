using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class ButtonCustom : MonoBehaviour
{
    public bool pressed { get; private set; }

    Rigidbody2D player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().rb;
    }
    private void OnMouseDown()
    {
        pressed = true;
        Debug.Log("pressed");
    }
    private void OnMouseDrag()
    {
        pressed = true;
        Debug.Log("pressed");
    }
    private void OnMouseUp()
    {
        Debug.Log("Unpressed");
        
        pressed = false;
        player.velocity = new Vector3(0, player.velocity.y, 0);
    }
    private void Update()
    {
        if (!pressed)
        {
            player.velocity = new Vector3(0, player.velocity.y, 0);
        }
    }

}
