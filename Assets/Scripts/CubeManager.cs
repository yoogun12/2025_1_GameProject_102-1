using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public CubeGenerator[] generatedCubes = new CubeGenerator[5];              //Ŭ���� �迭

    public float timer = 0.0f;                                            //�ð� Ÿ�̸� ���� float
    public float interval = 3.0f;                                         //3�� ���� �� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;                        //Ÿ�̸� �ð��� �ø���.
        if (timer >= interval)                          //���͹� �ð� �̻��� ��
        {
            RandomizeCubeActivation();                  //�Լ� ȣ��
            timer = 0.0f;                               //Ÿ�̸� �ʱ�ȭ
        }
    }

    public void RandomizeCubeActivation()
    {
        for(int i=0; i<generatedCubes.Length; i++)        //�� ť�긦 �����ϰ� Ȱ��
        {
            int randomNum = Random.Range(0, 2);           //���� : 0 �Ǵ� 1 ���� ���� ���� 50% Ȯ���� �� ����
            if(randomNum == 1 )
            {
                generatedCubes[i].GenCube();              //ť�� Ŭ������ ���� �Լ��� ȣ�� �Ѵ�.
            }
        }
    }
}
