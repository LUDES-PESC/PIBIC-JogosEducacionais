using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpCommand : Command
{
    public override void Execute(Player player)
    {
        player.Look(Vector2.up);
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "OLHAR CIMA", typeof(LookUpCommand));
    }
}
