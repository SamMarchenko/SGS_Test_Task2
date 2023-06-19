using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _vfxHit;

    [SerializeField] private float _speed = 30f;
    
    void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(_vfxHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}