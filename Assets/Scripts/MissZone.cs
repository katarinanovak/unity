using UnityEngine;

public class MissZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            var gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                Ball ball = collision.gameObject.GetComponent<Ball>();
                gameManager.Miss(ball);
            }
            else
            {
                Debug.LogError("GameManager nije pronađen!");
            }
        }
    }



}
