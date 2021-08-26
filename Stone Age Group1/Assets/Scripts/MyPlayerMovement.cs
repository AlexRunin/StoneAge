using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))] // ��������� ������� ������������� ����������
public class MyPlayerMovement : MonoBehaviour
{
    [Header("Player Property")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpForce;

    [SerializeField] private GameObject animObject;
    [SerializeField] private Animator animator; // �������� ���������

    private SpriteRenderer spriteRenderer;

    private float currentPlayerSpeed; // ��� �� ������� = 0, � ��� ��������� 
    private Rigidbody2D rb;
    private bool groundCheck; // �������� �����

    private string lastObjectEated;
    Vector3 defaultPosition;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = animObject.GetComponent<SpriteRenderer>(); // ������� ������
        animator = animObject.GetComponent<Animator>(); // �������� ���������
        defaultPosition = this.transform.position;
    }

    // ��������
    private void FixedUpdate()
    {
        /*
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * playerSpeed * Time.deltaTime;

        currentPlayerSpeed = move.x * playerSpeed;

        if (currentPlayerSpeed > 0)
        {
            RightMove();
        }
        else if (currentPlayerSpeed < 0)
        {
            LeftMove();
        }

        if (move.y > 0)
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            //Throw();
        }
        */
       
        // �� �������� enum
        // ������� ���� ����� ����
        // 4 ������� UIExample
        rb.velocity = new Vector2(currentPlayerSpeed * Time.deltaTime, rb.velocity.y);  //���������� � �������� �������� ������� velocity

        animator.SetFloat("Speed", Mathf.Abs(currentPlayerSpeed)); // ��������� �������� � ��������
    }

    public void Attack()
    {
        Debug.Log("Attack");
        animator.SetTrigger("Attack");
    }


    public void RightMove()
    {
        currentPlayerSpeed = playerSpeed;
        spriteRenderer.flipX = false;
      
    }
    public void LeftMove()
    {
        currentPlayerSpeed = -playerSpeed;
        spriteRenderer.flipX = true;
        
    }

    public void StopMove()
    {
        currentPlayerSpeed = 0f;
    }
    public void Jump()
    {
        if (groundCheck)
        {
            animator.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, playerJumpForce); // rb.velocity.x - �� ����������� ��������� ����
            groundCheck = false;
        }
    }

    public void Health()
    { 
        
    }

    public void EateFruit(GameObject banana)
    {
        Debug.Log("Eat banana");

        GameObject.Destroy(banana);

        //ChangeHealth(-0.15f);
    }

    public void EateMushroom(GameObject mushroom)
    {
        Debug.Log("Eating mushrooms");

        GameObject.Destroy(mushroom);

        //ChangeHealth(0.15f);
    }

    public void EateOrange(GameObject orange)
    {
        Debug.Log("Eating orange");

        GameObject.Destroy(orange);

        //ChangeHealth(0.15f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundCheck = true;

        switch (collision.tag)
        {
            case "Banana":
                {
                    EateFruit(collision.gameObject);
                    lastObjectEated = collision.tag;
                }
                break;
            case "Mushroom":
                {
                    EateMushroom(collision.gameObject);
                    lastObjectEated = collision.tag;
                }
                break;
            case "Orange":
                {
                    EateOrange(collision.gameObject);
                    lastObjectEated = collision.tag;
                }
                break;
            case "SpecialMushroom":
                {
                    EateMushroom(collision.gameObject);
                    lastObjectEated = collision.tag;
                }
                break;
            case "Water":
                {
                    if (lastObjectEated != "SpecialMushroom")
                    {
                        this.transform.position = defaultPosition;
                    }
                    else
                    {
                        SceneManager.LoadScene(0);
                    }
                    lastObjectEated = collision.tag;
                }
                break;
            
        }
    }




    // ��������
    //if (transform.localScale.x > 0)
    //{
    //    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //}

    //if (transform.localScale.x < 0)
    //{
    //    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //}
}
