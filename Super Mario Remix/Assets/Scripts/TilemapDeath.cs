using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapDeath : MonoBehaviour
{
    
void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.setLivesText(-2);

        }
    }

}