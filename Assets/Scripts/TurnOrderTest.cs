using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TurnOrderTest : MonoBehaviour
{
    private TurnOrder turnOrder;

    private void Start()
    {
        turnOrder = new TurnOrder();
        turnOrder.Add(new Actor() { Name = "Stephen", Priority = 78 });
        turnOrder.Add(new Actor() { Name = "Richard", Priority = 72 });
        turnOrder.Add(new Actor() { Name = "Jan", Priority = 68 });
        turnOrder.Add(new Actor() { Name = "Alec", Priority = 92 });
        turnOrder.Add(new Actor() { Name = "Xavier", Priority = 42 });
        turnOrder.Add(new Actor() { Name = "Julia", Priority = 12 });
        turnOrder.Add(new Actor() { Name = "Rebecca", Priority = 39 });
    }

    public void Next()
    {
        PrintCurrentActor();
        PrintTurnOrder();

        bool isMore = turnOrder.MoveNext();

        if (!isMore)
            print("Round end!");
    }

    private void PrintTurnOrder()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Current turn order is: ");
        foreach (Actor actor in turnOrder)
            builder.Append(actor.Name + ", ");
        builder.Remove(builder.Length - 2, 2);
        print(builder.ToString());
    }

    private void PrintCurrentActor()
    {
        ITurnBased current = turnOrder.Current;
        if (current != null)
        {
            Actor actor = current as Actor;
            print("Current actor is: " + actor.Name);
        }

        else
        {
            print("Current actor is null");
        }
    }
}
