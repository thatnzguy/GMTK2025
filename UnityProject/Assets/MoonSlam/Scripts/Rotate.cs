using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(0,0,_speed);
    }
}
