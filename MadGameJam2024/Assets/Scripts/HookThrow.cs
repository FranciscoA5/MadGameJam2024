using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HookThrow : MonoBehaviour
{

    GameObject enemyHooked;
    [SerializeField] private float hookDistance = 5;
    [SerializeField] private float speed = 5;
    private bool isHooked = false;
    private Vector3 mouseWorldPos;
    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; // zero z.

        if (Input.GetMouseButton(0))
        {
            CastRay();
        }

        if(enemyHooked != null)
        {
            PullEnemy(enemyHooked);
        }
    
    
    }

    private void PullEnemy(GameObject enemy)
    {
        if (isHooked && Vector2.Distance(transform.position, enemy.transform.position) > 2.5f)
        {
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, transform.position, speed * Time.deltaTime);
        }
        
        else if(Vector2.Distance(transform.position, enemy.transform.position) < 2.5f)
        {
            isHooked = false;
        }
       
    }

    private void CastRay()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, mouseWorldPos, hookDistance);
        Debug.DrawRay(transform.position, mouseWorldPos * hookDistance, Color.red, 1f);
       
        // If it hits something...
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            isHooked = true;
            enemyHooked = hit.collider.gameObject;
            Debug.Log("ENEMY");
        }
    }

}
