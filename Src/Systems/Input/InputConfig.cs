using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace MonogameRotoplanet.Systems.Input;

public enum GameState
{
    Gameplay,
    Menu,
    GameOver,
    Global
}

public enum InputAction
{
    Jump,
    MoveLeft,
    MoveRight, 
    MoveUp, 
    MoveDown, 
    Pause, 
    Select,
    NewGame
}

public class InputConfig
{
    public Dictionary<GameState, Dictionary<InputAction, HashSet<Keys>>> KeyBindings = new();

    public InputConfig()
    {
        string configPath = "Configs/InputConfig.json";
        string json = File.ReadAllText(configPath);
        var raw = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(json);

        foreach (var statePair in raw)
        {
            if (!Enum.TryParse(statePair.Key, out GameState state))
                continue;

            var actionDict = new Dictionary<InputAction, HashSet<Keys>>();

            foreach (var actionPair in statePair.Value)
            {
                if (!Enum.TryParse(actionPair.Key, out InputAction action))
                    continue;

                var keys = new HashSet<Keys>();

                foreach (var keyString in actionPair.Value)
                {
                    if (Enum.TryParse(keyString, out Keys key))
                    {
                        keys.Add(key);
                    }
                }

                actionDict[action] = keys;
            }

            KeyBindings[state] = actionDict;
        }
    }
}
