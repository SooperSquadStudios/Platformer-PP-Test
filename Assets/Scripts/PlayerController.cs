using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float jumpForce;

    private bool isGrounded;
    private bool isJumping;
    public Transform feetPos;
    public float checkRadius;

    private float jumpTimeCounter;
    [SerializeField] private float jumpTime;

    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
            if (isGrounded == true && Input.GetMouseButtonDown(0))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
            }
            if (Input.GetMouseButton(0) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isJumping = false;
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            GameManager.instance.isGameOver = true;
            GameManager.instance.gameOverPanel.SetActive(true);
            if(GameManager.instance.score > GameManager.instance.highScore)
            {
                GameManager.instance.highScore = GameManager.instance.score;
                PlayerPrefs.SetFloat("HighScore", GameManager.instance.highScore);
                GameManager.instance.highScoreText.text = "HighScore: " + GameManager.instance.highScore.ToString("0");
            }
        }
    }
}
