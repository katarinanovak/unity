using UnityEngine;

public class BonusOrb : MonoBehaviour
{
    public float fallSpeed = 2f; 

    private void Update()
    {
        
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle")) 
        {
            FindObjectOfType<GameManager>().lives++; 
            Destroy(gameObject); 
        }
        else if (collision.gameObject.CompareTag("MissZone")) 
        {
            Destroy(gameObject);
        }
    }
}
