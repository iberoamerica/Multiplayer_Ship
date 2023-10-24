using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : NetworkBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputHandler _playerInputHandler;
    private Vector2 _moveDirection;
    private Vector2 _angleDirection;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _maxDistance = 10f;



    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            _playerInputHandler = GetComponent<PlayerInputHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClient)
        {
            if (!IsLocalPlayer) return;
            if (_playerInputHandler.leftFire)
            {
                print("Atirou");
                _playerInputHandler.leftFire = false;
                //Disparar para esquerda
            }else if (_playerInputHandler.rightFire)
            {
                _playerInputHandler.rightFire = false;
                //Disparar para direita
            }
        }
    }
}
