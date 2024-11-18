using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
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

/*
    private void OnMouseEnter()
    {
        Debug.Log("Enter");
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit");
    }

    */
}
