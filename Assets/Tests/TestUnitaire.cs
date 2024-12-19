using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestUnitaire
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestUnitaireSimplePasses()
    {
        GameObject tester = new GameObject();
        GameManager gameManagerTester = tester.AddComponent<GameManager>();

        string[] players = { "Player1", "Player2", "Player3" };
        string[] values = { "10", "25", "5" };

        gameManagerTester.Extrem(players, values, out string max, out string min);
        Assert.AreEqual("Player3", min);
        Assert.AreEqual("Player2", max);
        Assert.AreEqual("10", gameManagerTester.Median(players, values));

        //Assert.IsTrue(gameManagerTester.Paire(4));
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestUnitaireWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
