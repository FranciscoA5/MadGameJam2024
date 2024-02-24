using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlhoDaAguia : Enemy
{
    private Vector3 targetPos;
    [SerializeField] private float shootingRange;
    private const float sRangeWindow = 0.2f;

    private float shootingTimer = 0;
    [SerializeField] private float shootInterval;

    protected override void Attack()
    {
        float playerDist = Vector3.Distance(playerTransform.position, transform.position);

        if (playerDist > shootingRange + sRangeWindow)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            rb2d.velocity = direction * speed * Time.fixedDeltaTime;
        }
        else if (playerDist < shootingRange - sRangeWindow)
        {
            Vector3 direction = (transform.position - playerTransform.position).normalized;
            rb2d.velocity = direction * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb2d.velocity = Vector3.zero;
            shootingTimer += Time.deltaTime;
            if (shootingTimer >= shootInterval)
            {
                Debug.Log("Shoot!");
                shootingTimer = 0;
            }
        }
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

    private Vector3 GetRandomPointInRange()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0);
        Vector3 pos = transform.position + randomDirection * Random.Range(0f, range);
        Debug.Log("New target position chosen: " + pos);
        return pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, shootingRange);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
