using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigCommand : Command {

    public override IEnumerator Execute(Player player)
    {
        yield return player.Dig();
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "CAVAR", typeof(DigCommand));
    }
}
