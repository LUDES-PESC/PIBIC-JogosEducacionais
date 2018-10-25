using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCommand : Command
{
    public override void Execute(Player player)
    {
        player.Walk();
    }
}
