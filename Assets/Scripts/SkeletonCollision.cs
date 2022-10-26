using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCollision : MonoBehaviour
{
    public int health;
    public GameObject dieEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > health)
        {
            Die();
        }
    }
    void Die()
    {
        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(transform.gameObject);
    }
}
