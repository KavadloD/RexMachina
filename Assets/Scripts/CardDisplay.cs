using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI abilityText;
    public Image dinoArt; // ← reference to the art slot

    public void SetupCard(DinoCardData data)
    {
        powerText.text = data.power.ToString();
        nameText.text = data.cardName;
        abilityText.text = data.ability;

        if (dinoArt != null && data.art != null)
        {
            dinoArt.sprite = data.art;
        }
    }
}
