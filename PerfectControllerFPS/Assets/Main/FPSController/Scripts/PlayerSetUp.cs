using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetUp : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRB;
    [SerializeField] private Transform _orientation;
    [SerializeField] private LayerMask _maskToAvoid;
    [SerializeField] private PlayerController _playerController;


    private void Awake()
    {
        PlayerMovement playerMovement = new PlayerMovement(_playerRB, _orientation, _maskToAvoid);
        _playerController.Initialize(playerMovement);
    }
}