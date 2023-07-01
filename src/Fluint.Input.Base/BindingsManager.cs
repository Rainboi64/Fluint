//
// BindingsManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

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
            new Binding { Tag = "SAVE_CONFIG", MainCombination = new[] { Key.S, Key.LeftControl } },
            new Binding { Tag = "MOVE_CAMERA", MainCombination = new[] { Key.LeftAlt } }
        };

        private readonly Dictionary<string, List<Binding>> _bindingsDictionary;
        private readonly IConfigurationManager _configurationManager;

        private string _current = "default";

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

        public IInputManager InputManager
        {
            get;
            set;
        }

        public InputState GetState(string bindName)
        {
            var binding = GetBinding(bindName);
            return GetState(binding);
        }

        public InputState GetState(Binding binding)
        {
            return binding.MainCombination.Aggregate(
                InputState.Press, (current, key) => {
                    if (current == InputState.Release)
                    {
                        return InputState.Release;
                    }

                    if (InputManager.WasKeyPressed(key) && InputManager.IsKeyPressed(key))
                    {
                        return InputState.Repeat;
                    }

                    return InputManager.State(key);
                });
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
    }
}