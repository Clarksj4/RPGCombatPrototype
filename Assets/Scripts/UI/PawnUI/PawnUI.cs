using UnityEngine;

public class PawnUI : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar = null;

    public void HandleOnHealthChanged(Stat health, int delta)
    {

        float fill = (float)health.Value / health.Max;
        healthBar.SetHealth(fill);
    }
}
