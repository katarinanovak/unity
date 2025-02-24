using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody2D rigidbody { get; private set; }
    public float speed = 200f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * speed);
    }


    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.linearVelocity = Vector2.zero;

        Invoke(nameof(SetRandomTrajectory), 1f);
    }
}
