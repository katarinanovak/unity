using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BrickRowMovement : MonoBehaviour
{
    public Transform row1; 
    public Transform row2; 
    public Sprite[] states = new Sprite[0];
    public SpriteRenderer spriteRenderer;
    public Brick brick;

    public float speed = 0.5f; 
    public float boundaryLeft = -8f; 
    public float boundaryRight = 8f; 

    private bool row1MovingRight = true; 
    private bool row2MovingRight = false; 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
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

        // Drugi red: Kretanje u suprotnom smeru
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
    

    void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Ball")
        {
            Brick brick = this.GetComponent<Brick>();

           
            if (brick != null)
            {
               
                FindObjectOfType<GameManager>().Hit(brick);
            }

            
            Destroy(gameObject);
        }
    }

}
