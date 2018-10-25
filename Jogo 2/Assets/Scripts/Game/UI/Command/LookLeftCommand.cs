using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookLeftCommand : Command
{
    public override void Execute(Player player)
    {
        player.Look(Vector2.left);
    }
}
