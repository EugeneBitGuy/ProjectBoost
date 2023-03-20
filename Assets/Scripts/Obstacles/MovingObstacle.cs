using System;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private const float TwoPie = Mathf.PI * 2f;
    private const float MovementFactorDivisor = 2f;
    private const float MovementFactorOffset = 1f;

    [SerializeField] private Vector3 movementVector = Vector3.one;
    [SerializeField] private float movementPeriod = 1f;

    private Vector3 _startPosition;
    private float _startTime;

    private bool _gizmoFreezMovementVector = false;
    
    private void Start()
    {
        _startPosition = transform.position;
        _startTime = Time.time;

        _gizmoFreezMovementVector = true;
    }

    private void Update()
    {
        ProcessMovement();
    }
    
    private void OnDrawGizmosSelected()
    {
        var startPoint = _gizmoFreezMovementVector ? _startPosition : transform.position;
        var endPoint = movementVector;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(startPoint, endPoint);
    }


    private void ProcessMovement()
    {
        if(movementPeriod <= Mathf.Epsilon)
            return;
        
        float elapsedTime = Time.time - _startTime;

        float numberOfFullCycles = elapsedTime / movementPeriod;

        float sinValue = Mathf.Sin(numberOfFullCycles * TwoPie);

        float movementFactor = (sinValue + MovementFactorOffset) / MovementFactorDivisor;

        Vector3 offset = movementVector * movementFactor;

        transform.position = _startPosition + offset;
    }
}
