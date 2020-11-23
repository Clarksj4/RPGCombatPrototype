using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Actions
{
    public class ActionHighlighter : MonoBehaviour
    {
        private static readonly Dictionary<ActionTag, Color> TAG_TO_COLOUR = new Dictionary<ActionTag, Color>()
        {
            { ActionTag.Damage, Color.red },
            { ActionTag.Heal, Color.green },
            { ActionTag.Movement, Color.cyan }
        };

        private void Awake()
        {
            ActionManager.Instance.OnActionSelected += HandleOnActionSelected;
            ActionManager.Instance.OnActionDeselected += HandleOnActionDeselected;
            ActionManager.Instance.OnActionStarted += HandleOnActionStarted;
        }

        private Color GetActionColour(BattleAction action)
        {
            // Get the first tag that has a colour associated with it
            ActionTag tag = action.Tags.FirstOrDefault(TAG_TO_COLOUR.ContainsKey);

            // Get the colour associated with tag
            Color color = TAG_TO_COLOUR[tag];
            return color;
        }

        private void HandleOnActionSelected(BattleAction action)
        {
            GridRenderer renderer = action.OriginFormation.GetComponent<GridRenderer>();
            Color colour = GetActionColour(action);

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