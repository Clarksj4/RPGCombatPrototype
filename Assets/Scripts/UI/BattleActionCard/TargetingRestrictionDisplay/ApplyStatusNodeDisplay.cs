
public class ApplyStatusNodeDisplay : BattleActionElementDisplay
{
    public override void Setup(IBattleActionElement element)
    {
        ApplyStatusNode node = element as ApplyStatusNode;
        image.sprite = SpriteManager.Instance.GetSpriteByName(node.Status.Name);
    }
}
