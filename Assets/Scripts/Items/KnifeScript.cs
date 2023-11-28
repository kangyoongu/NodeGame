using System.Collections;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public float damage;
    private void OnEnable()
    {
        StartCoroutine(Destory());
        
    }
    private IEnumerator Destory()
    {/*
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3(0, 0.83f), new Vector2(2.52f, 1.6f), transform.localEulerAngles.z);
        // 가져온 모든 오브젝트에 대해 처리
        foreach (Collider2D collider in colliders)
        {
            print(collider.name);
            Ghost g = null;
            if (collider.TryGetComponent(out g))
            {
                g.Hp -= damage;
                ScoreManager.Instance.CurrentScore += 30;
            }
        }*/
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push("Slash", gameObject);
    }
    private void OnDisable()
    {
        transform.position = Vector2.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost") || collision.gameObject.CompareTag("GhostBullet"))
        {
            Ghost g = null;
            if (collision.TryGetComponent(out g))
            {
                g.Hp -= damage;
                ScoreManager.Instance.CurrentScore += 20;
            }
        }
    }
}
