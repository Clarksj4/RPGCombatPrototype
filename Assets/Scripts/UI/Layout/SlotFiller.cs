using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(LayoutGroup), typeof(RectTransform))]
public class SlotFiller : MonoBehaviour
{
    [SerializeField]
    private Transform[] slots;

    public void Add(Transform child)
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                child.SetParent(slot);
                slot.gameObject.SetActive(true);
            }
        }
    }

    public void Remove(Transform child)
    {
        child.parent.gameObject.SetActive(false);
        child.SetParent(null);
    }
}
