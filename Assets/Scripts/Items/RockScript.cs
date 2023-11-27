using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float power = 50;
    public float damage = 10;
    bool addforce = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        addforce = true;
    }
    private void Update()
    {
        if (addforce)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �÷��̾�� ���콺������ ���� ���� ���
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            rb.AddForce(direction * power, ForceMode2D.Impulse);
            addforce = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PoolManager.Instance.Push("Rock", gameObject);
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
}