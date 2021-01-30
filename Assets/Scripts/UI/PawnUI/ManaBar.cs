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
        pawn.OnInitialized += HandleOnPawnInitialized;
        pawn.OnManaChanged += HandleOnPawnManaChanged;
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
            pips[i].gameObject.SetActive(i < pawn.MaxMana);

            // Fill it if pawn has that much mana
            if (i < pawn.Mana)
                pips[i].Fill();
            else
                pips[i].Empty();
        }
    }

    private void HandleOnPawnManaChanged(Pawn pawn, int delta)
    {
        print($"Pawn Mana Changed to: {pawn.Mana}");
        RefreshPips();
    }
}
