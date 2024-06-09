using UnityEngine;


public class PlayerLook
{
    //Player's Components
    private Transform _orientation;
    private Transform _playerCamera;
    //

    private float _sensY = 1;
    private float _sensX = 1;
    private float _xRotation = 0;
    private float _yRotation;


    public PlayerLook(Transform orientation, Transform playerCamera)
    {
        _orientation = orientation;
        _playerCamera = playerCamera;

        _yRotation = orientation.rotation.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Tick()
    {
        MyInput();

        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
    
    private void MyInput()
    {
        _yRotation += Input.GetAxisRaw("Mouse X") * _sensX;
        _xRotation -= Input.GetAxisRaw("Mouse Y") * _sensY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
    }
}
