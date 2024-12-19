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
    public TextMeshProUGUI playerTimerTxt;
    [Header("Transition")]
    public TextMeshProUGUI nextPlayer;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI player1;
    public TextMeshProUGUI player2;

    private Animator anim;


    public float timePlayer;
    public float timeTalke;

    private bool playerTimer = false;
    private float playerTime;
    private bool talkeTimer = false;
    private float talkeTime;
    private int minutes;
    private int secondes;

    void Awake()
    {
        if (instance == null)
            instance = this;

        anim = transform.GetComponent<Animator>();
    }

    public void Update()
    {
        //Timer disscution
        if(talkeTimer)
        {
            if(talkeTime>0)
            {
                talkeTime -= Time.deltaTime;
                minutes = Mathf.FloorToInt(talkeTime / 60f);
                secondes = Mathf.FloorToInt(talkeTime - minutes * 60f);
                timer.text = string.Format("{0:00}:{1:00}", minutes, secondes);
            }
            else
            {
                talkeTimer = false;
                talkeTime = timeTalke;
            }
        }

        //Timer Joueur
        if (playerTimer)
        {
            if (playerTime > 0)
            {
                playerTime -= Time.deltaTime;
                minutes = Mathf.FloorToInt(playerTime / 60f);
                secondes = Mathf.FloorToInt(playerTime - minutes * 60f);
                playerTimerTxt.text = string.Format("{0:00}:{1:00}", minutes, secondes);
            }
            else
            {
                playerTimer = false;
                playerTime = timePlayer;
            }
        }
    }

    public void TalkTime(string p1, string p2)
    {
        player1.text = p1;
        player2.text = p2;
        StartCoroutine(WaitTalke());
    }

    public void EndTalke()
    {
        talkeTimer = false;
        StopAllCoroutines();
        main.gameObject.SetActive(false);
        anim.Play("Discution_Out");
        NextPlayer(GameManager.instance.playerListe[0]);
    }

    private IEnumerator WaitTalke()
    {
        anim.Play("Discution_In");
        yield return new WaitForSeconds(0.15f);
        talkeTime = timeTalke;
        talkeTimer = true;
        yield return new WaitForSeconds(timeTalke);
        talkeTimer = false;
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
        StartCoroutine(WaitNextPlayer(player));
    }

    private IEnumerator WaitNextPlayer(string player)
    {
        yield return new WaitForSeconds(1);
        playerName.text = player;
    }

    public void PlayerStart()
    {
        main.gameObject.SetActive(true);
        CardRest();
        StartCoroutine(WaitPlayer());
    }

    public void EndPlayer()
    {
        CardLock(true);
        playerTimer = false;
        StopAllCoroutines();
    }

    private IEnumerator WaitPlayer()
    {
        anim.Play("NextPlayer_Out");
        yield return new WaitForSeconds(0.15f);
        playerTime = timePlayer;
        playerTimer = true;
        yield return new WaitForSeconds(timePlayer);
        playerTimer = false;
        CardScriptableGameObject card = new CardScriptableGameObject();
        card.value = "?";
        GameManager.instance.NextPlayer(card);
    }

    public void EndGame()
    {
        anim.Play("EndGame_In");
    }

    /// <summary>
    /// Spawn all cards from deck
    /// </summary>
    /// <param name="deck">List of all cards who muste be spawned</param>
    public void CardInit(CardScriptableGameObject[] deck)
    {
        foreach(CardScriptableGameObject card in deck)
        {
            Transform cardObj = ((GameObject)Instantiate(Resources.Load("Card"), cards)).transform;
            cardObj.GetChild(0).GetComponent<Card>().cardType = card;
        }
    }

    /// <summary>
    /// Reset all cards to default state
    /// </summary>
    public void CardRest()
    {
        for(int i =0; i<cards.childCount;i++)
        {
            cards.GetChild(i).GetComponent<Animator>().SetBool("Over", false);
            cards.GetChild(i).GetComponent<Animator>().Play("Card_idle");
            CardLock(false);
        }
    }

    /// <summary>
    /// Make all cards interactive or note
    /// </summary>
    /// <param name="state">Is the cards must be interactive</param>
    public void CardLock(bool state)
    {
        for (int i = 0; i < cards.childCount; i++)
        {
            cards.GetChild(i).GetChild(0).GetComponent<Card>().clickable = !state;
        }
    }
}
