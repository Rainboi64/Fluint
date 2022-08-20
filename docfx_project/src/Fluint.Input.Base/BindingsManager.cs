//
// BindingsManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Input;

namespace Fluint.Input.Base
{
    public class BindingsManager : IBindingsManager
    {
        public readonly static IEnumerable<Binding> Default = new[] {
            new Binding() { Tag = "SAVE_CONFIG", MainCombination = new[] { Key.S, Key.LeftControl } }
        };

        private readonly Dictionary<string, List<Binding>> _bindingsDictionary;
        private readonly IConfigurationManager _configurationManager;

        private string _current = "default";

        private IInputManager _inputManager;

        public BindingsManager(ModulePacket packet)
        {
            _configurationManager = packet.GetSingleton<IConfigurationManager>();

            if (_configurationManager.Contains<InputBindingsConfiguration>())
            {
                _bindingsDictionary = _configurationManager.Get<InputBindingsConfiguration>().Bindings;
            }
            else
            {
                _bindingsDictionary = new Dictionary<string, List<Binding>> {
                    { "default", Default.ToList() }
                };
            }
        }

        public event Action<InputState, Binding> BindingStateUpdated;

        public void Load(IInputManager inputManager)
        {
            _inputManager = inputManager;

            // TODO: This needs fixing asap
            // I've went ahead and disabled this shit
            // we'll have to see how to fix
            //  |
            //  |
            //  V

            //_inputManager.Keyboard += InputManager_Keyboard;
        }

        public InputState GetState(string bindName)
        {
            var binding = GetBinding(bindName);
            return GetState(binding);
        }

        public InputState GetState(Binding binding)
        {
            // set defualt state to press.
            var state = InputState.Press;

            // loop through each key in combination
            var combinationLength = binding.MainCombination.Length;
            for (var i = 0; i < combinationLength; i++)
            {
                // get state of key.
                var currentState = _inputManager.State(binding.MainCombination[i]);

                // if state isn't the current state.
                if (currentState == state)
                {
                    continue;
                }

                // only change if the new one is release.
                if (currentState == InputState.Release)
                {
                    state = InputState.Release;
                }

                // current input implementation doesn't support it for multiple keybinds.
                if (state == InputState.Press && currentState == InputState.Repeat)
                {
                    state = InputState.Repeat;
                }
            }

            if (state == InputState.Press)
            {
                return InputState.Press;
            }

            if (binding.SecondaryCombination is null)
            {
                return InputState.Release;
            }

            // loop through each key in combination
            combinationLength = binding.SecondaryCombination.Length;
            for (var i = 0; i < combinationLength; i++)
            {
                // get state of key.
                var currentState = _inputManager.State(binding.SecondaryCombination[i]);

                // if state isn't the current state.
                if (currentState != state)
                {
                    // only change if the new one is release.
                    if (currentState == InputState.Release)
                    {
                        state = InputState.Release;
                    }

                    // current input implementation doesn't support it for multiple keybinds.
                    if (state == InputState.Press && currentState == InputState.Repeat)
                    {
                        state = InputState.Repeat;
                    }
                }
            }

            return state;
        }

        public Binding GetBinding(string bindName)
        {
            // Damn it linq.
            // am going all out, not even a foreach.
            var bindings = _bindingsDictionary[_current];
            var count = bindings.Count;
            for (var i = 0; i < count; i++)
            {
                // looping through all of the bindings and checking if any key is pressed.
                var current = bindings[i];
                if (current.Tag == bindName)
                {
                    return current;
                }
            }

            // not gonna except
            return null;
        }

        public IEnumerable<Binding> GetBindings()
        {
            return _bindingsDictionary[_current];
        }

        public void LoadCollection(string collectionTag)
        {
            _current = collectionTag;
        }

        public IEnumerable<Binding> GetCollection(string tag)
        {
            return _configurationManager.Get<InputBindingsConfiguration>().Bindings[tag];
        }

        public void SaveCurrentCollection(string tag)
        {
            if (_bindingsDictionary.ContainsKey(tag))
            {
                _bindingsDictionary[tag] = _bindingsDictionary[_current];
            }
            else
            {
                _bindingsDictionary.Add(tag, _bindingsDictionary[_current]);
            }

            _configurationManager.Add(new InputBindingsConfiguration(_bindingsDictionary));
        }

        public void LoadBinding(Binding binding)
        {
            _bindingsDictionary[_current].Add(binding);
        }

        public void LoadBindings(IEnumerable<Binding> bindings)
        {
            _bindingsDictionary[_current].AddRange(bindings);
        }

        public InputState Get(Binding binding)
        {
            return GetState(binding);
        }

        public InputState Get(string binding)
        {
            return GetState(GetBinding(binding));
        }

        private void InputManager_Keyboard(InputState arg1, Key arg2)
        {
            var current = _bindingsDictionary[_current];
            var length = current.Count;
            for (var i = 0; i < length; i++)
            {
                var currentBinding = current[i];
                var state = GetState(currentBinding);

                BindingStateUpdated?.Invoke(state, currentBinding);
            }
        }
    }
}