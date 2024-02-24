using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Wonder,
        Attack
    }

    [SerializeField] private float range;
    [SerializeField] private float speed;

    private Vector3 playerPos;

    private State currState = State.Wonder;

    private void Wonder()
    {

    }

    private void Attack()
    {
        
    }

    private void Die()
    {

    }

    private void Update()
    {
        switch (currState)
        {
            case State.Wonder:
                break;
            case State.Attack:
                break;
        }

        if ((playerPos - transform.position).magnitude <= range)
        {
            currState = State.Attack;
        }
        else
        {
            currState = State.Wonder;
        }
    }
}
