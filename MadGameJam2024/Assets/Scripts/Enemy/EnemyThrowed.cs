using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowed : MonoBehaviour
{

    private bool hasBeenThrowed = false;
    private float speed;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Throwed(direction);
    }

    private void Throwed(Vector2 direction)
    {
        if (hasBeenThrowed && direction != null)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            Debug.DrawLine(transform.position, (Vector2)transform.position + direction);
        }
    }

    public void HasBeenThrowed(bool _hasBeenThrowed)
    {
        hasBeenThrowed = _hasBeenThrowed;
    }

    public void Direction(Vector2 _direction)
    {
        direction = _direction;
    }

    public void Speed(float _speed)
    {
        speed = _speed;
    }
}
