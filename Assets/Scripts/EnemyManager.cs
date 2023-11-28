using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    public Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }
}
