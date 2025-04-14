using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeGameUI : MonoBehaviour
{
    public TextMeshProUGUI TimerText;                                   //UI����
    public float Timer;                                                 //Ÿ�̸� ����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;                                            //Ÿ�̸� �ð��� �þ��.
        TimerText.text = "���� �ð� : " + Timer.ToString("0.00");           //���ڿ� ���·� ��ȯ�Ͽ� �����ش�.
    }
}
