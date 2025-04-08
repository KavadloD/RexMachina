using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI abilityText;
    public Image dinoArt; // ← reference to the art slot


    //Shadows
    public TextMeshProUGUI powerTextShadow;
    public TextMeshProUGUI nameTextShadow;
    public TextMeshProUGUI abilityTextShadow;

    public void SetupCard(DinoCardData data)
    {
        powerText.text = data.power.ToString();
        powerTextShadow.text = data.power.ToString();

        // Scale font size based on power (1 = 0.2, 13 = 1.0)
        float scaledFontSize = Mathf.Lerp(0.2f, 1.0f, (data.power - 1) / 12f);
        powerText.fontSize = scaledFontSize;
        powerTextShadow.fontSize = scaledFontSize;

        nameText.text = data.cardName;
        nameTextShadow.text = data.cardName;

        abilityText.text = data.ability;
        abilityTextShadow.text = data.ability;

        if (dinoArt != null && data.art != null)
        {
            dinoArt.sprite = data.art;
        }
    }

    private float CalculateFontSize(int power)
    {
        // Power 1 = 0.2 font size, Power 13 = 1.0
        return Mathf.Lerp(0.2f, 1.0f, (power - 1) / 12f);
    }
}
