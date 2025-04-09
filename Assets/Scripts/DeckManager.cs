using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class DeckManager : MonoBehaviour
{
    //Display objects for current card for player and enemy
    public CardDisplay playerCardDisplay;
    public CardDisplay enemyCardDisplay;

    //Starting decks
    public DinoCardData baseCardTemplate;
    public int cardsPerDeck = 15;

    public List<DinoCardData> playerDeck = new List<DinoCardData>();
    public List<DinoCardData> enemyDeck = new List<DinoCardData>();

    /*Autoplay
    public bool autoPlay = false;
    public float delayBetweenRounds = 1.0f;
    */

    //Hand and card selection
    private List<DinoCardData> playerHand = new List<DinoCardData>();
    private List<DinoCardData> enemyHand = new List<DinoCardData>();

    private DinoCardData selectedPlayerCard;
    private DinoCardData selectedEnemyCard;
    public GameObject handCardPrefab;
    public Transform playerHandContainer;
    private List<HandCardUI> handCardUIs = new List<HandCardUI>();



    //Starts game
    void Start()
    {
        FillDeck(playerDeck);
        FillDeck(enemyDeck);

        ShuffleDeck(playerDeck);
        ShuffleDeck(enemyDeck);

        StartGame();
    }

    //Fills each deck. Basic loop that creates a new card, randomizes power, then adds it to the deck (power is only 1-13 for now)
    void FillDeck(List<DinoCardData> deck)
    {
        for (int i = 0; i < cardsPerDeck; i++)
        {
            DinoCardData newCard = ScriptableObject.CreateInstance<DinoCardData>();
            newCard.hideFlags = HideFlags.DontSave;
            newCard.cardName = baseCardTemplate.cardName;
            newCard.ability = baseCardTemplate.ability;
            newCard.art = baseCardTemplate.art;
            newCard.power = Random.Range(1, 14); // 1–13 like classic War
            deck.Add(newCard);
        }
    }

    //Goes through each index and randomly swaps it with another index. This shuffles the deck
    void ShuffleDeck(List<DinoCardData> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            DinoCardData temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    //Gameplay loop
    public void PlayRound()
    {
        Debug.Log("Play button clicked");
        Debug.Log("selectedPlayerCard: " + (selectedPlayerCard != null ? selectedPlayerCard.cardName : "null"));
        Debug.Log("playerHand.Count: " + playerHand.Count);
        Debug.Log("enemyHand.Count: " + enemyHand.Count);

        if (selectedPlayerCard == null || playerHand.Count == 0 || enemyHand.Count == 0)
        {
            Debug.Log("Cannot play round – make sure a card is selected and hands are valid.");
            return;
        }

        // Enemy randomly selects a card from their hand
        selectedEnemyCard = enemyHand[Random.Range(0, enemyHand.Count)];

        if (playerDeck.Count == 0 || enemyDeck.Count == 0)
        {
            Debug.Log("Game Over!");
            return;
        }
        // Show both played cards
        playerCardDisplay.SetupCard(selectedPlayerCard);
        enemyCardDisplay.SetupCard(selectedEnemyCard);

        // Determine round winner (tie goes to player)
        if (selectedPlayerCard.power >= selectedEnemyCard.power)
        {
            Debug.Log("You win the round!");
            playerDeck.Add(selectedPlayerCard);
            playerDeck.Add(selectedEnemyCard);
        }
        else
        {
            Debug.Log("Enemy wins the round!");
            enemyDeck.Add(selectedEnemyCard);
            enemyDeck.Add(selectedPlayerCard);
        }

        // Remove played cards from hand
        playerHand.Remove(selectedPlayerCard);
        enemyHand.Remove(selectedEnemyCard);

        // Reset selections
        selectedPlayerCard = null;
        selectedEnemyCard = null;

        // Refill hands
        DrawCardToHand(playerDeck, playerHand);
        DrawCardToHand(enemyDeck, enemyHand);

        // Refresh hand UI (if implemented)
        DisplayPlayerHand();
    }

    /*
    public void StartAutoPlay()
    {
        if (!autoPlay)
        {
            autoPlay = true;
            StartCoroutine(AutoPlayRoutine());
        }
    }


    IEnumerator AutoPlayRoutine()
    {
        while (playerDeck.Count > 0 && enemyDeck.Count > 0)
        {
            PlayRound();
            yield return new WaitForSeconds(delayBetweenRounds);
        }

        autoPlay = false;
        Debug.Log("Auto-play ended.");
    }
    */

    public void StartGame()
    {
        playerHand.Clear();
        enemyHand.Clear();

        for (int i = 0; i < 3; i++)
        {
            DrawCardToHand(playerDeck, playerHand);
            DrawCardToHand(enemyDeck, enemyHand);
        }

        DisplayPlayerHand();
    }

    void DrawCardToHand(List<DinoCardData> fromDeck, List<DinoCardData> toHand)
    {
        if (fromDeck.Count == 0) return;

        DinoCardData card = fromDeck[0];
        fromDeck.RemoveAt(0);
        toHand.Add(card);
    }

    public void OnPlayerCardClicked(DinoCardData cardData)
    {
        selectedPlayerCard = cardData;
        Debug.Log("Player selected: " + cardData.cardName + " with power " + cardData.power);
        HighlightSelectedCard(cardData);
    }

    public void DisplayPlayerHand()
    {
        if (handCardPrefab == null || playerHandContainer == null)
        {
            Debug.LogWarning("HandCardPrefab or PlayerHandContainer not assigned.");
            return;
        }

        foreach (Transform child in playerHandContainer)
        {
            Destroy(child.gameObject);
        }

        handCardUIs.Clear();

        foreach (DinoCardData card in playerHand)
        {
            GameObject cardGO = Instantiate(handCardPrefab, playerHandContainer);
            HandCardUI ui = cardGO.GetComponentInChildren<HandCardUI>();
            ui.Setup(card, this);
            handCardUIs.Add(ui);
        }
    }

    public void HighlightSelectedCard(DinoCardData selected)
    {
        foreach (HandCardUI cardUI in handCardUIs)
        {
            bool isSelected = cardUI.cardDisplay.data == selected;
            cardUI.SetSelected(isSelected);
        }
    }

    private void ShowRoundResult(string message)
    {
        Debug.Log(message);
        // Add UI banner, animation, sound, etc. later
    }

}
