using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;                                         //ť�� �̵� �ӵ�


    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, -moveSpeed * Time.deltaTime);                     //z�� ���̳ʽ� �������� �̵�

        if(transform.position.z<-20)                                     //ť�갡 z�� -20���Ϸ� ������ Ȯ��
        {
            Destroy(gameObject);                                         //�ڱ� �ڽ� ����
        }
    }
}
