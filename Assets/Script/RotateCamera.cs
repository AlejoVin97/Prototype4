using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private InputSystem_Actions controls;
    public float rotationSpeed  = 150.0f;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        controls = new InputSystem_Actions();
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
        float horizontalInput = moveInput.x;
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime );
    }

    private void OnDisable()
    {
        controls.Disable();
        Debug.Log("Controls shut down");
    }
}
