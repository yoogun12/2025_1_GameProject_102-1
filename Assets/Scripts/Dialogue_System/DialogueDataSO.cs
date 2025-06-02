using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue" , menuName = "Dialogue/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
    [Header("ĳ���� ����")]
    public string characterName = "ĳ����";                        //��ȭâ�� ǥ�õ� ĳ���� �̸�
    public Sprite characterImage;                                   //ĳ���� �� �̹���

    [Header("��ȭ ����")]
    [TextArea(3, 10)]                                                //Inspector���� ���� �� �Է� �����ϰ� ����
    public List<string> dialogueLines = new List<string>();          //��ȭ ����� ( ������� ��µ� )
}
