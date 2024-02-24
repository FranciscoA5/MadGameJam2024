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

    [SerializeField] private float hookDistance = 5;
    [SerializeField] private float pullingSpeed = 5;
    [SerializeField] private float throwingSpeed = 2;
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
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
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
        }
       
    }

    IEnumerator CastRay()
    {
        Vector2 direction = mouseWorldPos - (Vector2)transform.position; // Calculate direction
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, hookDistance);
        Debug.DrawRay(transform.position, direction.normalized * hookDistance, Color.red, 1f);

        // If it hits something...
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            isHooked = true;
            enemyHooked = hit.collider.gameObject;
            Debug.Log("ENEMY");
            yield return new WaitForSeconds(throwingSpeed);
        }
     
        else
        {
            yield return new WaitForSeconds(throwingSpeed);
            rb.constraints = RigidbodyConstraints2D.None;
        }

       
    }

    //private void TryToRotate() {
    //    if (Input.GetMouseButton(0))
    //    {
    //        isRotating = true;
    //    }
    //    else isHooked = false;
    //}
    

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

            enemy.GetComponent<EnemyThrowed>().HasBeenThrowed(hasthrow);
            enemy.GetComponent<EnemyThrowed>().Speed(rotationSpeed * distanceRotating.magnitude);
            enemy.GetComponent<EnemyThrowed>().Direction(velocityRotating);

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
}
