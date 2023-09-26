using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private float speed = 5f;
    private Vector2 position;

    [SerializeField] private Rigidbody2D rb;

    private bool left, right, up, down;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move = Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move = Vector2.down;
        }
        rb.velocity = move * speed;
    }

    private void FixedUpdate()
    {
        
    }
}
