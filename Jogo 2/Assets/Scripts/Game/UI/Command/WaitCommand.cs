using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCommand : Command
{
    public override void Execute(Player player)
    {
        player.Wait();
    }
}
