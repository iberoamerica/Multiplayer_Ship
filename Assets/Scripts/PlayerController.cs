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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
