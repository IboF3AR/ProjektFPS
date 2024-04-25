using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player_Movement : MonoBehaviour
{
    // Movement
    private Player_Inputs player_Inputs;
    private CharacterController controller;
    public float speed = 10f;
    Vector3 velocity;
    public bool isMoving;
    // Falling
    private const float gravity = -18f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    // Jump
    public float jumpHeight = 2f;
    private float jumpVelocity;
    private bool flag_jump = false;
    // Inputs
    private float xInput, zInput;
    public Vector3 move;
    // Shooting
    //private Player_HandleCrossHair handleCrossHair; // would just change ui 
    private Player_HandleWeapon handleWeapon;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        handleWeapon = GetComponent<Player_HandleWeapon>();
        player_Inputs = Player_Inputs.Inst;
        //handleCrossHair = GetComponent<Player_HandleCrossHair>();
        jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void Update()
    {
        CheckStates();
        CheckInputs();
        if(move != Vector3.zero) handleWeapon.DecreasePrecision();
        Move();
    }

    private void Move()
    {
        move = transform.right * xInput + transform.forward *zInput;
        isMoving = (move.magnitude > 0.15f);
        controller.Move(move.normalized * speed * Time.deltaTime);
        if(flag_jump) velocity.y = jumpVelocity;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckInputs()
    {
        xInput = player_Inputs.movementInput.x * Time.deltaTime;
        zInput = player_Inputs.movementInput.y * Time.deltaTime;
        flag_jump = player_Inputs.Get_Btn_Space();;
    }

    private void CheckStates()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0) velocity.y = -2;
    }
}
