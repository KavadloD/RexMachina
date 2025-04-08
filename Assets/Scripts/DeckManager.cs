using System.Collections.Generic;
using UnityEngine;

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

//Starts game
    void Start()
    {
        FillDeck(playerDeck);
        FillDeck(enemyDeck);

        ShuffleDeck(playerDeck);
        ShuffleDeck(enemyDeck);

        PlayRound();
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
        if (playerDeck.Count == 0 || enemyDeck.Count == 0)
        {
            Debug.Log("Game Over!");
            return;
        }

        DinoCardData playerCard = playerDeck[0];
        DinoCardData enemyCard = enemyDeck[0];

        playerCardDisplay.SetupCard(playerCard);
        enemyCardDisplay.SetupCard(enemyCard);

        if (playerCard.power > enemyCard.power)
        {
            Debug.Log("You win the round!");
            playerDeck.Add(playerCard);
            playerDeck.Add(enemyCard);
        }
        else if (enemyCard.power > playerCard.power)
        {
            Debug.Log("You lose the round!");
            enemyDeck.Add(enemyCard);
            enemyDeck.Add(playerCard);
        }
        else
        {
            Debug.Log("Tie! (War logic could go here)");
            // You can expand this later!
        }

        playerDeck.RemoveAt(0);
        enemyDeck.RemoveAt(0);
    }
}
