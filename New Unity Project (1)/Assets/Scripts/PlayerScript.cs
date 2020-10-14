using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    Animator anim;
    
    private Rigidbody2D rd2d;

    public AudioClip musicWin;

    public AudioSource musicSource;

    public float speed;

    public Text score;

    public Text winText;

    public Text lives;

    private int playerLives = 3;

    private int scoreValue = 0;

    private bool facingRight = true;

    private bool groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        lives.text = "Lives: " + playerLives.ToString();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (hozMovement != 0 && groundCheck == true)
        {
            anim.SetInteger("State Int", 2);
        }
        if (hozMovement == 0 && groundCheck == true)
        { 
            anim.SetInteger("State Int", 1);
        }
        /*if (vertMovement != 0)
        {
           anim.SetInteger("State Int", 3);
        }*/
    

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >= 8)
        {
            winText.text = "You win! Game created by Stephanie Smith";
            musicSource.clip = musicWin;
            musicSource.Play();
        }
        }
        if (collision.collider.tag == "Enemy")
        {
            playerLives -= 1;
            lives.text = "Lives: " + playerLives.ToString();
            Destroy(collision.collider.gameObject);
            if (playerLives <=0)
            {
                Destroy(gameObject);
                winText.text = "You Lose! Game created by Stephanie Smith";
            }
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(-24.5f,3.06f);
            playerLives = 3;
            lives.text = "Lives: "  + playerLives.ToString();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            groundCheck = true;
            if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                groundCheck = false;
                anim.SetInteger("State Int", 3);
            }
        }
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}