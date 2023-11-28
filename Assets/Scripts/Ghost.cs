using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostType
{
    Fly,
    Walk
}

public class Ghost : MonoBehaviour
{
    
    
    [SerializeField] private GhostType _ghostType;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();

        if (_ghostType == GhostType.Fly)
        {
            _rigidbody.gravityScale = 0;
            _boxCollider.isTrigger = true;
        }
        else if(_ghostType == GhostType.Walk)
        {
            _rigidbody.gravityScale = 1;
            _boxCollider.isTrigger = false;
        }
    }

    private void Update()
    {
        Mathf.Sin(Time.time);
    }
}
