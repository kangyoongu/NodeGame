using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(KillThis());
    }
    IEnumerator KillThis()
    {
        yield return new WaitForSeconds(10);
        if(gameObject.name == "Audios")
        {
            PoolManager.Instance.Push("RockBreak", gameObject);
        }
        else if(gameObject.name == "Audios 1")
        {
            PoolManager.Instance.Push("Explosion", gameObject);
        }
    }
}
