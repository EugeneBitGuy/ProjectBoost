using UnityEngine;
using Random = UnityEngine.Random;

public class SpinningObstacle : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private bool spinAroundRandomAxis = false;
    [SerializeField] private Vector3 rotationAxis = Vector3.zero;
    private void Start()
    {
        PrepareRandomRotation();
    }

    private void PrepareRandomRotation()
    {
        if (spinAroundRandomAxis)
            rotationAxis = new Vector3(Random.value, Random.value, Random.value);
    }

    private void Update()
    {
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        transform.Rotate(rotationAxis.normalized * (rotationSpeed * Time.deltaTime));
    }
}
