using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaPip : MonoBehaviour
{
    public Color ManaColour;
    public Color EmptyColour;

    [SerializeField]
    private Image glowImage;
    [SerializeField]
    private Image darkenImage;
    [SerializeField]
    private Image baseImage;
    [SerializeField]
    private Image highlightImage;

    public void Fill()
    {
        baseImage.color = ManaColour;

        float a = highlightImage.color.a;
        highlightImage.color = new Color(ManaColour.r, ManaColour.g, ManaColour.b, a);
    }

    public void Empty()
    {
        baseImage.color = EmptyColour;

        float a = highlightImage.color.a;
        highlightImage.color = new Color(EmptyColour.r, EmptyColour.g, EmptyColour.b, a);
    }
}
