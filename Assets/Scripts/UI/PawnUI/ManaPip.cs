using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaPip : MonoBehaviour
{
    public Color ManaColour = Color.white;
    public Color EmptyColour = Color.white;

    [SerializeField]
    private Image baseImage = null;
    [SerializeField]
    private Image highlightImage = null;

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
