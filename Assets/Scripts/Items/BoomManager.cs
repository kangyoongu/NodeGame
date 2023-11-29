using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
            PoolManager.Instance.Pop("Boom", transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f))));
        }
    }
}
