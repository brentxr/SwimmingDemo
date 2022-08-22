using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Swimmer : MonoBehaviour
{
    #region Public Variables
    
    [Header("Swimming Settings")]
    [SerializeField] private float swimSpeed = 1f;
    [SerializeField] private float drag = 1f;
    [SerializeField] private float minForce;
    [SerializeField] private float timeBetweenStrokes;

    [Header("Controller Reference")]
    [SerializeField] private InputActionReference leftControllerRef;
    [SerializeField] private InputActionReference leftControlVelocity;
    [SerializeField] private InputActionReference rightControllerRef;
    [SerializeField] private InputActionReference rightControlVelocity;


    #endregion

    #region Private Variables

    private Rigidbody _rb;
    private float _coolDownTimer;
    private Transform _trackingRef;

    #endregion


    #region Unity Methods

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _trackingRef = this.gameObject.transform;
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        _coolDownTimer += Time.deltaTime;
        
        if (_coolDownTimer > timeBetweenStrokes && 
            leftControllerRef.action.IsPressed() && 
            rightControllerRef.action.IsPressed())
        {
            var leftHandVelocity = leftControlVelocity.action.ReadValue<Vector3>();
            var rightHandlVelocity = rightControlVelocity.action.ReadValue<Vector3>();
            Vector3 localVelocity = (leftHandVelocity + rightHandlVelocity) * -1;

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = _trackingRef.TransformDirection(localVelocity);
                _rb.AddForce(worldVelocity * swimSpeed, ForceMode.Acceleration);
                _coolDownTimer = 0f;
            }
        }

        if (_rb.velocity.sqrMagnitude > 0.01f)
        {
            _rb.AddForce(-_rb.velocity * drag, ForceMode.Acceleration);
        }
    }

    #endregion
    
}
