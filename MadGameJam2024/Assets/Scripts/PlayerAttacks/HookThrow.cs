using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HookThrow : MonoBehaviour
{

    GameObject enemyHooked;
    Rigidbody2D rb;
    [SerializeField] private float hookDistance = 5;
    [SerializeField] private float pullingSpeed = 5;
    [SerializeField] private float throwingSpeed = 2;
    private bool isHooked = false;
    private bool isRotating = false;
    private Vector2 mouseWorldPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseWorldPos.z = 0f; // zero z.

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
            Rotate();
        }
    
    
    }

    private void PullEnemy(GameObject enemy)
    {
        if (isHooked && Vector2.Distance(transform.position, enemy.transform.position) > 2.5f)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, pullingSpeed * Time.deltaTime);
        }
        
        else if(Vector2.Distance(transform.position, enemy.transform.position) < 2.5f)
        {
            //isHooked = false;
            //rb.constraints = RigidbodyConstraints2D.None;
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
        
        if(hit.collider ==  null)
        {
            yield return new WaitForSeconds(throwingSpeed);
            rb.constraints = RigidbodyConstraints2D.None;
        }

       
    }

    private void Rotate()
    {

    }


}
