/*
using NUnit.Framework;
using UnityEngine;
using TMPro;

[TestFixture]
public class UiManagerTests
{
    private UiManager uiManager;

    // Setup before each test
    [SetUp]
    public void Setup()
    {
        var go = new GameObject(); // Create a new GameObject to attach the UiManager
        uiManager = go.AddComponent<UiManager>(); // Add the UiManager component
        
        // Create necessary UI elements
        uiManager.taskText = new GameObject().AddComponent<TextMeshProUGUI>();
        uiManager.playerName = new GameObject().AddComponent<TextMeshProUGUI>();
        uiManager.nextPlayer = new GameObject().AddComponent<TextMeshProUGUI>();
    }

    // Teardown after each test
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(uiManager.gameObject); // Destroy the GameObject to clean up
    }

    // Test to check if the Singleton pattern is enforced
    [Test]
    public void Singleton_IsEnforced()
    {
        var instance1 = uiManager; // Reference the instance created in Setup
        var instance2 = new GameObject().AddComponent<UiManager>(); // Create a new GameObject with another UiManager

        // Ensure both instances refer to the same singleton instance
        Assert.AreSame(instance1, UiManager.instance);
        
        Object.Destroy(instance2.gameObject); // Clean up
    }

    // Test to check if the task is displayed correctly
    [Test]
    public void DisplayTask_ShowsCorrectTask()
    {
        uiManager.DisplayTask(1, "Task 1"); // Display task 1
        
        // Check that the task text is set correctly
        Assert.AreEqual("Task n°1: Task 1", uiManager.taskText.text);
    }

    // Test to check if the player name is updated correctly
    [Test]
    public void NextPlayer_UpdatesPlayerName()
    {
        uiManager.NextPlayer("Player1"); // Set the next player as "Player1"
        
        // Check that the player name is updated
        Assert.AreEqual("Player1", uiManager.playerName.text);
    }

    // Test to check if cards are initialized correctly
    [Test]
    public void CardInit_CreatesCards()
    {
        var deck = new CardScriptableGameObject[] { new CardScriptableGameObject(), new CardScriptableGameObject() }; // Simulate a deck of 2 cards
        uiManager.CardInit(deck); // Initialize cards in the UI

        // Check that the number of cards in the UI matches the deck size
        Assert.AreEqual(2, uiManager.cards.childCount);
    }
}
*/