using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;
    [Header("MainUi")]
    public Transform main;
    public TextMeshProUGUI taskText;
    public Transform cards;
    public TextMeshProUGUI playerName;
    [Header("Transition")]
    public TextMeshProUGUI nextPlayer;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI player1;
    public TextMeshProUGUI player2;

    private float time=0;
    private Animator anim;

    void Awake()
    {
        if (instance == null)
            instance = this;

        anim = transform.GetComponent<Animator>();
    }

    public void TalkTime()
    {
        StartCoroutine(WaitTalke());
    }

    public void EndTalke()
    {
        StopAllCoroutines();
        main.gameObject.SetActive(false);
        anim.Play("Discution_Out");
        NextPlayer(GameManager.instance.playerListe[0]);
    }

    private IEnumerator WaitTalke()
    {
        anim.Play("Discution_In");
        yield return new WaitForSeconds(5);
        main.gameObject.SetActive(false);
        anim.Play("Discution_Out");
        yield return new WaitForSeconds(0.15f);
        NextPlayer(GameManager.instance.playerListe[0]);
    }


    public void NextTask(int index, string taskName)
    {
        StartCoroutine(WaitTask(index,taskName));
    }

    private IEnumerator WaitTask(int index, string taskName)
    {
        anim.Play("Task_In");
        yield return new WaitForSeconds(1);
        main.gameObject.SetActive(false);
        taskText.text = "Task n°" + index + ": " + taskName;
        anim.Play("Task_Out");
        yield return new WaitForSeconds(0.25f);
        NextPlayer(GameManager.instance.playerListe[0]);
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

    public void PlayerStart()
    {
        anim.Play("NextPlayer_Out");
        main.gameObject.SetActive(true);
        CardRest();
        //Start Timer
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
