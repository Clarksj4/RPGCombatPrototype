using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrderUI : MonoBehaviour
{
    [SerializeField]
    private HorizontalLayoutGroup slideyBit;
    [SerializeField]
    private TurnOrderUIFrame FramePrefab;

    private void Awake()
    {
        TurnManager.Instance.OnTurnEnd += HandleOnTurnEnd;
    }

    private IEnumerator Start()
    {
        print(TurnManager.Instance.OrderOfActors.Count());
        foreach (ITurnBased turnbased in TurnManager.Instance.OrderOfActors)
        {
            Actor actor = turnbased as Actor;
            AddFrame(actor);
            yield return null;
        }
    }

    private void HandleOnTurnEnd(ITurnBased entity)
    {

    }

    private void AddFrame(Actor actor, bool first = false)
    {
        // Create as first child
        TurnOrderUIFrame frame = Instantiate(FramePrefab, slideyBit.transform, false);
        if (first)
            frame.transform.SetAsFirstSibling();

        // Set actor
        frame.SetActor(actor);
    }
}
