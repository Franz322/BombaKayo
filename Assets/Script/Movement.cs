using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;

    public Animator anim;

    private bool isPickingUp = false;
    private bool isThrowing = false;


    #region Variables: Movement
    [SerializeField] private float speed;
    #endregion

    #region Variables: Rotation
    [SerializeField] private float smoothTime = 0.05f;
    private float _currentVelocity;
    #endregion

    #region Variables: Gravity
    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;
    #endregion

    #region Variables: Jumping
    [SerializeField] private float jumpPower;
    #endregion

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ApplyGravity();
        ApplyRotation();
        ApplyMovement();

        if (isPickingUp)
        {
            anim.SetTrigger("PickUp");  // Trigger pick up animation
            isPickingUp = false;  // Reset flag after animation
        }

        if (isThrowing)
        {
            anim.SetTrigger("Throw");  // Trigger throw animation
            isThrowing = false;  // Reset flag after animation
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {

        if (_input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    private void ApplyMovement()
    {
        _characterController.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _direction = new Vector3(_input.x, 0.0f, _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded()) return;

        _velocity += jumpPower;
    }

    private bool IsGrounded() => _characterController.isGrounded;


    // Function to handle bomb pickup
    public void PickUpBomb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPickingUp = true;  // Set the flag to start pick up animation
        }
    }

    // Function to handle bomb throw
    public void ThrowBomb(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isThrowing = true;  // Set the flag to start throw animation
        }
    }
}
