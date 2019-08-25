using UnityEngine;

public class Ball : MonoBehaviour 
{
    // Config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField]float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds; 
    [SerializeField] float randomFactor = 0;
    

    //State
    Vector2 paddleToBallvector;
    bool hasStarted = false;

    // Cached components ref
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2d;

    void Start()
    {
        paddleToBallvector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2d = GetComponent<Rigidbody2D>();  
    }
 
    void Update()
    {
        if(!hasStarted) 
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2d.velocity = new Vector2(xPush,yPush);
        }
    }

    private void LockBallToPaddle() 
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallvector;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f,randomFactor),
            Random.Range(0f,randomFactor)); 

        if(hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2d.velocity += velocityTweak;  
        }
        
    }
}
