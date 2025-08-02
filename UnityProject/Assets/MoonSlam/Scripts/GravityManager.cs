using System;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    [SerializeField] private float _gravityStart;
    [SerializeField] private float _gravityEnd;
    [SerializeField] private AnimationCurve _gravityCurve;
    
    [SerializeField] private float _gravityPlayerStart;
    [SerializeField] private float _gravityPlayerEnd;
    [SerializeField] private AnimationCurve _gravityPlayerCurve;

    [SerializeField] private AnimationCurve _gravityCurveAbsolute;

    public Vector3 WorldGravity { get; private set; }
    public Vector3 PlayerGravity { get; private set; }
    public float GravityValue;

    private void Update()
    {
        float gravity = _gravityCurveAbsolute.Evaluate(GameManager.Instance.TimeElapsedSeconds);
        Physics.gravity = Vector3.up * gravity;
        PlayerGravity = Vector3.up * gravity;
        GravityValue = gravity;
        return;

        float gravityScale = _gravityCurve.Evaluate(GameManager.Instance.TimeRemainingNormalized);
        float gravityLerped = Mathf.Lerp(_gravityStart, _gravityEnd, gravityScale);
        WorldGravity = Vector3.up * gravityLerped;
        Physics.gravity = WorldGravity;
        
        float gravityPlayerScale = _gravityPlayerCurve.Evaluate(GameManager.Instance.TimeRemainingNormalized);
        float gravityPlayerLerped = Mathf.Lerp(_gravityPlayerStart, _gravityPlayerEnd, gravityPlayerScale);
        PlayerGravity = Vector3.up * gravityPlayerLerped;
    }
}