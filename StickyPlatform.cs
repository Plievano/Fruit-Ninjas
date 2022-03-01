using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // Checks if player is on the platform
        {
            collision.gameObject.transform.SetParent(transform); // "Sticks" player to platform
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // Checks if player is on the platform
        {
            collision.gameObject.transform.SetParent(null); // "Unsticks" player to platform
        }
    }
}
