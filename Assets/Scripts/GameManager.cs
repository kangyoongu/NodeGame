using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public CinemachineVirtualCamera startCvc;
    public Transform player;
    [HideInInspector] public bool playingGame = false;
    [HideInInspector] public int wave = 1;
    public GameObject gun;

    public void OnClickStart()
    {
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        UIManager.Instance.PlayUIIn();
        UIManager.Instance.MainUIOut();
        yield return new WaitForSeconds(2);
        startCvc.Priority = 5;
        gun.SetActive(true);
        playingGame = true;
        UIManager.Instance.WaveText();
    }
}
