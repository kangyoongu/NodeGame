using System;
using UnityEngine;

public enum Weapons : short
{
    Gun,
    Boom,
    Knife,
    Rock
};

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public float distance;//밑으로 레이 쏴서 거리 얼마면 점프 가능한지

    public LayerMask groundLayer; // 지면을 나타내는 레이어

    private Rigidbody2D rb;
    private bool isGrounded;//점프가능?

    public Weapons currentWeapon = Weapons.Gun;//지금무기

    public bool canUseGun = true;//각 무기 사용 가능한지
    public bool canUseBoom = true;
    public bool canUseKnife = true;
    public bool canUseRock = true;

    [SerializeField] private int hitCount = 4;
    [SerializeField] private GameObject _gameOverPanel;

    [SerializeField] private GameObject gunClose;
    [SerializeField] private GameObject knifeClose;
    [SerializeField] private GameObject bloomClose;
    [SerializeField] private GameObject rockClose;
    
    public int HitCount
    {
        get => hitCount;
        set
        {
            hitCount = value;
            switch (hitCount)
            {
                case 3 : 
                    canUseGun = false; 
                    gunClose.SetActive(true);
                    break;
                case 2 : 
                    canUseBoom = false;
                    knifeClose.SetActive(true);
                    break;
                case 1 : 
                    canUseKnife = false;
                    bloomClose.SetActive(true);
                    break;
                case 0 :
                    canUseRock = false;
                    rockClose.SetActive(true);
                    break;
                case -1:
                    _gameOverPanel.SetActive(true);
                    Time.timeScale = 0;
                    break;
            }
        }
    }
    public GameObject[] weapons;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//물리 가져오기
    }

    private void Update()
    {
        // 좌우 이동
        float horizontalInput = Input.GetAxis("Horizontal");//이동
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//점프
        {
            Jump();
        }

        // 레이캐스트로 땅에 닿았는지 확인
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);

        ChangeWeaponCheck();//무기 바꾸는지 체크
    }

    private void ChangeWeaponCheck()//무기 바뀌면 다른 무기 끄고 바뀐 무기로 전환
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseGun)
        {
            currentWeapon = Weapons.Gun;
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseBoom)
        {
            currentWeapon = Weapons.Boom;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseKnife)
        {
            currentWeapon = Weapons.Knife;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && canUseRock
            )
        {
            currentWeapon = Weapons.Rock;
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
            HitCount--;
        }
    }
}