using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    private Transform gunTransform;//총 회전
    private Transform gunPoint;
    void Awake()
    {
        gunTransform = transform.GetChild(0);
        gunPoint = gunTransform.GetChild(0).GetChild(0);

    }
    void Update()
    {
        RotateGunTowardsMouse();//총 마우스 보게 회전
        ClickMouse();
    }

    private void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolManager.Instance.Pop("Bullet", gunPoint.position, transform.localScale.x == 1 ? gunPoint.rotation : Quaternion.Euler(0, 180, -gunPoint.eulerAngles.z));
        }
    }

    private void RotateGunTowardsMouse()//총 마우스 보게 회전
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 총 스케일 설정
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector2 gunDirection = (mousePosition - gunTransform.position).normalized;
        float angle;
        if (gunDirection.x > 0)
        {
            angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(-gunDirection.y, -gunDirection.x) * Mathf.Rad2Deg;
        }
        gunTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


    }
}
