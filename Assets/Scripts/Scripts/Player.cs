using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    private Rigidbody rgb;
    private bool jumping;
    private GameObject mainCamera;

    [Header("Basic movement")]
    [SerializeField] private float topSpeed = 3f;
    [SerializeField] private float jumpHeight = 5f;

    [Header("Jetpack")]
    [SerializeField] private float rechargeRate = 1f;
    [SerializeField] private float maxFuel = 500f;
    [SerializeField] private float boostStrength = 1f;

    private float currentFuel = 0f;
    public float CurrentFuel() => currentFuel;
    public float MaxFuel() => maxFuel;

    private ParticleSystem jetpackParticles;

    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        jetpackParticles = GameObject.FindGameObjectWithTag("JetpackEffect").GetComponent<ParticleSystem>();
        jumping = false;
    }

    private void Update()
    {
        if (!jumping && currentFuel < maxFuel)
        {
            currentFuel += rechargeRate;
            UIManager.Instance().UpdateJetpackUI();
        }
        else if (currentFuel > maxFuel)
        {
            currentFuel = maxFuel;
            UIManager.Instance().UpdateJetpackUI();
        }
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        if (!GameStateManager.Instance().IsGameOver() && (horizontal_input != 0 || vertical_input != 0))
        {
            Move(horizontal_input, vertical_input);
        }

        float jump_input = Input.GetAxis("Jump");

        if (!GameStateManager.Instance().IsGameOver() && jump_input != 0)
        {
            Jump();
        }
        else
        {
            if (jetpackParticles.isPlaying)
            {
                jetpackParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }

    private void Move(float horizontal_input, float vertical_input)
    {
        //Take world input and translate it to input relative to the camera's transform
        //Adding Torque needs to be understood through a right hand rule
        Vector3 inputVector = new Vector3(vertical_input*topSpeed, 0, -horizontal_input*topSpeed);
        Vector3 actualVector = mainCamera.transform.TransformDirection(inputVector);

        rgb.AddTorque(actualVector, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        if (!jumping)
        {
            jumping = true;
            rgb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        }
        else
        {
            UseJetpack();
        }
    }

    private void UseJetpack()
    {
        if (currentFuel > 0)
        {
            currentFuel--;
            rgb.AddForce(new Vector3(0, boostStrength, 0), ForceMode.VelocityChange);
            if (jetpackParticles.isStopped)
            {
                jetpackParticles.Play();
            }
        }
        else
        {
            currentFuel = 0;
            if (jetpackParticles.isPlaying)
            {
                jetpackParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
        }
        UIManager.Instance().UpdateJetpackUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            GameStateManager.Instance().ItemCollected();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumping = true;
        }
    }
}
