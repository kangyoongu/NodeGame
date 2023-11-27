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

    public float distance;//������ ���� ���� �Ÿ� �󸶸� ���� ��������

    public LayerMask groundLayer; // ������ ��Ÿ���� ���̾�

    private Rigidbody2D rb;
    private bool isGrounded;//��������?

    public Weapons currentWeapon = Weapons.Gun;//���ݹ���

    public bool canUseGun = true;//�� ���� ��� ��������
    public bool canUseBoom = true;
    public bool canUseKnife = true;
    public bool canUseRock = true;

    public GameObject[] weapons;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//���� ��������
    }

    private void Update()
    {
        // �¿� �̵�
        float horizontalInput = Input.GetAxis("Horizontal");//�̵�
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // ����
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//����
        {
            Jump();
        }

        // ����ĳ��Ʈ�� ���� ��Ҵ��� Ȯ��
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, distance, groundLayer);

        ChangeWeaponCheck();//���� �ٲٴ��� üũ
    }

    private void ChangeWeaponCheck()//���� �ٲ�� �ٸ� ���� ���� �ٲ� ����� ��ȯ
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

    private void Jump()//����
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}