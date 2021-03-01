using UnityEngine;
using System.Collections;
using TMPro;

public class StatBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI defenseText = null;
    [SerializeField]
    private TextMeshProUGUI manaText = null;

    public void HandleOnStatValueChanged(Stat stat, int delta)
    {
        if (stat.Name == "Defense")
            defenseText.text = stat.Value.ToString();

        if (stat.Name == "Mana")
            manaText.text = stat.Value.ToString();
    }
}
