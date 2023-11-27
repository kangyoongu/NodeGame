using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomManager : MonoBehaviour
{
    void Update()
    {
        RotateObjectTowardsMouse();
        ClickMouse();
    }
    
    private void ClickMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolManager.Instance.Pop("Grenade", transform.position, Quaternion.Euler(Vector3.zero));
        }
    }

    void RotateObjectTowardsMouse()
    {
        // 마우스 포인터의 위치를 가져오기
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Z 값은 0으로 설정

        // 오브젝트를 마우스 포인터 방향으로 회전시키기
        transform.up = (mousePosition - transform.position).normalized;
    }
}
