using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class FormationManager : MonoSingleton<FormationManager>
{
    public IEnumerable<Formation> Formations { get { return formations; } }

    private Formation[] formations;

    protected override void Awake()
    {
        base.Awake();

        formations = FindObjectsOfType<Formation>();
    }

    public Formation GetFormation(Cell cell)
    {
        return formations.FirstOrDefault(f => f.Contains(cell));
    }
}
