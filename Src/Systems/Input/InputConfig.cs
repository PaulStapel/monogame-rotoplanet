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
        string json = File.ReadAllText("InputConfig");
        KeyBindings = JsonSerializer.Deserialize<Dictionary<GameState, Dictionary<InputAction, HashSet<Keys>>>>(json);
        // KeyBindings[GameState.Gameplay] = new Dictionary<InputAction, HashSet<Keys>>
        // {
        //     { InputAction.MoveLeft, new HashSet<Keys> {Keys.A, Keys.Left} },
        //     { InputAction.MoveRight, new HashSet<Keys> {Keys.D, Keys.Right} },

        //     { InputAction.Jump, new HashSet<Keys> {Keys.Space} },
        // };

        // KeyBindings[GameState.Global] = new Dictionary<InputAction, HashSet<Keys>>
        // {
        //     { InputAction.Pause, new HashSet<Keys> {Keys.P} }
        // };

        // KeyBindings[GameState.GameOver] = new Dictionary<InputAction, HashSet<Keys>>
        // {
        //     { InputAction.NewGame, new HashSet<Keys> {Keys.Enter, Keys.Space} }
        // };

        // KeyBindings[GameState.Menu] = new Dictionary<InputAction, HashSet<Keys>>
        // {
        //     { InputAction.MoveUp, new HashSet<Keys> {Keys.W, Keys.Up} },
        //     { InputAction.MoveDown, new HashSet<Keys> {Keys.S, Keys.Down} },

        //     { InputAction.Select, new HashSet<Keys> {Keys.Enter, Keys.Space} },
        // };
    }
}