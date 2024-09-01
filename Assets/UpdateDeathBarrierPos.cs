using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDeathBarrierPos : MonoBehaviour
{
    [SerializeField] PlatformHandler platformHandler;
    Vector2 _deathBarrierPos = Vector2.zero;


    private void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        if (platformHandler._bottomeScreenPlatform != null)
        {
            _deathBarrierPos = new Vector2(0, platformHandler._bottomeScreenPlatform.y);
            transform.position = _deathBarrierPos;
        }
    }




}
