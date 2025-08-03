using System;
using UnityEngine;

public class FPCAudio : MonoBehaviour
{
    [SerializeField] private FirstPersonController _controller;
    [SerializeField] private AudioSource _walkingAudioSource;
    [SerializeField] private AudioSource _oneShotAudioSource;
    [SerializeField] private AudioClip _walking;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _jumpHard;
    [SerializeField] private AudioClip _land;
    [SerializeField] private float _jumpHardGravityThreshold = -4;
    [SerializeField] private float _landedCooldown = 0.5f;
    
    private bool _wasWalking;
    private bool _wasGrounded;

    private void OnEnable()
    {
        _controller.OnJump += OnJump;
    }

    private void OnDisable()
    {
        _controller.OnJump -= OnJump;
    }

    private float _canLandTime;

    private void Update()
    {
        if (_controller.isGrounded && !_wasGrounded)
        {
            _wasGrounded = true;
            if (Time.time > _canLandTime)
            {
                _oneShotAudioSource.PlayOneShot(_land);
            }
        } 
        else if (!_controller.isGrounded && _wasGrounded)
        {
            _canLandTime = Time.time + _landedCooldown;
            _wasGrounded = false;
        }
        
        if (_controller.isWalking && !_wasWalking)
        {
            _wasWalking = true;
            _walkingAudioSource.Stop();
            _walkingAudioSource.Play();
        } else if (!_controller.isWalking && _wasWalking)
        {
            _wasWalking = false;
            _walkingAudioSource.Stop();
        }
    }

    private void OnJump()
    {
        if (GravityManager.Instance.GravityValue > _jumpHardGravityThreshold)
        {
            _oneShotAudioSource.PlayOneShot(_jump);
        }
        else
        {
            _oneShotAudioSource.PlayOneShot(_jumpHard);
        }
    }
}
