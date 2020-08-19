using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private Actor selectedActor;

    public void Move()
    {
        // TODO: pass in target
        selectedActor.Move();
    }

    public void Attack()
    {
        // TODO: pass in target
        selectedActor.Attack();
    }

    public void EndTurn()
    {
        selectedActor.EndTurn();
    }
}
