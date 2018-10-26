using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCommand : Command
{
    public override void Execute(Player player)
    {
        player.Walk();
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "ANDAR", typeof(WalkCommand));
    }
}
