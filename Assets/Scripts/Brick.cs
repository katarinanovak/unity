using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Transform row1; 
    public Transform row2; 
    public Sprite[] states = new Sprite[0];  
    public int points = 100; 
    public bool unbreakable;  
    public bool move; 
    public SpriteRenderer spriteRenderer;  
    private int health;

    public bool hasBonus;
    public GameObject bonusPrefab; 



    public float speed = 0.5f;  
    public float boundaryLeft = -8f; 
    public float boundaryRight = 8f;  

    private bool row1MovingRight = true;  
    private bool row2MovingRight = false; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);
        if (!unbreakable)
        {
            health = states.Length;
            if (health > 0)
            {
                spriteRenderer.sprite = states[health - 1];
            }
            else
            {
                Debug.LogError("States array is empty.");
            }
        }
    }

    private void Hit()
    {
        if (unbreakable)
        {
            return;
        }
        
        health--;
        if (health <= 0)
        {
            gameObject.SetActive(false);

            if (hasBonus && bonusPrefab != null)
            {
                Instantiate(bonusPrefab, transform.position, Quaternion.identity);
            }

        }
        else if (health - 1 >= 0 && health - 1 < states.Length)
        {
            spriteRenderer.sprite = states[health - 1];
        }
        else
        {
            Debug.LogError("Health index out of range: " + (health - 1));
        }
        FindObjectOfType<GameManager>().Hit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }


    void Update()
    {
        if (move)
        {
           
            if (row1MovingRight)
            {
                row1.Translate(Vector2.right * speed * Time.deltaTime);
                if (row1.position.x >= boundaryRight)
                    row1MovingRight = false;
            }
            else
            {
                row1.Translate(Vector2.left * speed * Time.deltaTime);
                if (row1.position.x <= boundaryLeft)
                    row1MovingRight = true;
            }

            if (row2MovingRight)
            {
                row2.Translate(Vector2.right * speed * Time.deltaTime);
                if (row2.position.x >= boundaryRight)
                    row2MovingRight = false;
            }
            else
            {
                row2.Translate(Vector2.left * speed * Time.deltaTime);
                if (row2.position.x <= boundaryLeft)
                    row2MovingRight = true;
            }
        }
    }
}
