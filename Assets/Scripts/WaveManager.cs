using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : SingleTon<WaveManager>
{
    float time = 0;//�� ���� ������
    float waveTime = 0;//���̺� ������ �ð�
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
            if(time+randomTime >= spawnPerWave[GameManager.Instance.wave])//�����ð� �ణ �����ϰ�
            {
                randomTime = Random.Range(-1f, 1f);
                time = 0;
                //�� ���� �ڵ�
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
    public void StartWave()//���̺� ����
    {
        for(int i = 0; i < disableWeaponWave.Length; i++)//���� ����
        {
            if(GameManager.Instance.wave == disableWeaponWave[i])
            {
                PlayerController.Instance.canUseWeapon[i] = false;
                if((short)PlayerController.Instance.CurrentWeapon >= i)//���� �� ���� �����־�����
                    PlayerController.Instance.CurrentWeapon = (Weapons)i-1;//������ ������
            }
        }
        waveTime = timePerWave[GameManager.Instance.wave];
        spawn = true;
    }
}
