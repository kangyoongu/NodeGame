using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 2;
    private void Start()
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
}
