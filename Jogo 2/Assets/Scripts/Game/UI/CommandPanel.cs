using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPanel : MonoBehaviour {
    [SerializeField] public List<Command> commands;

    [SerializeField] private GameObject commandPrefab;
    [SerializeField] private Transform commandRoot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //MoveCommands(selectedCommands, 0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddWalkCommand();
            AddWaitCommand();
            AddLookUpCommand();
            AddLookRightCommand();
            AddWalkCommand();
        }
    }
    public void AddWaitCommand()
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.AddComponent<WaitCommand>();
        var command = go.GetComponent<WaitCommand>();
        command.SetCommandName("ESPERAR");
        commands.Add(command);
        command.SetCommandIndex(commands.Count - 1);
    }
    public void AddWalkCommand()
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.AddComponent<WalkCommand>();
        var command = go.GetComponent<WalkCommand>();
        command.SetCommandName("ANDAR");
        commands.Add(command);
        command.SetCommandIndex(commands.Count - 1);
    }
    public void AddLookUpCommand()
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.AddComponent<LookUpCommand>();
        var command = go.GetComponent<LookUpCommand>();
        command.SetCommandName("OLHAR CIMA");
        commands.Add(command);
        command.SetCommandIndex(commands.Count - 1);
    }
    public void AddLookDownCommand()
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.AddComponent<LookDownCommand>();
        var command = go.GetComponent<LookDownCommand>();
        command.SetCommandName("OLHAR BAIXO");
        commands.Add(command);
        command.SetCommandIndex(commands.Count - 1);
    }
    public void AddLookRightCommand()
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.AddComponent<LookRightCommand>();
        var command = go.GetComponent<LookRightCommand>();
        command.SetCommandName("OLHAR DIREITA");
        commands.Add(command);
        command.SetCommandIndex(commands.Count - 1);
    }
    public void AddLookLeftCommand()
    {
        var go = Instantiate(commandPrefab, commandRoot);
        go.AddComponent<LookLeftCommand>();
        var command = go.GetComponent<LookLeftCommand>();
        command.SetCommandName("OLHAR ESQUERDA");
        commands.Add(command);
        command.SetCommandIndex(commands.Count - 1);
    }
    public void DeleteAllCommands()
    {
        //DELETE ALL
    }
    public void Run()
    {
        //RUN
    }
    public void MoveCommands(List<int> selectedCommands, int target)
    {
        List<int> commandsToRemove = selectedCommands.GetRange(0, selectedCommands.Count); ;
        List<Command> commandsToAdd = new List<Command>();
        for(int i = commands.Count - 1; i > 0; i--)
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
        for(int i = 0; i < selectedCommands.Count; i++)
        {
            commands.Insert(target, commandsToAdd[i]);
            //ajustar indice dos comandos
        }
    }
}
