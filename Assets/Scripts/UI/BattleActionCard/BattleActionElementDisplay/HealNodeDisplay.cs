using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class HealNodeDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField]
    private TextMeshProUGUI amountText = null;

    public override void Setup(IBattleActionElement element)
    {
        HealNode healNode = element as HealNode;
        amountText.text = healNode.Amount.ToString();
    }
}
