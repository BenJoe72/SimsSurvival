using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GameState", menuName = "Configurations/GameState")]
public class GameState : ScriptableObject
{
    public string Name;
    public Sprite Icon;
}
