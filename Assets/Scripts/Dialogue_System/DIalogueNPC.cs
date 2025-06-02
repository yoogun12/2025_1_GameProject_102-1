using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;                   //npc ���� ���� ��ȭ ������

    private DialogueManager dialogueManager;            //��ȭ �Ŵ��� ����

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        if(dialogueManager == null)
        {
            Debug.LogError("���̾�α� �Ŵ����� �����ϴ�. ");
        }
    }


    void OnMouseDown()                  //���콺�� NPC�� Ŭ�� ������
    {
        if (dialogueManager == null) return;                       //�Ŵ����� ������ ���� ����
        if (dialogueManager.IsDialogueActive()) return;            //�̹� ��ȭ ���̸� ���� ����
        if (myDialogue == null) return;                            //��ȭ �����Ͱ� ������ ���� ����

        dialogueManager.StartDialogue(myDialogue);                 //��� ������ �����Ǹ� �� ��ȭ ����
    }

}
