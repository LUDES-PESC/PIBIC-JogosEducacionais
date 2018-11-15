using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDownCommand : Command
{
    public override void Execute(Player player)
    {
        player.Look(Vector2Int.down);
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "OLHAR BAIXO", typeof(LookDownCommand));
    }
}
