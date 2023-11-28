using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolManager.Instance.Pop("Rock", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))));
        }
    }
}
