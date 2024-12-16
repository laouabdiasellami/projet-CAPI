using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Animator anim;
    public CardScriptableGameObject cardType;
    public bool clickable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        transform.GetComponent<Image>().sprite = cardType.img;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (clickable)
            anim.SetBool("Over", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (clickable)
            anim.SetBool("Over", false);
    }

    public void OnPointerClick(PointerEventData  eventData)
    {
        if(clickable)
        {
            UiManager.instance.CardLock(true);
            anim.Play("Card_Selected");
            StartCoroutine(WaitForNextPlayer());
        }

    }

    private IEnumerator WaitForNextPlayer()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.NextPlayer(cardType);
    }

}
