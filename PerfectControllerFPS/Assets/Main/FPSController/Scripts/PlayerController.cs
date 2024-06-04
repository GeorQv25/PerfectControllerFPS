using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;




    private float height = 2f;
    private float heightOffset = 0.35f;
    private float jumpForce = 8;
    public float raycastLen = 1;


    public void Initialize(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    private void Update()
    {
        _playerMovement.Tick();
    }

    private void FixedUpdate()
    {
        _playerMovement.PhysicsTick();
    }
}