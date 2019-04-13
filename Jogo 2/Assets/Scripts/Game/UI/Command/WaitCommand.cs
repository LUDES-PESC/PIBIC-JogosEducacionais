using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : Command
{
    public override IEnumerator Execute(Player player)
    {
        yield return player.Wait();
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "ESPERAR", typeof(WaitCommand));
    }
}
