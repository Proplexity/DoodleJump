using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCamPos : MonoBehaviour
{
    [SerializeField] Transform playerPos;

    Camera cam;
    Vector2 ScreenPos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        ScreenPos = cam.WorldToScreenPoint(playerPos.position);

        //Debug.Log(ScreenPos);
        
    }
}
