using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int speed = 3;
    Rigidbody2D rigidbody;
    [SerializeField] Camera camera;
    [SerializeField] PlayerManager playerManager;

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
    private void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
        camera.transform.position = Vector3.Lerp(camera.transform.position, newPos, 1f);
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rigidbody.MovePosition(rigidbody.position + Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rigidbody.MovePosition(rigidbody.position + Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.MovePosition(rigidbody.position + Vector2.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
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

    public void ChangeSpeed(int _speed)
    {
        speed = _speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.tag == "Wall1")
        {
            transform.position = Vector2.Lerp(transform.position, AreaController.Instance.CenterPos(), 2f);
            playerManager.TakeDamage();
        }

        if (collision.gameObject.tag == "Wall2")
        {
            transform.position = Vector2.Lerp(transform.position, AreaController.Instance.CenterPos(), 2f);
            playerManager.TakeDamage();
        }

        if (collision.gameObject.tag == "Wall3")
        {
            transform.position = Vector2.Lerp(transform.position, AreaController.Instance.CenterPos(), 2f);
            playerManager.TakeDamage();
        }

        if (collision.gameObject.tag == "Wall4")
        {
            transform.position = Vector2.Lerp(transform.position, AreaController.Instance.CenterPos(), 2f);
            playerManager.TakeDamage();
        }
    }
}
