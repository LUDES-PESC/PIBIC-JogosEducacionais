using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectionManager : MonoBehaviour {
    public static List<Command> selectedCommands = new List<Command>();
    public static bool isMoving;

    public static void SelectCommand(Command command, bool append = false)
    {
        if (!append)
        {
            if (!selectedCommands.Contains(command))
            {
                UnselectAll();
                command.Select();
                selectedCommands.Add(command);
            }
            else
            {
                UnselectAll();
            }
        }
        else
        {
            if (selectedCommands.Contains(command))
            {
                command.Unselect();
                selectedCommands.Remove(command);
            }
            else
            {
                selectedCommands.Add(command);
                command.Select();
            }
        }
    }
    private static void UnselectAll()
    {
        foreach(Command c in selectedCommands)
        {
            c.Unselect();
        }
        selectedCommands = new List<Command>();
    }
    public static void BeginSelectionMove(Command cmd)
    {
        if (selectedCommands.Contains(cmd))
        {
            isMoving = true;
        }
    }
    public static void EndSelectionMove(Command cmd)
    {
        if (!isMoving)
            return;
        if (!selectedCommands.Contains(cmd))
        {
            MoveCommands(cmd.index);
        }
        isMoving = false;
    }
    private static void MoveCommands(int index)
    {
        var commands = new List<int>();
        for(int i = 0; i < selectedCommands.Count; i++)
        {
            commands.Add(selectedCommands[i].index);
        }
        FindObjectOfType<CommandPanel>().MoveCommands(commands, index);
    }
    public static void ResetSelectedCommands()
    {
        selectedCommands = new List<Command>();
    }
}
