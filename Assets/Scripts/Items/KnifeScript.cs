using System.Collections;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(Destory());
    }
    private IEnumerator Destory()
    {
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push("Slash", gameObject);
    }
    private void OnDisable()
    {
        transform.position = Vector2.zero;
    }
}
