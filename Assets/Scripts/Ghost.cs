using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum GhostType
{
    Fly,
    Walk
}

public class Ghost : MonoBehaviour
{
    
    [SerializeField] private GhostType _ghostType;
    public float firePower;
    public float speed; //나중에 생성할 때 스피드값 랜덤으로 하면 재미있을 듯
    
    private Transform _playerTransform;
    private Vector3 moveDirection;
    
    //Components
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;
    
    //Managements
    private EnemyManager _enemyManager;
    
    //Controllers
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

       
    }

    private void Start()
    {
        _enemyManager = EnemyManager.Instance;
        _playerTransform = _enemyManager.playerTransform;
    }

    private void OnEnable()
    {
        if (_ghostType == GhostType.Walk)
        {
            Fire(firePower);
        }
    }


    private void Update()
    { 
        float playerToMyDistance = Vector3.Distance(_playerTransform.position, transform.position);
        if (_ghostType == GhostType.Fly)
        {
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1);
            }
            
            if (playerToMyDistance > 5)
            {
                moveDirection = (_playerTransform.position - transform.position).normalized;
            }
            _rigidbody.velocity = moveDirection * speed;
        }
        else
        {
            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1);
            }
            moveDirection = (_playerTransform.position - transform.position).normalized;
            if (playerToMyDistance > 4)
            {
                _rigidbody.velocity = new Vector2(moveDirection.x * speed, 0);
            }
            else
            {
                _rigidbody.velocity = new Vector2(0, 0);
            }
        }
    }

    private void Fire(float power)
    {
        StartCoroutine(FireCoroutine(power));
    }

    private IEnumerator FireCoroutine(float power)
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            GameObject gameObject = PoolManager.Instance.Pop("GhostBullet", transform.position, quaternion.identity);
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection * power;
        }
    }
}
