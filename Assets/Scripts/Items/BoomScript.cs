using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float power = 50;
    public float damage = 10;
    bool addforce = false;
    bool playParticle = false;
    private new ParticleSystem particleSystem;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        addforce = true;
        StartCoroutine("Timer");
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
        if(playParticle && !particleSystem.isPlaying)
        {
            PoolManager.Instance.Push("Rock", gameObject);
        }
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2f);
        //��ź����
        GetComponent<SpriteRenderer>().enabled = false;
        particleSystem.Play();
        playParticle = true;

        PoolManager.Instance.Pop("Explosion", transform.position, Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);

        // ������ ��� ������Ʈ�� ���� ó��
        foreach (Collider2D collider in colliders)
        {
            Ghost g = null;
            if (collider.TryGetComponent(out g))
            {
                g.Hp -= damage;
                ScoreManager.Instance.CurrentScore += 30;
            }
        }
    }
}
