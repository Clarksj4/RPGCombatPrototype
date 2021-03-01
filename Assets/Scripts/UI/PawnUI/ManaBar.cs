using System;
using UnityEngine;

public class ManaBar : MonoBehaviour
{
    [SerializeField]
    private ManaPip[] pips;
    private Pawn pawn;

    private void Awake()
    {
        pawn = GetComponentInParent<Pawn>();
        pawn.Stats["Mana"].OnValueChanged += HandleOnPawnManaChanged;
    }

    private void HandleOnPawnInitialized(Pawn pawn)
    {
        RefreshPips();
    }

    private void RefreshPips()
    {
        for (int i = 0; i < pips.Length; i++)
        {
            // Show it if pawn has capacity
            pips[i].gameObject.SetActive(i < pawn.Stats["Mana"].Max);

            // Fill it if pawn has that much mana
            if (i < pawn.Stats["Mana"].Value)
                pips[i].Fill();
            else
                pips[i].Empty();
        }
    }

    private void HandleOnPawnManaChanged(Stat stat, int delta)
    {
        RefreshPips();
    }
}
