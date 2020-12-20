using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Assets.Scripts.GridBased
{
    [RequireComponent(typeof(MonoGrid), typeof(Collider2D))]
    public class GridInput : MonoBehaviour
    {
        [Tooltip("Occurs when the associated grid receives an input.")]
        public UnityEvent<MonoGrid, Vector2Int> OnInput;

        private MonoGrid grid;

        private void Awake()
        {
            grid = GetComponent<MonoGrid>();
        }

        private void OnMouseDown()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (grid.WorldPositionToCoordinate(mousePosition, out var coordinate))
                OnInput?.Invoke(grid, coordinate);
        }
    }
}