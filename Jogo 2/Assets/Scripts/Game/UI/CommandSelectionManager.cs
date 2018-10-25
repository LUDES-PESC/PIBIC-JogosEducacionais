using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSelectionManager : MonoBehaviour {
    public static List<Command> selectedCommands = new List<Command>();

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
}
