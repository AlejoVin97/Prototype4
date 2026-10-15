using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controls;
    public float playerSpeed = 150.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerUp;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        controls = new InputSystem_Actions();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void OnEnable()
    {
        controls.Enable();
        Debug.Log(controls.Player.Move);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        float forwardInput = moveInput.y;
        playerRb.AddForce( playerSpeed * forwardInput * Time.deltaTime * focalPoint.transform.forward );
        powerupIndicator.transform.position = transform.position ;
    }

    private void OnDisable()
    {
        controls.Disable();
        Debug.Log("Controls shut down");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name +  "with powerup set to " + hasPowerUp);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }     
    }


    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
        Debug.Log("Powerup RunOut");
    }
}