using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    void OnBecameInvisible()
    {
        // TODO
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO
        if (collision.collider.name != this.name)
        {
            Destroy(gameObject);
        }
    }
}
