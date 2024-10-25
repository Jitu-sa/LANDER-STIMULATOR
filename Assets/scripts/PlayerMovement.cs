using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float mainthrust;
    public float rotationThrust = 1f;
    public float mouserotation;
    public AudioClip thrustersound;
    public ParticleSystem mainthrustparticle;
    public ParticleSystem leftthrustparticle;
    public ParticleSystem rightthrustparticle;
    public ParticleSystem frontthrustparticle;
    public ParticleSystem backtthrustparticle;

    AudioSource audioSource;
    Rigidbody rb;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        audioSource= GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessRotation();
        thrusting();
        mousexrotation();
    }

    void mousexrotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * mouserotation * Time.deltaTime);
    }

    void thrusting()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            startthrusting();
        }
        else
        {
            stopthrusting();
        }
    }

    void stopthrusting()
    {
        mainthrustparticle.Stop();
    }

    void startthrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainthrust * Time.deltaTime);
        playsound();
        mainthrustparticle.Play();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.S))
        {
            startrotatefront();
        }
        else
        {
            stoprotatefront();
        }

        if (Input.GetKey(KeyCode.W))
        {
            startrotateback();
        }
        else
        {
            stoprotateback();
        }

        if (Input.GetKey(KeyCode.D))
        {
            startrightrotation();
        }
        else
        {
            stoprightrotation();
        }

        if (Input.GetKey(KeyCode.A))
        {
            startleftrotation();
        }
        else
        {
            stopleftrotation();
        }
    }

    void startrotatefront()
    {
        frontbackmovement(rotationThrust);
        playsound();
        frontthrustparticle.Play();
    }

    void stoprotatefront()
    {
        frontthrustparticle.Stop();
    }

    void startrotateback()
    {
        frontbackmovement(-rotationThrust);
        playsound();
        backtthrustparticle.Play();
    }

    void stoprotateback()
    {
        backtthrustparticle.Stop();
    }

    void stopleftrotation()
    {
        leftthrustparticle.Stop();
    }

    void startleftrotation()
    {
        ApplyRotation(-rotationThrust);
        playsound();
        leftthrustparticle.Play();
    }

    void stoprightrotation()
    {
        rightthrustparticle.Stop();
    }

    void startrightrotation()
    {
        ApplyRotation(rotationThrust);
        playsound();
        rightthrustparticle.Play();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Translate(Vector3.left* rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreezing rotation so the physics system can take over
    }

    void frontbackmovement(float moveThisFrame)
    {
        rb.freezeRotation = true;  // freezing rotation so we can manually rotate
        transform.Translate(Vector3.forward * moveThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // unfreezing rotation so the physics system can take over
    }

    void playsound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustersound);
        }
    }
}