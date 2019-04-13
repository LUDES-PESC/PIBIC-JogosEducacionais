﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command {

    public override IEnumerator Execute(Player player)
    {
        yield return player.Shoot();
    }
    public override void InitializeCommand(int index, string name = null, System.Type type = null)
    {
        base.InitializeCommand(index, "SHOOT", typeof(ShootCommand));
    }
}
