using UnityEngine;
using Unity.Netcode;

public class ProjectileController : NetworkBehaviour
{
    public float speed = 20f; // Velocidade do proj�til
    public float lifetime = 5f; // Tempo de vida do proj�til antes da destrui��o
    private float currentLifetime = 0f; // Tempo de vida atual do proj�til

    private Rigidbody2D rb;

    private void Start()
    {
        currentLifetime = 0f;
        rb = GetComponent<Rigidbody2D>();

        // Aplica a velocidade inicial ao proj�til
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        // Define a velocidade inicial do proj�til
        
        // Atualiza o tempo de vida atual do proj�til
        currentLifetime += Time.deltaTime;

        // Destroi o proj�til se o tempo de vida atual exceder o tempo de vida m�ximo
        if (currentLifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com outro jogador
        if (other.CompareTag("Player"))
        {
            // Colocar aqui a l�gica de dano ou efeitos de colis�o, se necess�rio

            // Destroi o proj�til
            Destroy(gameObject);
        }
    }
    */
}
