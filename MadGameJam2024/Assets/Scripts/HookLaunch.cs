using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum HookState
{
    Put,
    Coming,
    Going
}
public class HookLaunch : MonoBehaviour
{
   

    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private float hookSpeed;
    [SerializeField] private float hookDistance;

    HookState currState = HookState.Put;

    private GameObject hook;

    public bool isRotating = false;
    private void Update()
    {
        switch (currState)
        {
            case HookState.Put:
                if (Input.GetMouseButtonDown(0) && !isRotating )
                {
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0;

                    Vector2 direction = (mousePos - transform.position).normalized;

                    hook = Instantiate(hookPrefab, transform.position, Quaternion.identity,transform);
                    hook.GetComponent<Rigidbody2D>().velocity = direction * hookSpeed;

                    currState = HookState.Going;
                }

                break;
            case HookState.Going:
               
                if (Vector2.Distance(hook.transform.position, transform.position) >= hookDistance)
                {
                    
                    hook.GetComponent<Rigidbody2D>().velocity *= -1;
                    currState = HookState.Coming;
                }
                break;
            case HookState.Coming:
                if ( Vector2.Distance(transform.position, hook.transform.position) <= 1)
                {
                    if(hook.transform.childCount > 0)
                    {
                        hook.transform.GetChild(0).parent = transform;
                    }

                    currState = HookState.Put;
                    Destroy(hook);
                    GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                   
                }
                break;
        }
       
    }

   

}
