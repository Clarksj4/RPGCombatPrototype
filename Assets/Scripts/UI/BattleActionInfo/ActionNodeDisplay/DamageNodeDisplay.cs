using UnityEngine;
using System.Collections;
using TMPro;

public class DamageNodeDisplay : ActionNodeDisplay
{
    [Header("Components")]
    private TextMeshProUGUI text;

    public override void Setup(ActionNode node) 
    {
        DoDamageNode damageNode = node as DoDamageNode;
        text.text = damageNode.BaseDamage.ToString();
    }
}
