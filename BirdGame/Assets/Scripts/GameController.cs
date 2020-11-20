using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Rigidbody2D rb;
    public float upForce;
    public GameObject go;
    public bool isDead;
    public Text scoretext;
    int score = 0;
    public Animator anim;

    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Bird Position
                rb.velocity = Vector2.zero;
                //Trigger for Bird Animate
                anim.SetTrigger("Tap");
                //AddForce to Bird for Fly.
                rb.AddForce(new Vector2(0, upForce));
            }
        }
        else if ((isDead == true) && Input.GetMouseButtonDown(0))
        {
            //When GameOver and Player Tap Mouse Button to Restart Game.
            SceneManager.LoadScene("Main");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D Called");

        //if (collision.tag == "ScoreTigger")
        {
            //score increment
            score++;
            Debug.Log("Collision Method Called");
            //display score
            scoretext.text = "SCORE : " + score;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Method Called");
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Column")
        {
            anim.SetTrigger("Die");
            isDead = true;
            go.SetActive(true);
        }
    }
}
