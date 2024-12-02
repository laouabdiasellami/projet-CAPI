using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;
    public TextMeshProUGUI taskText;
    public Transform cards;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void DisplayTask(int index,string taskName)
    {
        taskText.text = "Task n°" + index + ": " + taskName;
    }

    public void NextTask()
    {
        GameManager.instance.TaskUpdate();
    }


    public void CardInit(CardScriptableGameObject[] deck)
    {
        foreach(CardScriptableGameObject card in deck)
        {
            Transform cardObj = ((GameObject)Instantiate(Resources.Load("Card"), cards)).transform;
            cardObj.GetChild(0).GetComponent<Card>().cardType = card;
        }
    }
}
