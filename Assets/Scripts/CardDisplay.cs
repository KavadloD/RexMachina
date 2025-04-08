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

        nameText.text = data.cardName;
        nameTextShadow.text = data.cardName;

        abilityText.text = data.ability;
        abilityTextShadow.text = data.ability;

        if (dinoArt != null && data.art != null)
        {
            dinoArt.sprite = data.art;
        }
    }
}
