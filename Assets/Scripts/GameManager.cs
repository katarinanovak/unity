using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText { get; private set; }
    public TextMeshProUGUI healthText { get; private set; }

    // public Ball ball { get; private set; }

    public Ball[] balls { get; private set; }

    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    public int score=0;

    public int lives = 8;

    public int level = 1;

    public MissZone missZone { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        //this.ball = FindAnyObjectByType<Ball>();
        this.balls = FindObjectsOfType<Ball>();
        this.paddle = FindAnyObjectByType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();
        this.missZone = FindObjectOfType<MissZone>();

        this.scoreText = FindObjectOfType<Canvas>().transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        this.healthText = FindObjectOfType<Canvas>().transform.Find("HealthText").GetComponent<TextMeshProUGUI>();


        if (this.missZone == null)
        {
            Debug.LogError("MissZone nije pronađen u sceni!");
        }
        AssignBonusToRandomBrick(); 
        UpdateUI();
    }
    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 8;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene("Level" + level);


    }
    private void ResetLevel()
    {
        foreach (var ball in balls)
        {
            ball.gameObject.SetActive(true); 
            ball.ResetBall(); 
        }
        this.paddle.ResetPaddle();
    }


    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
       // NewGame();
    }
    public void Miss(Ball ball)
    {
        ball.gameObject.SetActive(false); 

        
        bool allBallsInactive = true;
        foreach (var b in balls)
        {
            if (b.gameObject.activeInHierarchy)
            {
                allBallsInactive = false;
                break;
            }
        }

        if (allBallsInactive)
        {
            this.lives--;
            UpdateUI();
            if (this.lives > 0)
            {
                ResetLevel();
            }
            else
            {
                GameOver();
            }
        }
    }


    public void Hit(Brick brick)
    {
        this.score += brick.points;
        UpdateUI();
        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }
 
    private bool Cleared()
    {
        for(int i=0; i< this.bricks.Length; i++)
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }

    //TESTIRANJE POSEBNOG NIVOA
    public void LoadSpecificLevel(int level)
    {
        LoadLevel(level);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            Debug.Log("T pressed, loading Level 5");
            //Promeniti na odgovarajuci nivo 1,2,3,4 ili 5
            LoadSpecificLevel(5);
        }
    }

    public void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + this.score;
        }

        if (healthText != null)
        {
            healthText.text = "Health: " + this.lives;
        }
    }

    private void AssignBonusToRandomBrick()
    {
        Brick[] breakableBricks = System.Array.FindAll(bricks, b => !b.unbreakable);
        if (breakableBricks.Length > 0)
        {
            int randomIndex = Random.Range(0, breakableBricks.Length);
            breakableBricks[randomIndex].hasBonus = true; 
            breakableBricks[randomIndex].bonusPrefab = Resources.Load<GameObject>("Circle"); 
        }
    }

}
