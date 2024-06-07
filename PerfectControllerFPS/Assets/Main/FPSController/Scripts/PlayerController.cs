using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;


    public void Initialize(PlayerMovement playerMovement, PlayerLook playerLook)
    {
        _playerMovement = playerMovement;
        _playerLook = playerLook;
    }

    private void Update()
    {
        _playerMovement.Tick();
        _playerLook.Tick();
    }

    private void FixedUpdate()
    {
        _playerMovement.PhysicsTick();
    }
}