using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    int speed = 10;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("MovingLeft");
            rigidbody.MovePosition(rigidbody.position + Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("MovingRight");
            rigidbody.MovePosition(rigidbody.position + Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("MovingUp");
            rigidbody.MovePosition(rigidbody.position + Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("MovingDown");
            rigidbody.MovePosition(rigidbody.position + Vector2.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rigidbody.MovePosition(rigidbody.position + (Vector2.up + Vector2.right).normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rigidbody.MovePosition(rigidbody.position + (Vector2.up + Vector2.left).normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            rigidbody.MovePosition(rigidbody.position + (Vector2.down + Vector2.left).normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rigidbody.MovePosition(rigidbody.position + (Vector2.down + Vector2.right).normalized * speed * Time.deltaTime);
        }
    }
}