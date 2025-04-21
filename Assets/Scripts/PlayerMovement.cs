using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("�⺻ �̵� ����")]
    public float moveSpeed = 5.0f;                             //�̵� �ӵ� ���� ����
    public float jumpForce = 7.0f;                             //������ ħ ���� �ش�.
    public float turnSpeed = 10f;                              //ȸ�� �ӵ�

    [Header("���� ���� ����")]
    public float fallMultiplier = 2.5f;                        //�ϰ� �߷� ����
    public float lowJumpMultiplier = 2.0f;                     //ª�� ���� ����

    [Header("���� ���� ����")]
    public float coyoteTime = 0.15f;                           //���� ���� �ð�
    public float coyoteTimeCounter;                            //���� Ÿ�̸�
    public bool realGrouned = true;                            //���� ���� ����

    [Header("�۶��̴� ����")]
    public GameObject gliderObject;                            //�۶��̴� ������Ʈ
    public float gliderFallSpeed = 1.0f;                       //�۶��̴� ���� �ӵ�
    public float gliderMoveSpeed = 7.0f;                       //�۶��̴� �̵� �ӵ�
    public float gliderMaxTime = 5.0f;                         //�ִ� ��� �ð�
    public float gliderTimeLeft;                               //���� ��� �ð�
    public bool isGliding = false;                             //�۶��̵� ������ ����


    public bool isGrounded = true;                             //���� �ִ��� üũ �ϴ� ���� (true/false)

    public int coinCount = 0;                                  //���� ȹ�� ���� ����
    public int totalCoins = 5;                                 //�� ���� ȹ�� �ʿ� ���� ����

    public Rigidbody rb;                                       //�÷��̾� ��ü�� ����

    // Start is called before the first frame update
    void Start()
    {
        //�۶��̴� ������Ʈ �ʱ�ȭ
        if (gliderObject != null)
        {
            gliderObject.SetActive(false);                     //���� �� ��Ȱ��ȭ
        }

        gliderTimeLeft = gliderMaxTime;                        //�۶��̴� �ð� �ʱ�ȭ

        coyoteTimeCounter = 0;                                 //���� Ÿ�̸� �ʱ�ȭ   
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� Ȱ��ȭ
        UpdateGroundedState();


        //������ �Է� 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //�̵� ���� ����
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);           //�̵� ���� ����

        if (movement.magnitude > 0.1f)            //�Է��� ������ ȸ��
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);             //�̵� ������ �ٶ󺸵��� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        //GŰ�� �۶��̴� ���� ( ������ ���ȸ� Ȱ��ȭ)
        if (Input.GetKey(KeyCode.G) && !isGrounded && gliderTimeLeft > 0) //GŰ�� �����鼭 ,���� ���� �ʰ� , �۶��̴� ���� �ð��� ���� �� (3���� ����)
        {
            if (!isGliding)                  //�۶��̴� Ȱ��ȭ(������ �ִ� ����)
            {
                //�۶��̴� Ȱ��ȭ �Լ� ( �Ʒ� ���� )
                EnableGlider();
            }

            gliderTimeLeft -= Time.deltaTime;               //�۶��̴� ��� �ð� ����

            if (gliderTimeLeft <= 0)                          //�۶��̴� �ð��� �� �Ǹ� ��Ȱ��ȭ
            {
                //�۶��̴� ��Ȱ��ȭ �Լ� ( �Ʒ� ���� )
                DisableGlider();
            }
        }
        else if (isGliding)
        {
            //GŰ�� ���� �۶��̴� ��Ȱ��ȭ
            DisableGlider();
        }

        if(isGliding)
        {
            ApplyGliderMovement(moveHorizontal, moveVertical);
        }
        else//���� ������ �ڵ���� else �� �ȿ� �ִ´�.
        {
            //�ӵ������� ���� �̵�
            rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

            //���� ���� ���� ����   
            if (rb.velocity.y < 0)        //�ϰ� �ÿ�
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;        //�ϰ� �� �߷� ��ȭ
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))         //��� �� ���� ��ư�� ���� ���� ����
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }

        //���鿡 ������ �۶��̴� �ð� ȸ�� �� �۶��̴� ��Ȱ��ȭ
        if(isGrounded)
        {
            if(isGliding)
            {
                DisableGlider();
            }

            gliderTimeLeft = gliderMaxTime;             //���� ���� �� ȸ��
        }

        //���� �Է�
        if (Input.GetButtonDown("Jump") && isGrounded) //&& �� ���� True �϶� -> (Jump ��ư {���� �����̽���} �� �� ���� ���� ��)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);       //�������� ������ ����ŭ ��ü�� ���� �����Ѵ�.
            isGrounded = false;                                           //������ �ϴ� ���� ������ �������� ������ Flase ��� �Ѵ�.
            realGrouned = false;
            coyoteTimeCounter = 0;                                        //�ڿ��� Ÿ�� ��� ����
        }
    }

    void OnCollisionEnter(Collision collision)           //�浹�� �Ͼ���� ȣ�� �Ǵ� �Լ� 
    {
        if (collision.gameObject.tag == "Ground")            //�浹�� �Ͼ ��ü�� Tag�� Ground�� ���
        {
            realGrouned = true;                         //���� �浹 ������ true�� ���� ���ش�.
        }
    }
    void OnCollisionStay(Collision collision)           //�浹�� �Ͼ���� ȣ�� �Ǵ� �Լ� 
    {
        if (collision.gameObject.tag == "Ground")            //�浹�� �����Ǵ� ��ü�� Tag �� Ground �� ���
        {
            realGrouned = true;                         //�浹�� ���� �Ǳ� ������ true
        }
    }
    void OnCollisionExit(Collision collision)           //���鿡�� ���������� Ȯ��
    {
        if (collision.gameObject.tag == "Ground")
        {
            realGrouned = false;                         //���鿡�� �������� ������ false
        }
    }

    void OnTriggerEnter(Collider other)         //Ʈ���� ���� �ȿ� ���Դٸ� �����ϴ� �Լ�
    {
        //���� ����
        if (other.CompareTag("Coin"))                                       //���� Ʈ���ſ� �浹 �ϸ�
        {
            coinCount++;                                                   //���� ���� 1 ���� ��Ų��. ++�� 1�� ���� ��Ų�ٴ� ���
            Destroy(other.gameObject);                                     //���� ������Ʈ�� �����Ѵ�.
            Debug.Log($"���� ���� : {coinCount}/{totalCoins}");            //������ ���� 1/5 ������ �ܼ� �α׿� ǥ���Ѵ�.
        }

        if (other.CompareTag("Door") && coinCount >= totalCoins)              //���� ��ü ī��Ʈ���� ������ �� ���� ���� ���
        {
            Debug.Log("���� Ŭ����");
            //���� �Ȥ��� ���� �� Scene ��ȯ �Ѵ�.
        }
    }

    //���� ���� ������Ʈ �Լ�
    void UpdateGroundedState()
    {
        if (realGrouned)                                    //���� ���鿡 ������ �ڿ��� Ÿ�� ����
        {
            coyoteTimeCounter = coyoteTime;
            isGrounded = true;
        }
        else
        {
            //�����δ� ���鿡 ������ �ڿ��� Ÿ�� ���� ������ ������ �������� �Ǵ�
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;                                //�ð��� ���������� ���� ��Ų��.
                isGrounded = true;
            }
            else
            {
                isGrounded = false;                                                  //Ÿ���� ������ FALSE
            }
        }
    }

    void EnableGlider()                 //�۶��̴� Ȱ��ȭ �Լ�
    {
        isGliding = true;

        if (gliderObject != null)            //�۶��̴� ������Ʈ ǥ��
        {
            gliderObject.SetActive(true);
        }
        rb.velocity = new Vector3(rb.velocity.x, -gliderFallSpeed, rb.velocity.z);          //�ϰ� �ӵ��� �ʱ�ȭ
    }

    void DisableGlider()                //�۶��̴� ��Ȱ��ȭ �Լ�
    {
        isGliding = false;

        if (gliderObject != null)
        {     //�۶��̴� ������Ʈ �����
            {
                gliderObject.SetActive(false);
            }

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);           //��� �����ϵ��� �߷� ����
        }
    }

        void ApplyGliderMovement(float horizontal, float vertical)
        {
            Vector3 gliderVelocity = new Vector3(horizontal * gliderMoveSpeed, -gliderFallSpeed, vertical * gliderMoveSpeed);

            rb.velocity = gliderVelocity;
        }
    }
