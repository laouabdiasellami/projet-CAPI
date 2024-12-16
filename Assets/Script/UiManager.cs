using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;
    [Header("MainUi")]
    public TextMeshProUGUI taskText;
    public Transform cards;
    public TextMeshProUGUI playerName;
    [Header("Transition")]
    public TextMeshProUGUI nextPlayer;

    private Animator anim;

    void Awake()
    {
        if (instance == null)
            instance = this;

        anim = transform.GetComponent<Animator>();
    }

    public void DisplayTask(int index,string taskName)
    {
        StartCoroutine(WaitNextTask(index, taskName));
    }

    private IEnumerator WaitNextTask(int index, string taskName)
    {
        yield return new WaitForSeconds(2);
        taskText.text = "Task n°" + index + ": " + taskName;
    }

    public void NextTask()
    {
        GameManager.instance.TaskUpdate();
    }

    public void NextPlayer(string player)
    {
        nextPlayer.text = player;
        anim.Play("NextPlayer_In");
        StartCoroutine(WaitForTransition(player));
    }

    private IEnumerator WaitForTransition(string player)
    {
        yield return new WaitForSeconds(1);
        playerName.text = player;
    }



    public void CardInit(CardScriptableGameObject[] deck)
    {
        foreach(CardScriptableGameObject card in deck)
        {
            Transform cardObj = ((GameObject)Instantiate(Resources.Load("Card"), cards)).transform;
            cardObj.GetChild(0).GetComponent<Card>().cardType = card;
        }
    }

    public void CardRest()
    {
        for(int i =0; i<cards.childCount;i++)
        {
            cards.GetChild(i).GetComponent<Animator>().SetBool("Over", false);
            cards.GetChild(i).GetComponent<Animator>().Play("Card_idle");
            CardLock(false);
        }
    }

    public void CardLock(bool state)
    {
        for (int i = 0; i < cards.childCount; i++)
        {
            cards.GetChild(i).GetChild(0).GetComponent<Card>().clickable = !state;
        }
    }
}
