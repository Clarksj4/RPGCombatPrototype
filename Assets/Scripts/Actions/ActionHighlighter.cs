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
            Color colour = GetActionColour(action);

            foreach (Formation formation in BattleManager.Instance.Formations)
            {
                GridRenderer renderer = formation.GetComponent<GridRenderer>();

                for (int x = 0; x < formation.NCells.x; x++)
                {
                    for (int y = 0; y < formation.NCells.y; y++)
                    {
                        Vector2Int coordinate = new Vector2Int(x, y);
                        if (action.IsTargetValid(formation, coordinate))
                            renderer.SetCellColour(coordinate, colour);
                    }
                }
            }
        }

        private void HandleOnActionDeselected(BattleAction action)
        {
            foreach (Formation formation in BattleManager.Instance.Formations)
            {
                GridRenderer renderer = formation.GetComponent<GridRenderer>();
                renderer.SetAllCellColours(Color.white);
            }
        }

        private void HandleOnActionStarted(Actor actor, BattleAction action)
        {
            GridRenderer renderer = action.OriginFormation.GetComponent<GridRenderer>();
            renderer.SetAllCellColours(Color.white);
        }
    }
}