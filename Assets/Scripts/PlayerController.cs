using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : NetworkBehaviour
{
    //Atributos
    public NetworkVariable<float> health = new NetworkVariable<float>(30f);
    [SerializeField] private Slider _healthSlider;
    //Movement
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    private PlayerInputHandler _inputHandler;
    private Rigidbody2D rb;
    private Vector2 _moveDirection;
    [SerializeField] private Vector3[] _spawnPositions;
    //Shoot
    public GameObject bulletPrefab; // Prefab do proj�til
    public Transform leftFirePoint; // Ponto de origem do disparo para a esquerda
    public Transform rightFirePoint; // Ponto de origem do disparo para a direita
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f; // Cad�ncia de tiros ajust�vel para o lado direito
    private float nextLeftFireTime = 0f; // Momento do pr�ximo disparo para o lado esquerdo
    private float nextRightFireTime = 0f; // Momento do pr�ximo disparo para o lado direito


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _moveDirection = Vector2.zero;
        _healthSlider.maxValue = health.Value;
        health.OnValueChanged += (float old, float newVal) =>
        {
            _healthSlider.value = (float)newVal;
        };
        if (IsLocalPlayer)
        {
            
            Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
            GetComponent<PlayerInput>().enabled = true;
            _inputHandler = GetComponent<PlayerInputHandler>();
        }
    }
    void Update()
    {
        if (IsClient)
            if (!IsLocalPlayer) return;
            if (_inputHandler.movementInput != _moveDirection)
            _moveDirection = _inputHandler.movementInput;
            MoveServerRpc(_moveDirection);
            if (_inputHandler.LeftFired && Time.time >= nextLeftFireTime)
            {
                //Vector3 fireSpawnPointE = leftFirePoint.position;
                FireShootServerRpc(leftFirePoint.position, leftFirePoint.rotation); // Dispara para a esquerda
                nextLeftFireTime = Time.time + 1f / fireRate; // Atualiza o momento do pr�ximo disparo com base na cad�ncia
        }
            if (_inputHandler.RightFired && Time.time >= nextRightFireTime)
            {
               // Vector3 fireSpawnPointD = rightFirePoint.position;
                FireShootServerRpc(rightFirePoint.position, rightFirePoint.rotation ); // Dispara para a direita
            nextRightFireTime = Time.time + 1f / fireRate;
        }
    }
    [ServerRpc]
    public void MoveServerRpc(Vector2 move)
    {
        _moveDirection = move.normalized;
    }
    [ServerRpc]
    public void FireShootServerRpc(Vector3 firePointSpawn, Quaternion firePointRotation)
    {
        GameObject go = Instantiate(bulletPrefab, firePointSpawn, firePointRotation );
        go.GetComponent<NetworkObject>().Spawn();
        var bullet = go.GetComponent<ShootController>();
        bullet.SetVelocity(go.transform.right * bulletSpeed);

       
    }
    
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            int index = (int)OwnerClientId;
            transform.position = _spawnPositions[index];
        }
    }
    public void ApplyDamage(float damage)
    {
        if (IsServer)
        {
            float novaSaude = health.Value - damage;
            if (novaSaude <= 0)
            {
                Debug.Log("Morreu");
                Destroy(gameObject);
            }
            else{
                health.Value = novaSaude;
                // _healthSlider.value = novaSaude;  // Atualiza o valor do slider
                

            }
            
        }
        
    }
    private void FixedUpdate()
    {
        if (IsServer)
        {
            float moveInput = -_moveDirection.y;
            float rotateInput = _moveDirection.x;

            // Rotacionar o barco
            if (rotateInput != 0)
            {

                float rotation = rotateInput * rotationSpeed * Time.fixedDeltaTime;
                transform.Rotate(0, 0, -rotation);
            }
            else
            {
                rb.angularVelocity = 0f;
            }

            // Mover o barco para frente ou para tr�s
            Vector2 movement = transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        
    }
}
