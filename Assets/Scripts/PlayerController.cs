using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.InputSystem;


public class PlayerController : NetworkBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputHandler _playerInputHandler;
    private Vector2 _moveDirection;
    private Vector2 _angleDirection;
    private bool _isMoving;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxDistance = 10f;
    [SerializeField] private Vector3[] _spawnPositions;



    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = Vector3.zero;  
        _rb = GetComponent<Rigidbody2D>();
        if (IsLocalPlayer)
        {
            GetComponent<PlayerInput>().enabled = true;
            _playerInputHandler = GetComponent<PlayerInputHandler>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Input();
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            int index = (int)OwnerClientId;
            transform.position = _spawnPositions[index];
        }
    }
    private void FixedUpdate()
    {
       Move();
    }
    private void Shoot()
    {
        if (IsClient)
        {
            if (!IsLocalPlayer) return;
            if (_playerInputHandler.leftFire)
            {
                print("Atirou");
                _playerInputHandler.leftFire = false;
                //Disparar para esquerda
            }
            else if (_playerInputHandler.rightFire)
            {
                _playerInputHandler.rightFire = false;
                //Disparar para direita
            }
        }

    }
    private void Input()
    {
        if (!IsLocalPlayer) return;
        if (_playerInputHandler.movementInput.x != _moveDirection.x)
        {
            _moveDirection = _playerInputHandler.movementInput;
            MoveNormalizedServerRpc(_moveDirection);
        }
        if (_playerInputHandler.movementInput.y != _angleDirection.y)
        {
            _angleDirection = _playerInputHandler.movementInput;
            MoveNormalizedServerRpc(_moveDirection);
        }
    }
    [ServerRpc]
    public void MoveNormalizedServerRpc(Vector2 move)
    {
        _moveDirection = move.normalized;
    }
    public void Move()
    {
        if (IsServer)
        {
            _rb.velocity = new Vector2(_moveDirection.x * _speed, _rb.velocity.y);
            
        }
    }
}
