/*
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CardScriptableGameObjectTests
{
    private CardScriptableGameObject cardScriptableObject;

    // Setup before each test
    [SetUp]
    public void Setup()
    {
        cardScriptableObject = ScriptableObject.CreateInstance<CardScriptableGameObject>(); // Create an instance of CardScriptableGameObject
    }

    // Teardown after each test
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(cardScriptableObject); // Clean up by destroying the ScriptableObject
    }

    // Test to check if CardScriptableGameObject initializes correctly
    [Test]
    public void CardScriptableGameObject_InitializesCorrectly()
    {
        Assert.NotNull(cardScriptableObject); // Check that the CardScriptableGameObject is not null
        Assert.AreEqual(cardScriptableObject.img, null); // Check that the image is not set by default
    }

    // Test to check if the card's image can be assigned
    [Test]
    public void CardScriptableGameObject_CanAssignImage()
    {
        var sprite = new Sprite(); // Create a new sprite
        cardScriptableObject.img = sprite; // Assign the sprite to the image property

        // Check that the image has been assigned correctly
        Assert.AreEqual(cardScriptableObject.img, sprite);
    }
}
*/