using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MonogameRotoplanet.Systems.Input;

public static class InputManager
{
    private static KeyboardState _currentKeyState;
    private static KeyboardState _previousKeyState;
    private static Dictionary<GameState, Dictionary<InputAction, HashSet<Keys>>> _keyBindings;

    public static void Init(InputConfig config)
    {
        _keyBindings = config.KeyBindings;
    }

    public static void Update()
    {
        _previousKeyState = _currentKeyState;
        _currentKeyState = Keyboard.GetState();
    }

    public static bool IsKeyDown(GameState state, InputAction action)
    {
        // No keys
        if (!_keyBindings.TryGetValue(state, out Dictionary<InputAction, HashSet<Keys>> dict) || !dict.TryGetValue(action, out HashSet<Keys> keys))
            return false;

        // Check if one of the keys is pressed. 
        foreach (Keys key in keys)
        {
            if (_currentKeyState.IsKeyDown(key))
                return true;
        }
        return false; 
    }

    public static bool IsKeyPressed(GameState state, InputAction action)
    {
        // No keys
        if (!_keyBindings.TryGetValue(state, out Dictionary<InputAction, HashSet<Keys>> dict) || !dict.TryGetValue(action, out HashSet<Keys> keys))
            return false;

        // Check if one of the keys is pressed. 
        foreach (Keys key in keys)
        {
            if (_currentKeyState.IsKeyDown(key) && !_previousKeyState.IsKeyDown(key))
                return true;
        }
        return false; 
    }
    
    public static bool IsKeyReleased(GameState state, InputAction action)
    {
        // No keys
        if (!_keyBindings.TryGetValue(state, out Dictionary<InputAction, HashSet<Keys>> dict) || !dict.TryGetValue(action, out HashSet<Keys> keys))
            return false;

        // Check if one of the keys is pressed. 
        foreach (Keys key in keys)
        {
            if (!_currentKeyState.IsKeyDown(key) && _previousKeyState.IsKeyDown(key))
                return true;
        }
        return false; 
    }
}