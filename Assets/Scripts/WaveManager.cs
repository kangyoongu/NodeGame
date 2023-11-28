using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : SingleTon<WaveManager>
{
    float time = 0;//적 스폰 딜레이
    float waveTime = 0;//웨이브 끝나는 시간
    public float[] timePerWave;
    public float[] spawnPerWave;
    public float[] speedPerWave;
    float randomTime = 0;
    bool spawn = false;
    public TextMeshProUGUI timer;
    private void Start()
    {
        randomTime = Random.Range(-1f, 1f);
    }
    void Update()
    {
        if (GameManager.Instance.playingGame)
        {
            timer.text = waveTime.ToString("0") + "s";
            if (spawn == true)
            {
                time += Time.deltaTime;
                if (time + randomTime >= spawnPerWave[GameManager.Instance.wave])//스폰시간 약간 랜덤하게
                {
                    randomTime = Random.Range(-1f, 1f);
                    time = 0;
                    if (Random.value >= 0.5f)
                    {
                        Vector3 pos = PlayerController.Instance.transform.position + (Vector3)Random.insideUnitCircle.normalized * 7;
                        if (pos.y <= -0.867f)
                        {
                            pos.y = -0.867f;
                        }
                        PoolManager.Instance.Pop("Ghost_01", pos, Quaternion.identity).GetComponent<Ghost>().speed = speedPerWave[GameManager.Instance.wave] + Random.Range(-0.7f, 0.7f);
                    }
                    else
                    {
                        Vector3 pos = PlayerController.Instance.transform.position + (Vector3)Random.insideUnitCircle.normalized * 7;
                        if (pos.y <= -0.867f)
                        {
                            pos.y = -0.867f;
                        }
                        PoolManager.Instance.Pop("Ghost_02", pos, Quaternion.identity).GetComponent<Ghost>().speed = speedPerWave[GameManager.Instance.wave] + Random.Range(-0.7f, 0.7f);
                    }
                }
                waveTime -= Time.deltaTime;
                if (waveTime <= 0)
                {
                    waveTime = 0;
                    spawn = false;
                    GameManager.Instance.wave++;
                    UIManager.Instance.WaveText();
                }
            }
            else
            {
                time = 0;
                waveTime = 0;
            }
        }
    }
    public void StartWave()//웨이브 시작
    {
        waveTime = timePerWave[GameManager.Instance.wave];
        PlayerController.Instance.belt[0].SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            PlayerController.Instance.canUseWeapon[i] = true;
            PlayerController.Instance.blocks[i].SetActive(false);
        }
        spawn = true;
    }
}
