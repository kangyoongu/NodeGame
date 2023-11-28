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
    private ParticleSystem particleSystem;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
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
        if(playParticle && !particleSystem.isPlaying)
        {
            PoolManager.Instance.Push("Rock", gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopCoroutine("Timer");
        StartCoroutine("Timer");
    }
    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3);
        GetComponent<SpriteRenderer>().enabled = false;
        particleSystem.Play();//여기서폭탄이터지니까 주위 그 뭐냐 적들 데미지 줘
        playParticle = true;
    }
}
