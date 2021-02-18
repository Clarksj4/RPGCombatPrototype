using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(LayoutGroup), typeof(RectTransform))]
public class SlotFiller : MonoBehaviour
{
    [SerializeField]
    private Transform[] slots;
    [SerializeField]
    private bool hideEmpty;

    private void Awake()
    {
        Show();   
    }

    private void Show()
    {
        foreach (Transform slot in slots)
        {
            bool show = slot.childCount > 0 || !hideEmpty;
            slot.gameObject.SetActive(show);
        }
            
    }

    public void Add(Transform child)
    {
        if (child != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                Transform slot = slots[i];
                if (slot.childCount == 0)
                {
                    child.SetParent(slot, false);
                    slot.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    public void Remove(Transform child)
    {
        if (child != null)
        {
            child.parent.gameObject.SetActive(false);
            child.SetParent(null);
        }
    }
}
