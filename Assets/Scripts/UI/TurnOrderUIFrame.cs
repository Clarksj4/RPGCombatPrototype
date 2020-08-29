using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUIFrame : MonoBehaviour
{
    public Image Image { get; private set; }
    public Actor Actor { get; private set; }
    public RectTransform RectTransform { get { return transform as RectTransform; } }

    private void Awake()
    {
        Image = GetComponent<Image>();
    }

    public void SetActor(Actor actor)
    {
        Actor = actor;
        Image.sprite = SpriteManager.Instance.GetSpriteByName(actor.name);
        name = actor.name;
    }
}
