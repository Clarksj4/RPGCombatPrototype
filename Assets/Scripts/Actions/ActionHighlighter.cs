using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace Assets.Scripts.Actions
{
    public class ActionHighlighter : MonoBehaviour
    {
        private void Awake()
        {
            ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
            ActionManager.Instance.OnActionDeselected += HandleOnActionDeselected;
            ActionManager.Instance.OnActionStarted += HandleOnActionStarted;
        }

        private void HandleOnActionSelected(BattleAction action)
        {
            GridRenderer renderer = action.OriginFormation.GetComponent<GridRenderer>();
            Color colour = action.Tags.Contains(ActionTag.Damage) ? Color.red : Color.cyan;
            for (int x = 0; x < action.OriginFormation.NCells.x; x++)
            {
                for (int y = 0; y < action.OriginFormation.NCells.y; y++)
                {
                    Vector2Int coordinate = new Vector2Int(x, y);
                    if (action.IsTargetValid(action.OriginFormation, coordinate))
                        renderer.SetCellColour(coordinate, colour);
                        
                }
            }
        }

        private void HandleOnActionDeselected(BattleAction action)
        {
            GridRenderer renderer = action.OriginFormation.GetComponent<GridRenderer>();
            renderer.SetAllCellColours(Color.white);
        }

        private void HandleOnActionStarted(Actor actor, BattleAction action)
        {
            GridRenderer renderer = action.OriginFormation.GetComponent<GridRenderer>();
            renderer.SetAllCellColours(Color.white);
        }
    }
}