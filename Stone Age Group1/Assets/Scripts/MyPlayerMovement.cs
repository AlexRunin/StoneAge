using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = animObject.GetComponent<SpriteRenderer>(); // ������� ������
        animator = animObject.GetComponent<Animator>(); // �������� ���������
    }

    // ��������
    private void FixedUpdate()
    {
        // �� �������� enum
        // ������� ���� ����� ����
        // 4 ������� UIExample
        rb.velocity = new Vector2(currentPlayerSpeed * Time.deltaTime, rb.velocity.y);  //���������� � �������� �������� ������� velocity

        animator.SetFloat("Speed", Mathf.Abs(currentPlayerSpeed)); // ��������� �������� � ��������
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        groundCheck = true;
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
