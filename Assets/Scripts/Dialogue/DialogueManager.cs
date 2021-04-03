using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public GameObject portrait;

    public Animator anim;
    public Animator charAnim;

    private Queue<string> sentences;

    public QuestManager qm;

    public bool isTalking = false;

    //Dialogue Zoom
    private float camZoom = 7f;
    private float camNorm = 21f;
    private float zoomTime = 3f;
    public float smooth;
    private float currentCam;

    void Start()
    {
        currentCam = Camera.main.orthographicSize;
        qm = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
        sentences = new Queue<string>(); 
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && isTalking)
        {
            DisplayNextSentence();
        }

        if(isTalking  && currentCam > camZoom)
        {
            CamZoom();
        }
        if(!isTalking && currentCam < camNorm)
        {
            CamZoomOut();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //portrait.gameObject.GetComponent<Image>().sprite = dialogue.characterPic;

        if(dialogue.quest != "")
        {
            qm.CheckIfOnQuest(dialogue.quest);
        }


        nameText.text = dialogue.name;

        if (nameText.text == "Biker Bob")
        {
            charAnim.SetBool("isBikerBob", true);
        }

        if (nameText.text == "Norman Gnome")
        {
            charAnim.SetBool("isNormanGnome", true);
        }

        anim.SetBool("isOpen", true);

        isTalking = true;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.01f);
        }
    }

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        DiablePortraitAnim();
        isTalking = false;
    }

    public void DiablePortraitAnim()
    {
        charAnim.SetBool("isBikerBob", false);
        charAnim.SetBool("isNormanGnome", false);
    }

    public void CamZoom()
    {
        currentCam -= (smooth * Time.deltaTime);
        Camera.main.orthographicSize = currentCam;
    }

    public void CamZoomOut()
    {
        currentCam += (smooth * Time.deltaTime);
        Camera.main.orthographicSize = currentCam;
    }
}
