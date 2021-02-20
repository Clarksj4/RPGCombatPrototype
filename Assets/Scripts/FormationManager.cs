using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class FormationManager : Singleton<FormationManager>
{
    public IEnumerable<Formation> Formations 
    { 
        get 
        { 
            // Lazy formation retrieval
            if (formations == null )
                formations = GameObject.FindObjectsOfType<Formation>();
            return formations; 
        } 
    }

    private Formation[] formations;

    public Formation GetFormation(Cell cell)
    {
        return Formations.FirstOrDefault(f => f.Contains(cell));
    }
}
