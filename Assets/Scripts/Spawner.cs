using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coinPrefabs;                                //���� �������� ���� �Ѵ�.
    public GameObject MissilePrefabs;

    [Header("���� Ÿ�̹� ����")]
    public float minSpawninterval = 0.5f;               //�ּ� ���� ����(��)
    public float maxSpawninterval = 2.0f;               //�ִ� ���� ����(��)

    [Header("���� ���� Ȯ�� ����")]
    [Range(0, 100)]
    public int coinSpawnChance = 50;                    //50%Ȯ���� ������ ���� �ȴ�.

    public float timer = 0.0f;                          //Ÿ�̸�
    public float nextSpawnTime;                         //���� ���� �ð�

    // Start is called before the first frame update
    void Start()
    {
        SetNextSpawnTime();                                   //�Լ� ȣ��
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;                                     //�ð��� 0���� ���� �����Ѵ�.

        if(timer >= nextSpawnTime)                                   //���� �ð��� �Ǹ� ������Ʈ�� ���� �Ѵ�
        {
            SpawnObject();
            timer = 0.0f;                                                  //�ð��� �ʱ�ȭ �����ش�.
            SetNextSpawnTime();                                            //�ٽ� �Լ� ȣ��
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(nextSpawnTime, minSpawninterval);   //�ּ�-�ִ� ������ ������ �ð� ����
    }

    void SpawnObject()
    {
        Transform spawnTransform = transform;               //������ ������Ʈ�� ��ġ�� ȸ������ �����´�.  (���� ����)

        //Ȯ���� ���� ���� �Ǵ� �̻��� ����
        int randomValue = Random.Range(0, 100);                    //0 - 100�� ���� ���� �̾Ƴ���.
        if(randomValue < coinSpawnChance)                          //0 - coinSpawnChance(ex 50) �����̸� ������ ���� �Ѵ�.
        {
            Instantiate(coinPrefabs, spawnTransform.position, spawnTransform.rotation); //���� �������� �ش� ��ġ�� ���� �Ѵ�.
        }
        else
        {
            Instantiate(MissilePrefabs, spawnTransform.position, spawnTransform.rotation); //�̻��� �������� �ش� ��ġ�� ���� �Ѵ�.
        }

    }
}
