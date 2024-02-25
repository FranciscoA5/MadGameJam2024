using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemy : MonoBehaviour
{

    private Vector2 velocityRotating;
    [SerializeField] private float hookDistance = 5;
    [SerializeField] private float pullingSpeed = 5;
    [SerializeField] private float grabingSpeed = 2;
    [SerializeField] private int throwingSpeed = 5;
    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] private float timer = 2;

    [SerializeField] private HookLaunch hookLaunch;
    void FixedUpdate()
    {

        if (transform.childCount > 0 && transform.GetChild(0).CompareTag("Enemy"))
        {
            GameObject enemy = transform.GetChild(0).gameObject;
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            enemy.transform.RotateAround(transform.position, Vector3.forward, 10);
            hookLaunch.isRotating = true;
           //TimerToSpeedUp();
           //
           if (Input.GetMouseButton(0))
           {
               enemy.transform.parent = null;
           
               Vector2 distanceRotating = transform.position - enemy.transform.position;
               velocityRotating.x = distanceRotating.y;
               velocityRotating.y = -distanceRotating.x;

                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
         
                enemy.GetComponent<Enemy>().HasBeenThrowed();
                enemy.GetComponent<Enemy>().Speed(rotationSpeed * distanceRotating.magnitude);
                enemy.GetComponent<Enemy>().Direction(velocityRotating);

                //GetComponent<PlayerMovement>().ChangeSpeed(10);
               rotationSpeed = 2f;

                StartCoroutine(LaunchHookTimer());
            
               
           }
        }
    }

    IEnumerator LaunchHookTimer()
    {
        yield return new WaitForSeconds(0.5f);
        hookLaunch.isRotating = false;
       
    }
}
