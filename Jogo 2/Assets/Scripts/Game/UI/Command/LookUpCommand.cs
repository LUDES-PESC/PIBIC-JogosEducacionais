using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpCommand : Command
{
    public override IEnumerator Execute(Player player)
    {
        yield return player.Look(Vector2Int.up);
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "OLHAR CIMA", typeof(LookUpCommand));
    }
}
