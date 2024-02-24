using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private enum State
    {
        Wonder,
        Attack,
        Thrown
    }

    [SerializeField] protected float range;
    [SerializeField] protected float speed;
    [SerializeField] protected float Rotspeed;

    protected Transform playerTransform;
    protected Rigidbody2D rb2d;

    Vector2 direction;

    private State currState = State.Wonder;

    protected abstract void Wonder();

    protected abstract void Attack();

    protected virtual void Throw()
    {
        currState = State.Thrown;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    protected abstract void Die();

    protected virtual void WonderToAttack() { }

    protected virtual void AttackToWonder() { }

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (currState)
        {
            case State.Wonder:
                Wonder();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Thrown:
                Debug.Log("throwed");
                Throwed(direction);
                break;
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= range)
        {
            if (currState == State.Wonder)
            {
                WonderToAttack();
                currState = State.Attack;
                Debug.Log("Changed enemy state to ATTACK");
            }
        }
        else
        {
           if (currState == State.Attack)
           {
               AttackToWonder();
               currState = State.Wonder;
               Debug.Log("Changed enemy state to WONDER");
           }
        }
    }

    private void Throwed(Vector2 direction)
    {
        if (currState == State.Thrown && direction != null)
        {
            transform.Translate(direction * Rotspeed * Time.deltaTime, Space.World);

            //rb2d.velocity = direction * speed * Time.fixedDeltaTime;
            Debug.DrawLine(transform.position, (Vector2)transform.position + direction);
        }
    }

    public void HasBeenThrowed()
    {
        currState = State.Thrown;
    }

    public void Direction(Vector2 _direction)
    {
        direction = _direction;
    }

    public void Speed(float _speed)
    {
        Rotspeed = _speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
