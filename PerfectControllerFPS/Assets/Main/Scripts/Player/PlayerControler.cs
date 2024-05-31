using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DragType
{
    AirDrag = 1,
    NormalDrag = 6,
}

public class PlayerControler : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private Transform orientation;
    [SerializeField] private LayerMask maskToAvoid;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    //CustomGravity
    private float gravityValue = 0f;
    private float downForce = 600f;
    //

    private float height = 2f;
    private float heightOffset = 0.35f;

    private Vector3 moveDir;
    private float moveSepeed = 10f;
    private float horizontalMovement;
    private float verticalMovement;
    private float speedMultipl = 6.5f;
    private float speedMultiplInAir = 0.6f;
    private float jumpForce = 8;
    private bool isGrounded;
    public float raycastLen = 1;

    RaycastHit hit;


    private void Update()
    {
        IsGrounded();
        SetDrag();
        CustomGravity();
        MyInput();
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDir = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void IsGrounded()
    {
        Collider[] result = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - height / 2 + heightOffset, transform.position.z), 0.45f, ~maskToAvoid);
        isGrounded = result.Length > 0;
    }

    void SetDrag()
    {
        playerRb.drag = isGrounded ? (float) DragType.NormalDrag : (float)DragType.AirDrag;
    }

    private void FixedUpdate()
    {
        SlopeMovement();

        if (isGrounded)
        {
            playerRb.AddForce(moveDir.normalized * moveSepeed * speedMultipl, ForceMode.Acceleration);
        }
        else
        {
            playerRb.AddForce(moveDir.normalized * moveSepeed * speedMultiplInAir, ForceMode.Acceleration);
        }
    }

    private void SlopeMovement()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastLen))
        {
            if(hit.normal != transform.up)
            {
                playerRb.AddForce(Vector3.ProjectOnPlane(moveDir, hit.normal).normalized * moveSepeed * speedMultipl, ForceMode.Acceleration);
            }
        }
    }

    private void CustomGravity()
    {
        if (isGrounded)
        {
            gravityValue = 0;
            return;
        }

        gravityValue += downForce * Time.deltaTime * Time.deltaTime;
        playerRb.AddForce(-orientation.up * gravityValue);
    }
}