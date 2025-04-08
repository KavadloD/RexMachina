using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public CardDisplay cardDisplay;
    public DinoCardData testCard;

    void Start()
    {
        cardDisplay.SetupCard(testCard);
    }
}
