using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }
}
