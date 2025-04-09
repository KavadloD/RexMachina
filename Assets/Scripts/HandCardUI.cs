using UnityEngine;
using UnityEngine.UI;

public class HandCardUI : MonoBehaviour
{
    public CardDisplay cardDisplay;
    private DinoCardData cardData;
    private DeckManager deckManager;

    public Image highlightBorder;

    public void Setup(DinoCardData data, DeckManager manager)
    {
        cardData = data;
        deckManager = manager;

        if (cardDisplay == null)
        {
            Debug.LogError("[HandCardUI] cardDisplay is NULL on: " + gameObject.name);
        }
        else
        {
            cardDisplay.SetupCard(data);
        }

        SetSelected(false);

        var btn = GetComponent<Button>();
        if (btn == null)
        {
            Debug.LogError("[HandCardUI] Button component missing on: " + gameObject.name);
        }
        else
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        Debug.Log("Card clicked: " + (cardData != null ? cardData.cardName : "NULL card"));

        if (deckManager != null && cardData != null)
        {
            deckManager.OnPlayerCardClicked(cardData);
        }
        else
        {
            Debug.LogWarning("Card click failed: deckManager or cardData was null.");
        }
    }

    public void SetSelected(bool isSelected)
    {
        if (highlightBorder != null)
            highlightBorder.enabled = isSelected;
    }
}
