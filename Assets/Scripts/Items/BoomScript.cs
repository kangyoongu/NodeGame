using System.Collections;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float power = 50;
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

            // 플레이어에서 마우스까지의 방향 벡터 계산
            Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

            rb.AddForce(direction * power, ForceMode2D.Impulse);
            addforce = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Destroy());
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    private IEnumerator Destroy()
    {
        yield return 3;
        PoolManager.Instance.Pop("GrenadeExplosion", transform.position, Quaternion.identity);
        PoolManager.Instance.Push("Grenade", gameObject);
    }
}
