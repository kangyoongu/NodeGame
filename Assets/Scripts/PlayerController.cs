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

    public float distance;//������ ���� ���� �Ÿ� �󸶸� ���� ��������

    public LayerMask groundLayer; // ������ ��Ÿ���� ���̾�

    private Rigidbody2D rb;
    private bool isGrounded;//��������?

    private Weapons currentWeapon = Weapons.Gun;//���ݹ���
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
            weapons[(short)value].SetActive(true);
        }
    }

    public bool[] canUseWeapon;//�� ���� ��� ��������

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
        rb = GetComponent<Rigidbody2D>();//���� ��������
    }

    private void Update()
    {
        if (GameManager.Instance.playingGame)//���� ������������ ��
        {
            // �¿� �̵�
            float horizontalInput = Input.GetAxis("Horizontal");//�̵�
            Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            rb.velocity = movement;

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//����
            {
                Jump();
            }

            // ����ĳ��Ʈ�� ���� ��Ҵ��� Ȯ��
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);

            ChangeWeaponCheck();//���� �ٲٴ��� üũ
        }
    }

    private void ChangeWeaponCheck()//���� �ٲ�� �ٸ� ���� ���� �ٲ� ����� ��ȯ
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canUseWeapon[0])
        {
            currentWeapon = Weapons.Gun;
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canUseWeapon[1])
        {
            currentWeapon = Weapons.Boom;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canUseWeapon[2])
        {
            currentWeapon = Weapons.Knife;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            weapons[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && canUseWeapon[3])
        {
            currentWeapon = Weapons.Rock;
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            weapons[3].SetActive(true);
        }

    }

    private void Jump()//����
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}