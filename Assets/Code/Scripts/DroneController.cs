/*
using UnityEngine;

public class DroneController : MonoBehaviour
{
    #region Variables
    
    private Rigidbody _rb;

    [SerializeField] private float cyclicFactor = 15f;
    [SerializeField] private float pedalFactor = 15f;
    [SerializeField] private float collectiveFactor = 15f;

    [SerializeField] private float maxSpeed = 100f;

    [SerializeField] private Transform cameraTransform;

    #endregion
    
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mouseDirection =
            new Vector3(cameraTransform.position.x, transform.position.y, cameraTransform.position.z) -
            transform.position;

        transform.forward = mouseDirection.normalized;

        Vector3 moveDirection = transform.forward * CyclicValue.y + transform.right * CyclicValue.x;

        Vector3 force = moveDirection.normalized * cyclicFactor;

        _rb = GetComponent<Rigidbody>();
        
        if (CyclicValue == Vector2.zero)
        {
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
        }
        else
        {
            _rb.linearVelocity = new Vector3(force.x, _rb.linearVelocity.y, force.z);
        }

        
        Vector3 currentSpeed = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
        if (currentSpeed.magnitude > maxSpeed)
        {
            float fallValue = _rb.linearVelocity.y;
            Vector3 directionSpeed = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z).normalized * maxSpeed;
            _rb.linearVelocity = new Vector3(directionSpeed.x, fallValue, directionSpeed.z);
        }
    }
}
*/
