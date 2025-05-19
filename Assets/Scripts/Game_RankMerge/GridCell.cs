using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public int x, y;                                    //�׸��忡���� ��ġ(��ǥ)
    public DraggableRank currentRank;                 //���� ĭ�� �ִ� �����
    public SpriteRenderer cellRenderers;                //ĭ�� �̹��� ������

    private void Awake()
    {
        cellRenderers = GetComponent<SpriteRenderer>();
    }


    //��ǥ �ʱ�ȭ
    public void Initialize(int gridX, int gridY)
    {
        x= gridX;
        y= gridY;
        name = "Cell_" + x + "_" + y;                   //�̸� ����
    }

    public bool IsEmpty()                   //ĭ�� ��� �ִ��� Ȯ��
    {
        return currentRank == null;         //��� ������ True �ƴϸ� False
    }

    public bool ContainPosition(Vector3 position)   //Ư�� ��ġ�� �� ĭ �ȿ� �ִ��� Ȯ��
    {
        Bounds bounds = cellRenderers.bounds;           //ĭ�� ��� ���� ��������
        return bounds.Contains(position);               //��ġ�� ��� �ȿ� �ִ��� Ȯ��
    }

    public void SetRank(DraggableRank rank)         //ĭ�� ����� ����
    {
        currentRank = rank;                         //���� �Ա��� ����

        if (rank != null)
        {
            rank.currentCell = this;                //����忡 ���� ĭ ���� �˷��ֱ�
        }

        rank.originalPosition = new Vector3(transform.position.x, transform.position.y, 0); //Z��ġ�� 0���� ����
        rank.transform.position = new Vector3(transform.position.x, transform.position.y, 0);   //������� ���� ĭ ��ġ�� �̵�

    }




}
