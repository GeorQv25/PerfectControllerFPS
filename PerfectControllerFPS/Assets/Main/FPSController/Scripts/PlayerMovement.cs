using Unity.Burst.CompilerServices;
using UnityEngine;


public class PlayerMovement
{
    private Rigidbody _playerRb;
    private Transform _orientation;
    private LayerMask _maskToAvoid;
    
    private float _height = 2f;
    private float _heightOffset = 0.35f;
    
    private Vector3 _moveDir;
    private float _moveSepeed = 10f;
    private bool _isGrounded;
    private float _speedMultipl = 6.5f;
    private float _speedMultiplInAir = 0.6f;

    private float _jumpForce = 8f;

    //CustomGravity
    private float gravityValue = 0f;
    private float downForce = 600f;
    //


    public PlayerMovement(Rigidbody playerRb, Transform orientation, LayerMask maskToAvoid)
    {
        _playerRb = playerRb;
        _orientation = orientation;
        _maskToAvoid = maskToAvoid;
    }

    public void Tick()
    {
        SetInput();
        IsGrounded();
        SetDrag();
        Jump();
        //CustomGravity();
    }

    public void PhysicsTick()
    {
        //SlopeMovement();

        if (_isGrounded)
        {
            _playerRb.AddForce(_moveDir.normalized * _moveSepeed * _speedMultipl, ForceMode.Acceleration);
        }
        else
        {
            _playerRb.AddForce(_moveDir.normalized * _moveSepeed * _speedMultiplInAir, ForceMode.Acceleration);
        }
    }

    private void SlopeMovement()
    {
        //if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastLen))
        //{
        //    if (hit.normal != transform.up)
        //    {
        //        _playerRb.AddForce(Vector3.ProjectOnPlane(_moveDir, hit.normal).normalized * _moveSepeed * speedMultipl, ForceMode.Acceleration);
        //    }
        //}
    }

    //To DO, it's better to have custom input class
    private void SetInput()
    {
        _moveDir = _orientation.forward * Input.GetAxisRaw("Vertical") + _orientation.right * Input.GetAxisRaw("Horizontal");
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    void IsGrounded()
    {
        Collider[] result = Physics.OverlapSphere(new Vector3(_playerRb.transform.position.x, _playerRb.transform.position.y - _height / 2 + _heightOffset, _playerRb.transform.position.z), 0.45f, ~_maskToAvoid);
        _isGrounded = result.Length > 0;
    }

    void SetDrag()
    {
        _playerRb.drag = _isGrounded ? (float) DragType.NormalDrag : (float)DragType.AirDrag;
    }

    private void CustomGravity()
    {
        if (_isGrounded)
        {
            gravityValue = 0;
            return;
        }

        gravityValue += downForce * Time.deltaTime * Time.deltaTime;
        _playerRb.AddForce(Vector3.down * gravityValue);
    }
}