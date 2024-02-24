using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projetil : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    Vector3 direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void StartPrefab(Vector3 dir)
    {
        direction = dir;

        rb2d.velocity = direction * speed * Time.deltaTime;
       float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }

}
