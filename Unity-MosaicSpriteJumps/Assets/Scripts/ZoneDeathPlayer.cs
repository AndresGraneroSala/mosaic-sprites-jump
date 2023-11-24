using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDeathPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject Player = other.transform.parent.gameObject;
            Rigidbody2D rbPlayer = Player.GetComponent<Rigidbody2D>();

            rbPlayer.velocity = Vector2.zero;
            rbPlayer.mass = 0.1f;
            rbPlayer.gravityScale = 0.1f;


        }
    }
}
