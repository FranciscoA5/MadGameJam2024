using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacoDeVidro : Enemy
{

    private Vector3 targetPos;



    protected override void Attack()
    {
       
    }

    protected override void Die()
    {
        
    }

    protected override void Wonder()
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        rb2d.velocity = direction * speed * Time.fixedDeltaTime;

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = GetRandomPointInRange();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GetRandomPointInRange();
    }

    protected override void AttackToWonder()
    {
        targetPos = GetRandomPointInRange();
    }

    private Vector3 GetRandomPointInRange()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        Vector3 pos = transform.position + randomDirection * Random.Range(0f, range);
        Debug.Log("New target position chosen: " + pos);
        return pos;
    }
}
