using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDisplay : MonoBehaviour
{
    [SerializeField] GameObject EndScreenUI;

    public void DisplayEndGameScreen()
    {
        EndScreenUI.SetActive(true);

    }
    public void UnDisplayEndGameScreen()
    {
        EndScreenUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            DisplayEndGameScreen();
        }
    }
}
