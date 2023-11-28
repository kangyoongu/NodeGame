using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RockManager : MonoBehaviour
{
    void Update()
    {
        ClickMouse();
    }

    private void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("sakfdsf");
            PoolManager.Instance.Pop("Rock", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))));
        }
    }
}
