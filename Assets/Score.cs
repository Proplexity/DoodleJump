using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    Transform playerPos;
    public float scoreMultiplier { get; private set; } = 1f;
    public float cachedScore { get; private set; }

    int score;

    private void Start()
    {
        playerPos = this.transform;
    }

    private void Update()
    {
        UpdateScore();
        score = (int)cachedScore / 1;

    }

    void UpdateScore()
    {
        if (playerPos.position.y > cachedScore / scoreMultiplier)
            cachedScore += playerPos.position.y * scoreMultiplier;

        

    }
}
