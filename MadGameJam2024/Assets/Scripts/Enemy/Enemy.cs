using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private enum State
    {
        Wonder,
        Attack
    }

    [SerializeField] protected float range;
    [SerializeField] protected float speed;

    protected Transform playerTransform;
    protected Rigidbody2D rb2d;

    private State currState = State.Wonder;

    protected abstract void Wonder();

    protected abstract void Attack();

    protected abstract void Die();

    protected virtual void WonderToAttack() { }

    protected virtual void AttackToWonder() { }

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
        Debug.Log("Got playerTransform: " + playerTransform);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
