using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int gridWidth = 7;                           //���� ĭ ��
    public int gridHeight = 7;                          //���� ĭ ��
    public float cellSize = 1.43f;                      //�� ĭ�� ũ��
    public GameObject cellPrefabs;                      //��ĭ ������
    public Transform gridContainer;                     //�׸��带 ���� �θ� ������Ʈ

    public GameObject rankPrefabs;                      //����� ������
    public Sprite[] rankSprites;                        //�� ������ �Ա��� �̹���
    public int maxRankLevel = 7;                       //�ִ� �Ա��� ����

    public GridCell[,] grid;                            //��� ĭ�� �����ϴ� 2���� �迭

    void InitializeGrid()               //�׸��� �ʱ�ȭ
    {
        grid = new GridCell[gridWidth, gridHeight];         //2���� �迭 ����

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                    x * cellSize - (gridWidth * cellSize / 2) + cellSize / 2,
                    y * cellSize - (gridHeight * cellSize / 2) + cellSize / 2,
                    1f
                    );

                GameObject cellObj = Instantiate(cellPrefabs, position, Quaternion.identity, gridContainer);
                GridCell cell = cellObj.AddComponent<GridCell>();
                cell.Initialize(x,y);

                grid[x,y] = cell;       //�迭�� ����
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();

        for(int i = 0; i < 4; i++)      //4���� ����� ����
        {
            SpawnNewRank();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DraggableRank CreateRankInCell(GridCell cell, int level)
    {

        if (cell == null || !cell.IsEmpty()) return null;   //����ִ� ĭ�� �ƴϸ� ���� ����

        level = Mathf.Clamp(level, 1, maxRankLevel);

        Vector3 rankPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, 0f);   //����� ��ġ ����

        //�巡�� ������ �Ա��� ������Ʈ �߰�
        GameObject rankObj = Instantiate(rankPrefabs, rankPosition, Quaternion.identity, gridContainer);
        rankObj.name = "Rank_Lvel" + level;

        DraggableRank rank = rankObj.AddComponent<DraggableRank>();
        rank.SetRankLevel(level);

        return rank;

    }

    private GridCell FindEmptyCell()                //����ִ� ĭ ã��
    {
        List<GridCell> emptyCells = new List<GridCell>();           //�� ĭ���� ������ ����Ʈ

        for (int x = 0; x < gridHeight; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x,y].IsEmpty())            //ĭ�̶�� ����Ʈ�� �߰�
                {
                    emptyCells.Add(grid[x,y]);
                }
            }
        }

        if (emptyCells.Count == 0)                  //��ĭ�� ������ null �� ��ȯ
        {
            return null;
        }
        return emptyCells[Random.Range(0, emptyCells.Count)];           //�����ϰ� �� ĭ �ϳ� ����

    }

    public bool SpawnNewRank()          //�� ����� ����
    {
        GridCell emptyCell = FindEmptyCell();           //1. ����ִ� ĭ ã��
        if (emptyCell == null) return false;            //2. ����ִ� ĭ�� ������ ����

        int rankLevel = Random.Range(0, 100) < 80 ? 1 : 2;  //80% Ȯ���� ���� 1, 20%Ȯ���� ���� 2

        CreateRankInCell(emptyCell, rankLevel);     //3.����� ���� �� ����

        return true;
    }
}
