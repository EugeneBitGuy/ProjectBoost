using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float thrustPower = 1000f;
    [SerializeField] private float rotationValue = 1f;
    
    [SerializeField] private AudioClip engineSound;
    [SerializeField] private float audioFadeSpeed = 0.1f;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem leftBoosterParticles;
    [SerializeField] private ParticleSystem rightBoosterParticles;
    
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessRotation();
    }
    
    void FixedUpdate()
    {
        ProcessThrust();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * (thrustPower * Time.fixedDeltaTime));
            PlayEngineSound();
            SpawnMainEngineParticles();
        }
        else
        {
            StopEngineSound();
            StopToSpawnMainEngineParticles();
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationValue);
            SpawnRightBoosterParticles();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationValue);
            SpawnLeftBoosterParticles();
        }
        else
        {
            StopToSpawnBoostersParticles();
        }
    }

    void ApplyRotation(float rotationPower)
    {
        _rigidbody.constraints = _rigidbody.constraints + (int) RigidbodyConstraints.FreezeRotationZ;
        transform.Rotate(Vector3.forward * (rotationPower * Time.deltaTime));
        _rigidbody.constraints = (RigidbodyConstraints) (_rigidbody.constraints - RigidbodyConstraints.FreezeRotationZ);
    }
    
    private void PlayEngineSound()
    {
        _audioSource.volume = 1f;
        if(!_audioSource.isPlaying)
            _audioSource.PlayOneShot(engineSound);
    }

    private void StopEngineSound()
    {
        if (_audioSource.volume > 0f)
        {
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0f, Time.fixedDeltaTime * audioFadeSpeed);
        }
        else
        {
            _audioSource.Stop();
        }
        
    }

    void SpawnMainEngineParticles()
    {
        if(!mainEngineParticles.isPlaying)
            mainEngineParticles.Play();
    }

    void StopToSpawnMainEngineParticles()
    {
        mainEngineParticles.Stop();
    }

    void SpawnLeftBoosterParticles()
    {
        rightBoosterParticles.Stop();
        if(!leftBoosterParticles.isPlaying)
            leftBoosterParticles.Play();
    }
    
    void SpawnRightBoosterParticles()
    {
        leftBoosterParticles.Stop();
        if(!rightBoosterParticles.isPlaying)
            rightBoosterParticles.Play();
    }

    void StopToSpawnBoostersParticles()
    {
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    public void KillAllParticles()
    {
        StopToSpawnBoostersParticles();
        StopToSpawnMainEngineParticles();
    }
}
