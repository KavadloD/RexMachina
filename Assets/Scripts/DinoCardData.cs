using UnityEngine;

[CreateAssetMenu(fileName = "New Dino Card", menuName = "Card/DinoCard")]
public class DinoCardData : ScriptableObject
{
    public string cardName;
    public int power;
    public string ability;
    public Sprite art;
}
