using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public string quest;
    public Text questText;
    public bool isAlreadyOnQuest;

    //Totals required
    public int plantsRequired;
    public int recyclablesRequired;

    //Check if quest has been done
    public bool plantingDone;
    public bool reclycingDone;
    public string finishedQuest;

    public int questsCompleted;
    public bool bothQuestsCompleted;

    public GameMaster gm;

    void Start()
    {
        plantsRequired = 2;
        recyclablesRequired = 2;

        plantingDone = false;
        reclycingDone = false;

        finishedQuest = "";
        questsCompleted = 0;

        questText.text = "";

        gm = GameObject.Find("GameManager").GetComponent<GameMaster>();
        isAlreadyOnQuest = false;
    }

    public void CheckIfOnQuest(string tempQuest)
    {                   
        if(!isAlreadyOnQuest && tempQuest != finishedQuest)
        {

            quest = tempQuest;
            finishedQuest = tempQuest;
            isAlreadyOnQuest = true;
            StartQuest();
        }        
        else
        {
            return;
        }
    }

    public void StartQuest()
    {

        if (quest == "plants")
        {
            if(!plantingDone)
            {
                isAlreadyOnQuest = true;
                questText.text = "Plant 30 Plants in the Garden:  " + gm.plantsPlanted + "/30";
            }

        }
        else if (quest == "recycle")
        {
            if(!reclycingDone)
            {
                isAlreadyOnQuest = true;
                questText.text = "Recycle 10 Pieces of Garbage:  " + gm.trashRecycled + "/10";
            }

        }
    }

    public void UpdateQuestText()
    {
        if (quest == "plants")
        {
            isAlreadyOnQuest = true;
            questText.text = "Plant 30 Plants in the Garden:  " + gm.plantsPlanted + "/30";
        }
        else if (quest == "recycle")
        {
            isAlreadyOnQuest = true;
            questText.text = "Recycle 10 Pieces of Garbage:  " + gm.trashRecycled + "/10";
        }
    }
    public void CheckQuestCompleted()
    {
        if (quest == "plants")
        {
            if (gm.plantsPlanted >= plantsRequired)
            {
                StartCoroutine(QuestCompleted());
                plantingDone = true;
            }
        }
        else if (quest == "recycle")
        {
            if(gm.trashRecycled >= recyclablesRequired)
            {
                StartCoroutine(QuestCompleted());
                reclycingDone = true;
            }
        }
        else
        {
            return;
        }
    }

    public IEnumerator QuestCompleted()
    {
        isAlreadyOnQuest = false;
        questText.text = "Quest Completed!";
        yield return new WaitForSeconds(3);

        questsCompleted++;
        if (questsCompleted == 2)
        {
            bothQuestsCompleted = true;
            questText.text = "Both Quests Completed!!";
        }
        else
        {
            questText.text = "";
        }
    }
}
