using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeManager : MonoBehaviour
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
            PoolManager.Instance.Pop("Slash", transform.position, transform.rotation);
        }
    }

    void RotateObjectTowardsMouse()
    {
        // ���콺 �������� ��ġ�� ��������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Z ���� 0���� ����

        // ������Ʈ�� ���콺 ������ �������� ȸ����Ű��
        transform.up = (mousePosition - transform.position).normalized;
    }
}
