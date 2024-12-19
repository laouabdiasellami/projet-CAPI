using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GamePreset", menuName = "Scriptable Objects/GamePreset")]
public class GamePresetScriptableGameObject : ScriptableObject
{
    public int nbPlayer;
    public string jsonPath;
    public string gamerule;
    public List<string> playersName = new List<string>();
}
