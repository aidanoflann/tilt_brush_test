using UnityEngine;
using System.Collections;

public class MovementController: MonoBehaviour
{
    //Variables
    public float speed = 1.0F;
    public float jumpSpeed = 3.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Vector3 currentAngle;

    public Camera mainCamera;
    private Vector3 cameraOffset = new Vector3(0.0f, -0.2f, 0.6f);

    void Start()
    {
        this.currentAngle = new Vector3(0f, 0f, 0f);
        this.controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        this.controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            this.currentAngle.y = Input.GetAxis("HorizontalView");
            this.transform.Rotate(this.currentAngle);
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * Time.deltaTime);
        UpdateCamera();
    }

    void UpdateCamera()
    {
        // we want the camera to face the same direction and always be behind and above
        this.mainCamera.transform.rotation = this.transform.rotation;
        this.mainCamera.transform.position = this.transform.position - this.transform.rotation * this.cameraOffset;
    }
}