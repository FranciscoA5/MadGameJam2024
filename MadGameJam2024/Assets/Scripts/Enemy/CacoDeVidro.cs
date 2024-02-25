using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacoDeVidro : Enemy
{

    private Vector3 targetPos;
    private Vector3 spikeSpot;
    List<Vector3> spikeSpots = new List<Vector3>();
    List<GameObject> spikeList = new List<GameObject>();

    [SerializeField] private float timeToDestroy = 5f;
    private bool isAttacking = false;

    [SerializeField] GameObject spike;

    void Start()
    {
        targetPos = GetRandomPointInRange();
    }

    private void Update()
    {
        base.Update();

        if (isAttacking)
        {
            timeToDestroy -= Time.deltaTime;
            if (timeToDestroy < 0.1f)
            {
                Debug.Log("Destroyed");
                DestroySpikesAndSpots();
                timeToDestroy = 5f;
                rb2d.constraints = RigidbodyConstraints2D.None;
                isAttacking = false;
            }
        }
    }

    protected override void Attack()
    {
        StartAttack();
        isAttacking = true;
    }

    protected override void Die()
    {

    }

    protected override void Wonder()
    {
        Debug.Log("Wondering");
        Vector3 direction = (targetPos - transform.position).normalized;
        rb2d.velocity = direction * speed * Time.fixedDeltaTime;

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = GetRandomPointInRange();
        }
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
        return pos;
    }

    private void StartAttack()
    {
        if (!isAttacking)
        {
            for (int i = 0; i < 20; i++)
            {
                spikeSpot = GetRandomPointInRange();
                Debug.Log("spot: " + spikeSpot);
                spikeSpots.Add(spikeSpot);
            }
            StartCoroutine(InstantiateSpikes());
        }
    }

    IEnumerator InstantiateSpikes()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezePosition; ;
        yield return new WaitForSeconds(1.5f);
        foreach (Vector3 point in spikeSpots)
        {
            GameObject go = Instantiate(spike, point, Quaternion.identity);
            spikeList.Add(go);
        }
    }

    private void DestroySpikesAndSpots()
    {
        foreach(GameObject spike in spikeList)
        {
            spikeList.Remove(spike);
            Destroy(spike);
        }

        foreach (Vector3 spot in spikeSpots)
        {
            spikeSpots.Remove(spot);
        }
        //spikeList.RemoveAll(spike => spike == null);
        Debug.Log("spikeList: " + spikeList.Count);
    }
}
