using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class BattleActionElementDisplay : MonoBehaviour
{
    [Tooltip("Which battle action element does this object represent?")]
    public string DisplaysFor;

    [BoxGroup("Components")]
    [SerializeField]
    [Tooltip("Set this image reference if you want the image to be retrieved from the sprite manager.")]
    protected Image image;

    /// <summary>
    /// Show iconography and information about the given action node.
    /// </summary>
    public virtual void Setup(IBattleActionElement element) 
    {
        if (image != null)
            image.sprite = SpriteManager.Instance.GetSpriteByName(element.name);
    }
}
