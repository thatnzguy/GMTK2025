using System;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private float _minVelocity;
    [SerializeField] private float _canHitVelocity;
    [SerializeField] private Rigidbody _rigidbody;
    
    private float _lastVelocity;

    private void Update()
    {
        if (_rigidbody == null) return;
        
        if (_rigidbody.linearVelocity.magnitude < _minVelocity && _lastVelocity > _canHitVelocity)
        {
            _audioSource.PlayOneShot(_hitSFX);
        }
        
        _lastVelocity = _rigidbody.linearVelocity.magnitude;
    }
}
