using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUIFrame : MonoBehaviour
{
    /// <summary>
    /// Gets the actor whose place in the turn order this frame represents.
    /// </summary>
    public Actor Actor { get; private set; }
    /// <summary>
    /// Gets the rect transform of this frame.
    /// </summary>
    public RectTransform RectTransform { get { return transform as RectTransform; } }

    [SerializeField][Tooltip("The image component that will hold the actor's portrait.")]
    private Image portrait;

    /// <summary>
    /// Sets the actor whose place in the turn order this frame
    /// represents.
    /// </summary>
    public void SetActor(Actor actor)
    {
        Actor = actor;
        portrait.sprite = SpriteManager.Instance.GetSpriteByName(actor.name);
        name = actor.name;
    }
}
