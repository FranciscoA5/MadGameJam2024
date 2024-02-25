using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HookThrow : MonoBehaviour
{

    GameObject enemyHooked;
    Rigidbody2D rb;

    private Vector2 mouseWorldPos;
    private Vector2 velocityRotating;
    private Vector2 direction;

    [SerializeField] private float hookDistance = 5;
    [SerializeField] private float pullingSpeed = 5;
    [SerializeField] private float grabingSpeed = 2;
    [SerializeField] private int throwingSpeed = 5;
    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] private float timer = 2;

    private bool isHooked = false;
    private bool isRotating = false;
    private bool hasthrow = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {     
            StartCoroutine(CastRay());
        }

        if(enemyHooked != null)
        {
            PullEnemy(enemyHooked);
        }

        if(isRotating == true)
        {
            Rotate(enemyHooked);
        } 
    }

    private void PullEnemy(GameObject enemy)
    {
        if (isHooked && Vector2.Distance(transform.position, enemy.transform.position) > 2.5f)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, pullingSpeed * Time.deltaTime);
        }
        
        else if(Vector2.Distance(transform.position, enemy.transform.position) < 2.5f && isHooked)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            enemy.transform.parent = transform;
            isRotating = true;
            enemy.GetComponent<Enemy>().IsRotating();
            GetComponent<PlayerMovement>().ChangeSpeed(throwingSpeed);
        }
    }

    IEnumerator CastRay()
    {
        direction = mouseWorldPos - (Vector2)transform.position; // Calculate direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, hookDistance);
        Debug.DrawRay(transform.position, direction.normalized * hookDistance, Color.red, 1f);

        Debug.Log("hit: " + hit);
       

        // If it hits something...
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            enemyHooked = hit.collider.gameObject;
            Debug.Log("hit collider: " + hit.collider.gameObject);
            if (Vector2.Distance(transform.position, enemyHooked.transform.position) > 2.5f)
            {
                isHooked = true;
                Debug.Log("ishooked: " + isHooked);
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
            }
            yield return new WaitForSeconds(grabingSpeed);
        }
     
        else
        {
            yield return new WaitForSeconds(grabingSpeed);
            rb.constraints = RigidbodyConstraints2D.None;
        }

       
    }
    
    private void Rotate(GameObject enemy)
    {

        enemy.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed);
        //rb.constraints = RigidbodyConstraints2D.FreezePosition;
        TimerToSpeedUp();

        if(Input.GetMouseButtonDown(0))
        {
            enemy.transform.parent = null;

            Vector2 distanceRotating = transform.position - enemy.transform.position;
            velocityRotating.x = distanceRotating.y;
            velocityRotating.y = -distanceRotating.x;

            rb.constraints = RigidbodyConstraints2D.None;

            isRotating = false;
            isHooked = false;
            hasthrow = true;

            enemy.GetComponent<Enemy>().HasBeenThrowed();
            enemy.GetComponent<Enemy>().Speed(rotationSpeed * distanceRotating.magnitude);
            enemy.GetComponent<Enemy>().Direction(velocityRotating);

            GetComponent<PlayerMovement>().ChangeSpeed(10);
            rotationSpeed = 2f;
        }
    }

    private void TimerToSpeedUp()
    {
        timer -= Time.deltaTime;
        if (timer > 0.1f && rotationSpeed < 5)
        {
            rotationSpeed += 0.005f;
        }
        else timer = 2f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, direction.normalized * hookDistance);
    }
}
