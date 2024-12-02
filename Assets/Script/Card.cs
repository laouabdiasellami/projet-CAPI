using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private Animator anim;
    public CardScriptableGameObject cardType;

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
        Debug.Log("Enter");
        anim.SetBool("Over", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        Debug.Log("Exit");
        anim.SetBool("Over", false);
    }

    public void OnPointerClick(PointerEventData  eventData)
    {
        Debug.Log("Clicked");
        anim.Play("Card_Selected");

        GameManager.instance.NextPlayer(cardType);
    }

}
