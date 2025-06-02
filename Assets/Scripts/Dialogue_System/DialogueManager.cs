using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI ��� - Instector���� ����")]
    public GameObject DialoguePanel;                                    //��ȭâ ��ü �г�(ó���� ��Ȱ��ȭ)
    public Image characterImage;                                        //ĳ���� �̹��� ǥ���ϴ� Image UI
    public TextMeshProUGUI characternameText;                           //ĳ���� �̸� ǥ���ϴ� �ؽ�Ʈ
    public TextMeshProUGUI dialogueText;                                //��ȭ ���� �����ϴ� �ؽ�Ʈ
    public Button nextButton;                                           //"����" ��ư ( Ŭ�� �� ���� ��ȭ�� )

    [Header("�⺻ ����")]                                       
    public Sprite defaultCharacterImage;                                //ĳ���� �̹����� ���� �� ����� �⺻ �̹���

    [Header("Ÿ���� ȿ�� ����")]
    public float typingSpeed = 0.05f;                                   //���� �ϳ��� ��� �ӵ� ( �ʴ��� )
    public bool skipTypingOnClick = true;                               //Ŭ�� �� Ÿ���� ��� �Ϸ� �� �� ����

    //���� ������
    private DialogueDataSO currentDialogue;                                 //���� ���� ���� ��ȭ ������
    private int currentLineIndex = 0;                                       //���� �� ��° ��ȭ ������ ( 0���� ���� )
    private bool isDialogueActive = false;                                  //��ȭ�� ���� ������ Ȯ���ϴ� �÷���
    private bool isTyping = false;                                          //���� Ÿ���� ȿ���� ���� ������ Ȯ��
    private Coroutine typingCoroutine;                                      //Ÿ���� ȿ�� �ڷ�ƾ ���� ( ������ )

    // Start is called before the first frame update
    void Start()
    {
        DialoguePanel.SetActive(false);                              //��ȭâ �����
        nextButton.onClick.AddListener(HandleNextInput);            //"����" ��ư�� ���ο� �Է� ó�� ����
    }

    // Update is called once per frame
    void Update()
    {
        if(isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            HandleNextInput();                  //���� �Է� ó�� ( Ÿ���� ���̸� �Ϸ� , �ƴϸ� ������ )
        }
    }

    public void StartDialogue(DialogueDataSO dialogue)          //���ο� ��ȭ�� ���� �ϴ� �Լ�
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;          //��ȭ ������ ���ų� ��ȭ ������ ��������� ���� ���� ���� 

        //��ȭ ���� �غ�
        currentDialogue = dialogue;                         //���� ��ȭ ������ ����
        currentLineIndex = 0;                               //ù ��° ��ȭ ���� ����
        isDialogueActive = true;                            //��ȭ Ȱ��ȭ �÷��� ON

        //UI ������Ʈ
        DialoguePanel.SetActive(true);                      //��ȭâ ���̱�
        characternameText.text = dialogue.characterName;    //ĳ���� �̸� ǥ��

        if(characterImage != null)
        {
            if(dialogue.characterImage != null)
            {
                characterImage.sprite = dialogue.characterImage;        //��ȭ �������� �̹��� ���
            }
            else
            {
                characterImage.sprite = defaultCharacterImage;          //�⺻ �̹��� ���
            }
        }

        ShowCurrentLine();                                              //ù ��° ��ȭ ���� ǥ��
    }

    IEnumerator TypeText(string textToType)         //Ÿ���� �� ��ü �ؽ�Ʈ
    {
        isTyping = true;                //Ÿ���� ����
        dialogueText.text = "";         //�ؽ�Ʈ �ʱ�ȭ

        //�ؽ�Ʈ�� �� ���ھ� �߰�
        for(int i =0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];         //�� ���� �߰�
            yield return new WaitForSeconds(typingSpeed);           //��� �ð� ����
        }

        isTyping = false;                   //Ÿ���� �Ϸ�
    }

    private void CompleteTyping()           //Ÿ���� ȿ���� ��� �Ϸ� �ϴ� �Լ�
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);             //�ڷ�ƾ ����
        }

        isTyping = false;                   //Ÿ���� ���� ����

        //���� ���� ��ü �ؽ�Ʈ�� ��� ǥ��
        if(currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
        }
    }

    void ShowCurrentLine()      //���� ��ȭ ���� ������ Ÿ���� ȿ���� �Բ� ȭ�鿡 ǥ���ϴ� �Լ�
    {
        if(currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)   //��ȭ �����Ϳ� �ε����� ��ȣ���� Ȯ��
        {
            if(typingCoroutine != null)             //���� Ÿ���� ȿ���� �ִٸ� ����
            {
                StopCoroutine (typingCoroutine);
            }
            
            //���� ���� ��ȭ �������� Ÿ���� ȿ�� ����
            string currentText = currentDialogue.dialogueLines [currentLineIndex];
            typingCoroutine = StartCoroutine(TypeText(currentText));
        }
    }

    public void ShowNextLine()          //���� ��ȭ �ٷ� �̵� ��Ű�� �Լ� ( Ÿ������ �Ϸ�� �Ŀ��� ȣ�� )
    {
        currentLineIndex++;             //���� �ٷ� �ε��� ����

        //������ ��ȭ������ Ȯ��
        if(currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();                  //��ȭ�� �������� ���� �� ǥ�� 
        }
    }

    void EndDialogue()                  //��ȭ�� ������ ���� �ϴ� �Լ�
    {
        if(typingCoroutine != null)                 //Ÿ���� ȿ�� ����
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isDialogueActive = false;                           //��ȭ ��Ȱ��ȭ
        isTyping = false;                                   //Ÿ���� ���� ����
        DialoguePanel.SetActive(false);                     //��ȭâ �����
        currentLineIndex = 0;                               //�ε��� �ʱ�ȭ
    }

    public void HandleNextInput()               //�����̽��ٳ� ��ư Ŭ�� �� ȣ��Ǵ� �Է� ó�� �Լ�
    {
        if(isTyping && skipTypingOnClick)
        {
            CompleteTyping();                           //Ÿ���� ���̸� ��� �Ϸ�
        }
        else if(!isTyping)
        {
            ShowNextLine();                             //Ÿ���� �Ϸ� ���¸� ���� �ٷ�
        }
    }

    public void SkipDialogue()                  //��ȭ ��ü�� �ٷ� ��ŵ�ϴ� �Լ�
    {
        EndDialogue ();
    }

    public bool IsDialogueActive()              //���� ��ȭ�� ���� ������ Ȯ�� �ϴ� �Լ�
    {
        return isDialogueActive;
    }
}

