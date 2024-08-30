using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDeathBarrierPos : MonoBehaviour
{
    [SerializeField] EndGameDisplay endGameDisplay;

    BoxCollider2D collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider.enabled = false;

        if (collision.gameObject.name == "Player")
        {
            endGameDisplay.DisplayEndGameScreen();
        }
    }
}
