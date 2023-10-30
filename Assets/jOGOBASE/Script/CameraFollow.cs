using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string targetTag = "Player"; // Tag do jogador
    public float smoothSpeed = 0.125f; // Velocidade de suaviza��o

    private Transform target; // Refer�ncia din�mica para o transform do jogador
    private Vector3 offset; // Dist�ncia inicial entre a c�mera e o jogador

    private void Start()
    {
        FindPlayer(); // Encontra o jogador no in�cio
    }

    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(targetTag); // Encontra o GameObject do jogador pela tag
        if (player != null)
        {
            target = player.transform; // Define o jogador como alvo
            offset = transform.position - target.position; // Calcula a dist�ncia inicial entre a c�mera e o jogador
        }
        else
        {
            Debug.LogWarning("Player not found with tag " + targetTag); // Avisa se o jogador n�o for encontrado
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            FindPlayer(); // Encontra o jogador se ele n�o existir ainda
            return;
        }

        Vector3 desiredPosition = target.position + offset; // Calcula a posi��o desejada da c�mera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suaviza o movimento
        transform.position = smoothedPosition; // Atualiza a posi��o da c�mera
    }
}
