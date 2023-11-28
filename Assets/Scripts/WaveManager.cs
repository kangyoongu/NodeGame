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
    public float[] disableWeaponWave;
    float randomTime = 0;
    bool spawn = false;
    public TextMeshProUGUI timer;
    private void Start()
    {
        randomTime = Random.Range(-1f, 1f);
    }
    void Update()
    {
        timer.text = waveTime.ToString("0") + "s";
        if(spawn == true)
        {
            time += Time.deltaTime;
            if(time+randomTime >= spawnPerWave[GameManager.Instance.wave])//스폰시간 약간 랜덤하게
            {
                randomTime = Random.Range(-1f, 1f);
                time = 0;
                //적 스폰 코드
            }
            waveTime -= Time.deltaTime;
            if(waveTime <= 0)
            {
                waveTime = 0;
                spawn = false;
                UIManager.Instance.WaveText();
                GameManager.Instance.wave++;
            }
        }
        else
        {
            time = 0;
            waveTime = 0;
        }
    }
    public void StartWave()//웨이브 시작
    {
        for(int i = 0; i < disableWeaponWave.Length; i++)//무기 끄기
        {
            if(GameManager.Instance.wave == disableWeaponWave[i])
            {
                PlayerController.Instance.canUseWeapon[i] = false;
                if((short)PlayerController.Instance.CurrentWeapon >= i)//만약 끈 무기 쓰고있었으면
                    PlayerController.Instance.CurrentWeapon = (Weapons)i-1;//강제로 딴무기
            }
        }
        waveTime = timePerWave[GameManager.Instance.wave];
        spawn = true;
    }
}
