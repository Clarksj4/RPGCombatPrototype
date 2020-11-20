using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Assets.Scripts.GridBased
{
    [RequireComponent(typeof(Grid), typeof(Collider2D))]
    public class GridInput : MonoBehaviour
    {
        [Tooltip("Occurs when the associated grid receives an input.")]
        public UnityEvent<Grid, Vector2Int> OnInput;

        private Grid grid;

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        private void OnMouseDown()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (grid.WorldPositionToCoordinate(mousePosition, out var coordinate))
                OnInput?.Invoke(grid, coordinate);
        }
    }
}