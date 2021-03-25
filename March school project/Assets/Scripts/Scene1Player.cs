using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Scene1Player : MonoBehaviour
{
    float xInput;
    public float movePower;
    Rigidbody2D rb;
    bool facingRight = true;
    public float jumpPower;
    bool grounded;
    bool jump;
    
    public Camera cam;
    private Animator anim;
    bool gotKey = false;
    int keyCollected;
    public GameObject keyPic;
    public AudioSource jumpSFX;
    public AudioSource landingSFX;
    public AudioSource gameOverSFX;
    public AudioSource winSFX;




    // Start is called before the first frame update
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        keyCollected = 0;
        

    }

    // Update is called once per frame
    // Move
    void Update()
    {
       
            GetInputs();
            JumpInput();
        
    }
    
    void GetInputs()

    {

        
        xInput = Input.GetAxisRaw("Horizontal");
        if (xInput < 0 && facingRight)
        {
            flip();


        }
        else if (xInput > 0 && !facingRight)
        {
            flip();


        }
        if (xInput == 0 && grounded == true)
        {
            anim.Play("Idle");

        }
        else if (grounded == true && jump == false)
        {
            anim.Play("Run");

        }
        else if (grounded == false)
        {
            anim.Play("Jump");


        }

    }

    void Movement()
    {
        Vector2 MovementVector = new Vector2(xInput * movePower, 0);
        rb.AddForce(MovementVector, ForceMode2D.Impulse);

        if (xInput == 0 && rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    private void FixedUpdate()
    {
        Movement();
        JumpForce();
    }
    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            jump = true;
        }
    }


    //jump
    void JumpForce()
    {
        if (jump == true)
        {
            rb.AddForce(Vector2.up * jumpPower);
            jump = false;
            anim.Play("Jump");

        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
            landingSFX.Play();

        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        jumpSFX.Play();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "death zone")
        {
            
            Debug.Log("OW");
            gameOverSFX.Play();
            


            //Time.timeScale = 0;
            //fadePanel.DOFade(1, 1);


            StartCoroutine(ReloadLevel());
        }

        if (other.gameObject.tag == "key")
        {

            other.gameObject.SetActive(false);
            gotKey = true;



            PickedUp();
        }
        if (other.tag == "win" && gotKey)
        {
            StartCoroutine(WonLevel());
            winSFX.Play();

        }
        void PickedUp()
        {

            Debug.Log("MOI");
            keyCollected++;
            if (keyCollected == 1)
            {

                keyPic.SetActive(true);
            }

            //CameraFollow
            //  void cameraFollow()
            // {
            //   Vector3 followPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -90f);
            // cam.transform.position = followPosition;
            // }
            //private void LateUpdate()
            //{
            //  cameraFollow();
            //}

        }
    }
   
    IEnumerator WonLevel()
    {
        //Time.timeScale = 0;
        //fadePanel.DOFade(1, 1);
      
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(2);


    }
    IEnumerator ReloadLevel()
    {

        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(1);
    }
}
