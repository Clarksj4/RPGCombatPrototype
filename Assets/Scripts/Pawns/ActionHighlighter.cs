using UnityEngine;

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

        private Color GetActionColour(BattleAction action)
        {
            Color colour = Color.cyan;

            if (action.Tags.HasFlag(ActionTag.Damage))
                colour = Color.red;

            else if (action.Tags.HasFlag(ActionTag.Heal))
                colour = Color.green;
            
            return colour;
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