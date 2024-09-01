using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    // This is for the UpdateDeathBarrierPos Script
    public Vector2 _bottomeScreenPlatform;


    [SerializeField] Transform _player;
    [SerializeField] GameObject _platformPrefab;

    [Header("Platform Variables")]
    [SerializeField] float _platformSpawnHeight;
    [SerializeField] float _platformSpawnHeightOffsetMin;
    [SerializeField] float _platformSpawnHeightOffsetMax;
    [SerializeField] float _startOfScreen;
    [SerializeField] float _endOfScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float playerPosY = _player.position.y;
        float spawnPosX = Random.Range(_startOfScreen, _endOfScreen);
        float spawnHeightOfest = Random.Range(_platformSpawnHeightOffsetMin, _platformSpawnHeightOffsetMax);

        if (collision != null)
            _bottomeScreenPlatform = collision.transform.position;

        if (collision.gameObject.name != "DeathBarrier")
        {
            Instantiate(_platformPrefab, new Vector2(spawnPosX, (playerPosY + _platformSpawnHeight + spawnHeightOfest)), Quaternion.identity);
            Destroy(collision.gameObject);
        }
        
    }
}
