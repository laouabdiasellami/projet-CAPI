using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Scriptable Objects/Card")]
public class CardScriptableGameObject : ScriptableObject
{
    public Sprite img;
    public string value;
}
