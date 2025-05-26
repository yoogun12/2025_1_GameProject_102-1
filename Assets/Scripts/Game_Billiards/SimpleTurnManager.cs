using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    //���� ���� (��� ���� ����)
    public static bool canPlay = true;                              //���� ĥ �� �ִ���
    public static bool anyBallMoving = false;                       //� ���̶� �����̴���



    // Update is called once per frame
    void Update()
    {
        CheckAllBalls();                        //��� ���� ������ Ȯ��
        if(!anyBallMoving && !canPlay)          //��� ���� ���߸� �ٽ� ĥ �� �ְ� ��
        {
            canPlay = true;
            Debug.Log("�� ����! �ٽ� ĥ �� �ֽ��ϴ�. ");
        }
    }

    void CheckAllBalls()                    //��� ���� �ح����� Ȯ��
    {
        SimpleBallController[] allBalls = FindObjectsOfType<SimpleBallController>();         //���� �ִ� ��� �� ã��
        anyBallMoving = false;

        foreach(SimpleBallController ball in allBalls)
        {
            if(ball.IsMoving())
            {
                anyBallMoving = true;
                break;
            }
        }
    }

    public static void OnBallHit()          //���� �÷��� ���� �� ȣ��
    {
        canPlay = false;                    //�ٸ� ������ �� �����̰� ��
        anyBallMoving = true;
        Debug.Log("�� ����! ���� ���� �� ���� ��ٸ�����");
    }
}
