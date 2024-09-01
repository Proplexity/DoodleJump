using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCamPos : MonoBehaviour
{

    BoxCollider2D _collider;
    Rigidbody2D _rb;


    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
       _rb = GetComponent<Rigidbody2D>();
       
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionY;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _rb.constraints = RigidbodyConstraints2D.None;

        }
    }
}
