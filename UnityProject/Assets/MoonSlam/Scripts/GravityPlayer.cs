using System;
using UnityEngine;

public class GravityPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void FixedUpdate()
    {
        _rigidbody.AddForce(GravityManager.Instance.PlayerGravity, ForceMode.Acceleration);
    }
}
