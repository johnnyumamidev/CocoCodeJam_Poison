using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI uiText;
    void OnEnable()
    {
        Events.OnSuccessfulBrew += SuccessText;
        Events.OnFailedBrew += FailText;
    }
    void OnDisable()
    {
        Events.OnSuccessfulBrew -= SuccessText;
        Events.OnFailedBrew -= FailText;
    }

    void SuccessText(PotionSO _potionSO)
    {
        uiText.text = "Successfully Brewed: " + _potionSO.prefab.name;
    }

    void FailText()
    {
        uiText.text = "Failed Brew";
    }
}
