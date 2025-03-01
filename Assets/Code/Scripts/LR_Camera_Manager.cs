using UnityEngine;

public class LR_CameraManager : MonoBehaviour
{
    [SerializeField] private Camera player1Camera;
    [SerializeField] private Camera player2Camera;

    private void Start()
    {
        if (player1Camera != null)
        {
            // Top half of the screen
            player1Camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
        }

        if (player2Camera != null)
        {
            // Bottom half of the screen
            player2Camera.rect = new Rect(0f, 0f, 1f, 0.5f);
        }
    }
}