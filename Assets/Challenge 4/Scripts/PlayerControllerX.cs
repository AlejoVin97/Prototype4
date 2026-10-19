using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    private InputSystem_Actions controls;
    
    public ParticleSystem boostParticle;//boost particule effect
    private float boostCooldown = 3.0f;//time required between boost action inputs
    private float boostWait = 0.0f;//time remaining before player can activate boost again

    void Awake()
    {
        controls = new InputSystem_Actions();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = controls.Player.Move.ReadValue<Vector2>().y;
        
        if ((boostWait <= 0.0f) && controls.Player.Jump.triggered )
        {
            boostParticle.Play();
            playerRb.AddForce(focalPoint.transform.forward * 50 , ForceMode.Impulse);
            boostWait = boostCooldown;
        }
        else 
        {
            if (boostWait > 0.0f) { 
                boostWait -= Time.deltaTime;
            }
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
            
        }
        
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  (transform.position - other.gameObject.transform.position).normalized; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(-awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(-awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
