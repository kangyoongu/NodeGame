using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomManager : MonoBehaviour
{
    void Update()
    {
        ClickMouse();
    }

    private void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolManager.Instance.Pop("Boom", transform.position, Quaternion.Euler(Vector3.zero));
        }
    }
}
