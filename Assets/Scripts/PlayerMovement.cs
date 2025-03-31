using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;                             //�̵� �ӵ� ���� ����
    public float jumpForce = 5.0f;                             //������ ħ ���� �ش�.

    public bool isGrounded = true;                             //���� �ִ��� üũ �ϴ� ���� (true/false)

    public int coinCount = 0;                                  //���� ȹ�� ���� ����
    public int totalCoins = 5;                                 //�� ���� ȹ�� �ʿ� ���� ����

    public Rigidbody rb;                                       //�÷��̾� ��ü�� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //������ �Է� 
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //�ӵ������� ���� �̵�
        rb.velocity = new Vector3(moveHorizontal * moveSpeed, rb.velocity.y, moveVertical * moveSpeed);

        //���� �Է�
        if(Input.GetButtonDown("Jump") && isGrounded) //&& �� ���� True �϶� -> (Jump ��ư {���� �����̽���} �� �� ���� ���� ��)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);       //�������� ������ ����ŭ ��ü�� ���� �����Ѵ�.
            isGrounded = false;                                           //������ �ϴ� ���� ������ �������� ������ Flase ��� �Ѵ�.
        }
    }

    void OnCollisionEnter(Collision collision)           //�浹�� �Ͼ���� ȣ�� �Ǵ� �Լ� 
    {
        if (collision.gameObject.tag == "Ground")            //�浹�� �Ͼ ��ü�� Tag�� Ground�� ���
        {
            isGrounded = true;                         //���� �浹 ������ true�� ���� ���ش�.
        }
    }

    void OnTriggerEnter(Collider other)         //Ʈ���� ���� �ȿ� ���Դٸ� �����ϴ� �Լ�
    {
        //���� ����
        if(other.CompareTag("Coin"))                                       //���� Ʈ���ſ� �浹 �ϸ�
        {
            coinCount++;                                                   //���� ���� 1 ���� ��Ų��. ++�� 1�� ���� ��Ų�ٴ� ���
            Destroy(other.gameObject);                                     //���� ������Ʈ�� �����Ѵ�.
            Debug.Log($"���� ���� : {coinCount}/{totalCoins}");            //������ ���� 1/5 ������ �ܼ� �α׿� ǥ���Ѵ�.
        }

        if(other.CompareTag("Door") && coinCount>=totalCoins)              //���� ��ü ī��Ʈ���� ������ �� ���� ���� ���
        {
            Debug.Log("���� Ŭ����");
            //���� �Ȥ��� ���� �� Scene ��ȯ �Ѵ�.
        }                                                                   
    }
}
