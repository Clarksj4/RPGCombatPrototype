using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class DamageNodeDisplay : BattleActionElementDisplay
{
    [BoxGroup("Components")]
    [SerializeField]
    private TextMeshProUGUI amountText = null;

    public override void Setup(IBattleActionElement element) 
    {
        DoDamageNode damageNode = element as DoDamageNode;
        amountText.text = damageNode.BaseDamage.ToString();
    }
}
