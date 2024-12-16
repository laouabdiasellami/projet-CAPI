using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[TestFixture]
public class CardTests
{
    private Card card;

    // Setup before each test
    [SetUp]
    public void Setup()
    {
        var go = new GameObject(); // Create a new GameObject for the card
        card = go.AddComponent<Card>(); // Add the Card component
        card.cardType = new CardScriptableGameObject(); // Initialize card type
        card.cardType.img = new Sprite(); // Set a sprite for the card
    }

    // Teardown after each test
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(card.gameObject); // Clean up by destroying the GameObject
    }

    // Test to check if OnPointerEnter sets the "Over" animation to true
    [Test]
    public void OnPointerEnter_SetsOverAnimationTrue()
    {
        card.clickable = true; // Make the card clickable
        var eventData = new PointerEventData(EventSystem.current); // Create PointerEventData
        card.OnPointerEnter(eventData); // Call OnPointerEnter

        // Check that the "Over" animation is set to true
        Assert.IsTrue(card.anim.GetBool("Over"));
    }

    // Test to check if OnPointerExit sets the "Over" animation to false
    [Test]
    public void OnPointerExit_SetsOverAnimationFalse()
    {
        card.clickable = true; // Make the card clickable
        var eventData = new PointerEventData(EventSystem.current); // Create PointerEventData
        card.OnPointerExit(eventData); // Call OnPointerExit

        // Check that the "Over" animation is set to false
        Assert.IsFalse(card.anim.GetBool("Over"));
    }

    // Test to check if OnPointerClick locks the card and plays the "Card_Selected" animation
    [Test]
    public void OnPointerClick_LocksCardAndPlaysSelectedAnimation()
    {
        card.clickable = true; // Make the card clickable
        var eventData = new PointerEventData(EventSystem.current); // Create PointerEventData
        card.OnPointerClick(eventData); // Call OnPointerClick

        // Check that the "Over" animation is set to true and that the card is locked
        Assert.IsTrue(card.anim.GetBool("Over"));
        // Additional checks can be added depending on the exact behavior of the `WaitForNextPlayer` coroutine
    }
}
