using System;
using UnityEngine;

public enum Weapons : short
{
    Gun = 0,
    Boom = 1,
    Knife = 2,
    Rock = 3
};

public class PlayerController : SingleTon<PlayerController>
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public float distance;//밑으로 레이 쏴서 거리 얼마면 점프 가능한지

    public LayerMask groundLayer; // 지면을 나타내는 레이어

    private Rigidbody2D rb;
    private bool isGrounded;//점프가능?
    public GameObject[] blocks;
    public RectTransform[] points;
    public RectTransform check;

    private Weapons currentWeapon = Weapons.Gun;//지금무기
    public Weapons CurrentWeapon
    {
        get => currentWeapon;
        set
        {
            currentWeapon = value;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
            weapons[(short)currentWeapon].SetActive(true);
            check.anchoredPosition = points[(short)currentWeapon].GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public bool[] canUseWeapon;//각 무기 사용 가능한지

    [SerializeField] private int hitCount = 0;
    [SerializeField] private GameObject _gameOverPanel;
    
    public int HitCount
    {
        get => hitCount;
        set
        {
            if (value <= 3)
            {
                hitCount = value;
                blocks[hitCount].SetActive(true);
                canUseWeapon[hitCount - 1] = false;
                if ((short)CurrentWeapon < hitCount)//만약 끈 무기 쓰고있었으면
                    CurrentWeapon = (Weapons)hitCount;//강제로 딴무기
                else
                {
                    CurrentWeapon = CurrentWeapon;
                }
            }
            else
            {
                weapons[0].SetActive(false);
                weapons[1].SetActive(false);
                weapons[2].SetActive(false);
                weapons[3].SetActive(false);
                ScoreManager.Instance.GameOver();
            }
        }
    }
    public GameObject[] weapons;

    private float hp = 100;
    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
            UIManager.Instance.hpBar.fillAmount = hp * 0.01f;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//물리 가져오기
    }

    private void Update()
    {
        if (GameManager.Instance.playingGame)//게임 시작했을때만 함
        {
            // 좌우 이동
            float horizontalInput = Input.GetAxis("Horizontal");//이동
            Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            rb.velocity = movement;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && isGrounded)//점프
            {
                Jump();
            }

            // 레이캐스트로 땅에 닿았는지 확인
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);

            ChangeWeaponCheck();//무기 바꾸는지 체크
        }
    }

    private void ChangeWeaponCheck()//무기 바뀌면 다른 무기 끄고 바뀐 무기로 전환
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseWeapon[0])
        {
            CurrentWeapon = Weapons.Gun;
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseWeapon[1])
        {
            CurrentWeapon = Weapons.Boom;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseWeapon[2])
        {
            CurrentWeapon = Weapons.Knife;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && canUseWeapon[3])
        {
            CurrentWeapon = Weapons.Rock;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(true);
        }

    }

    private void Jump()//점프
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("GhostBullet"))
        {
            HitCount++;
        }
    }
}