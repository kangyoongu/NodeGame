using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 2;
    public float damage = 10;
    private void OnEnable()
    {
        StartCoroutine(KillMe());
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    IEnumerator KillMe()
    {
        yield return new WaitForSeconds(4);
        PoolManager.Instance.Push("Bullet", gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost") || collision.gameObject.CompareTag("GhostBullet"))
        {
            Ghost g = null;
            if (collision.TryGetComponent(out g))
            {
                g.Hp -= damage;
                ScoreManager.Instance.CurrentScore += 6;
            }
            PoolManager.Instance.Push("Bullet", gameObject);
        }
    }
}
