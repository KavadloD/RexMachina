using UnityEngine;
using UnityEngine.UI;

public class HandCardUI : MonoBehaviour
{
    public CardDisplay cardDisplay;
    private DinoCardData cardData;
    private DeckManager deckManager;

    public Image highlightBorder; // optional

    public void Setup(DinoCardData data, DeckManager manager)
    {
        cardData = data;
        deckManager = manager;

        if (cardDisplay != null)
            cardDisplay.SetupCard(data);

        SetSelected(false);

        // Dynamically hook up OnClick event here!
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("Card clicked: " + cardData?.cardName); // <- Add this line

        if (deckManager != null && cardData != null)
        {
            deckManager.OnPlayerCardClicked(cardData);
        }
    }


    public void SetSelected(bool isSelected)
    {
        if (highlightBorder != null)
            highlightBorder.enabled = isSelected;
    }
}
