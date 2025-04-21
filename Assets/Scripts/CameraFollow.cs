using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                                                //���� ���(�÷��̾�)
    public Vector3 offset = new Vector3(0, 5, -10);                        //�÷��̾�� ������ �Ÿ�
    public float smoothSpeed = 0.125f;                                      //���󰡴� �ӵ�


    private void LateUpdate()
    {
        //LeteUpdate�� ����ϴ� ������ ī�޶� �÷��̾��� �̵��� ��� ó���� ���Ŀ� ���󰡰� �ϱ� ����
        Vector3 desiredPosition = target.position + offset;                                             //ī�޶� ��ġ ����
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);        //���� ��ġ ����
        transform.position = smoothPosition;                                                            //���� ������Ʈ ��ġ�� ����ش�.

        transform.LookAt(target.position);                                                           //ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����
    }
}
