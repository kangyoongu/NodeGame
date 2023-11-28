using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float power = 50;
    public float damage = 30;
    bool addforce = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        addforce = true;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(8);
        PoolManager.Instance.Push("Rock", gameObject);
    }

    private void Update()
    {
        if (addforce)
        {
            rb.velocity = Vector2.zero;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 플레이어에서 마우스까지의 방향 벡터 계산
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            rb.AddForce(direction * power, ForceMode2D.Impulse);
            addforce = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PoolManager.Instance.Pop("RockBreak", transform.position, Quaternion.identity);
        PoolManager.Instance.Push("Rock", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost") || collision.gameObject.CompareTag("GhostBullet"))
        {
            Ghost g = null;
            if (collision.TryGetComponent(out g))
            {
                g.Hp -= damage;
                ScoreManager.Instance.CurrentScore += 10;
            }
            PoolManager.Instance.Pop("RockBreak", transform.position, Quaternion.identity);
            PoolManager.Instance.Push("Rock", gameObject);
        }
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
}
