using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset = new Vector3(0, 5, -10); 

    void LateUpdate()
    {
        if (player != null)
        {
            // Atualiza a posição da câmera com base no player e no offset
            transform.position = player.position + offset;
            transform.LookAt(player);
        }
    }
}
