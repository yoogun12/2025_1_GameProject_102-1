using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//5�� ���� 5�� ���ǵ�� ������ �̵� �ϰ� ������� ������Ʈ Ŭ���� (������Ʈ������ �� Z��)
public class ZAxisMover : MonoBehaviour          //z������ �̵� �ϴ� Ŭ����
{
    public float speed = 5.0f;                   //�̵� �ӵ� ����
    public float timer = 5.0f;                   //Ÿ�̸� ����

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);   //z�� �������� ������ �̵� [Translate �Լ��� ����Ͽ�]

        timer -= Time.deltaTime;                                 //�ð��� ī��Ʈ �ٿ� �Ѵ�.
        if ( timer < 0 )                                         //�ð��� ���� �Ǹ�
        {                                                        
            Destroy(gameObject);                                 //�ڱ� �ڽ��� �ı� �Ѵ�.
        }
    }
}
