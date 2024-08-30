using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenLoop : MonoBehaviour
{
    Camera cam;
    Transform _playerPos;
    Rigidbody2D _rb;
    Vector2 _screenPos;
    float RightSideScreen = 8.5f;
    float LeftSideScreen = -8.5f;
     

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        _playerPos = this.gameObject.transform;
        
    }

    private void Update()
    {
        _screenPos = GetPlayerOnScreenPos();
        

        if (_rb.velocity.x < 0 &&  _screenPos.x < 0)
        {
            Vector2 newPlayerPos = new Vector2(RightSideScreen, _playerPos.position.y);
            _playerPos.position = newPlayerPos;
        }
        if (_rb.velocity.x > 0 && _screenPos.x > 480)
        {
            Vector2 newPlayerPos = new Vector2(LeftSideScreen, _playerPos.position.y);
            _playerPos.position = newPlayerPos;
        }

;       
    }



    Vector2 GetPlayerOnScreenPos()
    {
        Vector2 screenPos = cam.WorldToScreenPoint(_playerPos.position);
        return screenPos; 
    }
}
