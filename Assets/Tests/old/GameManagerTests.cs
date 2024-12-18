/*
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class GameManagerTests
{
    private GameManager gameManager;

    // Setup before each test
    [SetUp]
    public void Setup()
    {
        var go = new GameObject(); // Create a new GameObject to attach the GameManager
        gameManager = go.AddComponent<GameManager>(); // Add the GameManager component
    }

    // Teardown after each test
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(gameManager.gameObject); // Destroy the GameObject to clean up
    }

    // Test to check if the Singleton pattern is enforced
    [Test]
    public void Singleton_IsEnforced()
    {
        var instance1 = gameManager; // Reference the instance created in Setup
        var instance2 = new GameObject().AddComponent<GameManager>(); // Create a new GameObject with another GameManager

        // Ensure both instances refer to the same singleton instance
        Assert.AreSame(instance1, GameManager.instance);
        
        Object.Destroy(instance2.gameObject); // Clean up
    }

    // Test to check if JSON data is read and tasks are populated correctly
    [Test]
    public void ReadJson_PopulatesTaskListCorrectly()
    {
        gameManager.jsonText = new TextAsset("{\"difficulty\":\"Hard\",\"taskListe\":[{\"taskName\":\"Test Task\",\"taskDifficulty\":\"Medium\"}]}");
        gameManager.ReadJson(); // Read JSON and populate task list

        // Check that the difficulty and task list are populated correctly
        Assert.AreEqual("Hard", gameManager.myTaskListe.difficulty);
        Assert.AreEqual(1, gameManager.myTaskListe.taskListe.Length);
        Assert.AreEqual("Test Task", gameManager.myTaskListe.taskListe[0].taskName);
    }

    // Test to check if unanimity is handled correctly
    [Test]
    public void Unanimite_ReturnsTrueForUnanimity()
    {
        gameManager.playerValue = new string[] { "1", "1", "1" }; // All players have the same value
        bool result = gameManager.Unanimite(out string decision);

        // Check that unanimity is true and the decision is "1"
        Assert.IsTrue(result);
        Assert.AreEqual("1", decision);
    }

    // Test to check if the average calculation ignores invalid inputs
    [Test]
    public void Moyenne_CalculatesAverageIgnoringInvalidInputs()
    {
        gameManager.playerValue = new string[] { "2", "4", "Cafée?", "?" }; // Invalid inputs "Cafée?" and "?"
        gameManager.playerListe = new string[4]; // Simulate four players
        
        // Calculate the average and check that invalid inputs are ignored
        string result = gameManager.Moyenne();
        Assert.AreEqual("3", result); // (2+4)/2 = 3
    }

    // Test to check if the NextPlayer method works as expected
    [Test]
    public void NextPlayer_AdvancesToNextPlayer()
    {
        gameManager.playerListe = new string[] { "Player1", "Player2" }; // Simulate two players
        gameManager.currentPlayer = 0; // Start with Player1
        gameManager.NextPlayer(null); // Call the NextPlayer method

        // Check that the current player has advanced to Player2
        Assert.AreEqual(1, gameManager.currentPlayer);
    }
}
*/