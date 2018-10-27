using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPanel : MonoBehaviour {
    [SerializeField] public List<Command> commands;
    [SerializeField] private GameObject commandPrefab;
    [SerializeField] private Transform commandRoot;

    public void AddCommand<T>() where T : Command
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.name = typeof(T).Name;
        go.AddComponent<T>();
        var command = go.GetComponent<T>();
        commands.Add(command);
        command.InitializeCommand(commands.Count - 1);
    }
    public void DeleteCommands(){
        var list = CommandSelectionManager.selectedCommands;
        if(list.Count == 0){
            DeleteAllCommands();
        }else{
            DeleteCommandsOnList(list);
        }
    }
    private void DeleteCommandsOnList(List<Command> cmdList){
        cmdList.Sort(delegate(Command x, Command y){
            return x.index.CompareTo(y.index);
        });
        for(int i = cmdList.Count - 1; i >= 0; i--)
            commands.RemoveAt(cmdList[i].index);
        ResetCommands();
    }
    private void DeleteAllCommands()
    {
        CommandSelectionManager.ResetSelectedCommands();
        commands = new List<Command>();
        for(int i = commandRoot.childCount-1; i >= 0; i--)
        {
            Destroy(commandRoot.GetChild(i).gameObject);
        }
    }
    public void Run()
    {
        //RUN
    }
    public void ResetCommands()
    {
        var commandsCopy = commands.GetRange(0, commands.Count);
        DeleteAllCommands();
        for(int i = 0; i < commandsCopy.Count; i++)
        {
            var t = commandsCopy[i].type;
            if (t == typeof(LookDownCommand))
                AddCommand<LookDownCommand>();
            else if (t == typeof(LookRightCommand))
                AddCommand<LookRightCommand>();
            else if (t == typeof(LookLeftCommand))
                AddCommand<LookLeftCommand>();
            else if (t == typeof(LookUpCommand))
                AddCommand<LookUpCommand>();
            else if (t == typeof(WalkCommand))
                AddCommand<WalkCommand>();
            else
                AddCommand<WaitCommand>();
        }
    }
    public void MoveCommands(List<int> selectedCommands, int target)
    {
        List<int> commandsToRemove = selectedCommands.GetRange(0, selectedCommands.Count); ;
        List<Command> commandsToAdd = new List<Command>();
        for(int i = commands.Count - 1; i >= 0; i--)
        {
            for(int j = 0; j < commandsToRemove.Count; j++)
            {
                if(commandsToRemove[j] == i)
                {
                    commandsToAdd.Add(commands[i]);
                    if (i <= target)
                        target--;
                    commandsToRemove.RemoveAt(j);
                    commands.RemoveAt(i);
                    break;
                }
            }
        }
        for(int i = 0; i < commandsToAdd.Count; i++)
        {
            commands.Insert(target, commandsToAdd[i]);
        }
        for(int i = 0; i < commands.Count; i++)
        {
            commands[i].index = i;
        }
        ResetCommands();
    }
}
