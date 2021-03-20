using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Input;
using System;
using System.Collections.Generic;

namespace Fluint.Implementation.Input
{
    public class BindingsManager : IBindingsManager
    {
        public static readonly IEnumerable<Binding> Default = new List<Binding>()
        {
            new Binding() { Tag = "that one key", MainCombination = new[] { Key.LeftControl, Key.R }, SecondaryCombination = new[] { Key.RightControl, Key.R } }
        };

        private string _current = "default";

        private IInputManager _inputManager;

        private readonly Dictionary<string, List<Binding>> _bindingsDictionary;
        private readonly IConfigurationManager _configurationManager;

        // private Binding _lastReleased;

        public event Action<InputState, Binding> BindingStateUpdated;

        public void Load(IInputManager inputManager)
        {
            _inputManager = inputManager;
            _inputManager.Keyboard += InputManager_Keyboard;
        }

        private void InputManager_Keyboard(InputState arg1, Key arg2)
        {
            var current = _bindingsDictionary[_current];
            var length = current.Count;
            for (var i = 0; i < length; i++)
            {
                var currentBinding = current[i];
                var state = GetState(currentBinding);

                // this code is used to prevent false negatives in bindings, but it doesn't work.
                //if (state == InputState.Release)
                //{
                //    if (currentBinding == _lastReleased)
                //    {
                //        _lastReleased = null;
                //        continue;
                //    }

                //    _lastReleased = currentBinding;
                //}

                BindingStateUpdated?.Invoke(state, currentBinding);
            }
        }

        public BindingsManager(ModulePacket packet)
        {
            _configurationManager = packet.GetSingleton<IConfigurationManager>();

            if (_configurationManager.Contains<InputBindingsConfiguration>())
            {
                _bindingsDictionary = _configurationManager.Get<InputBindingsConfiguration>().Bindings;
            }
            else 
            {
                _bindingsDictionary = new Dictionary<string, List<Binding>>
                {
                    { "default", (List<Binding>)Default }
                };
            }
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
                // looping through all of the bindinds and checking if any key is pressed.
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
            if (_bindingsDictionary.ContainsKey(tag) && tag != _current)
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
    }
}
