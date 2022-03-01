using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0; // Variable that stores how many cherries the player has hit

    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) // Starts when you collide with another object
    {
        if (collision.gameObject.CompareTag("Cherry")) // Checks if a cherry was hit
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject); // Removes the Cherry that was just hit
            cherries++; // Adds 1 to the Cherries variable
            cherriesText.text = "Cherries: " + cherries;  // Changes the displayed cherries collected
        }
    }
}
