using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            PoolManager.Instance.Pop("Rock", transform.position, Quaternion.Euler(Vector3.zero));
        }
    }
}
