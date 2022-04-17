//
// ConsoleKeyHandler.cs
//
// This is a re-write of tonerdo/readline which
// is a library based on gnu readline.
//
// sadly it looks to be discontinued so I decided to reimplement it.
//
// Copyright (C) 2021 Yaman Alhalabi
//


using System;
using System.Text;
using System.Collections.Generic;

namespace Fluint.Implementation.SDK
{
    public class ConsoleKeyHandler
    {
        public ConsoleKeyHandler(List<string> history)
        {
            _history = history;
            _historyIndex = history.Count;

            FillReactionDictionary();

        }

        private void FillReactionDictionary()
        {
            _reactions = new Dictionary<string, Action>();
            _reactions["LeftArrow"] = MoveLeft;
            _reactions["RightArrow"] = MoveRight;
            _reactions["Home"] = MoveToStart;
            _reactions["End"] = MoveToEnd;
            _reactions["ControlA"] = MoveToStart;
            _reactions["ControlB"] = MoveLeft;
            _reactions["RightArrow"] = MoveRight;
            _reactions["ControlF"] = MoveRight;
            _reactions["ControlE"] = MoveToEnd;
            _reactions["Backspace"] = Backspace;
            _reactions["Delete"] = Delete;
            _reactions["ControlD"] = Delete;
            _reactions["ControlH"] = Backspace;
            _reactions["ControlL"] = ClearLine;
            _reactions["Escape"] = ClearLine;
            _reactions["UpArrow"] = PreviousHistory;
            _reactions["ControlP"] = PreviousHistory;
            _reactions["DownArrow"] = NextHistory;
            _reactions["ControlN"] = NextHistory;
            _reactions["ControlU"] = () =>
            {
                while (!IsStartOfLine())
                    Backspace();
            };
            _reactions["ControlK"] = () =>
            {
                var pos = _cursorPosition;
                MoveToEnd();
                while (_cursorPosition > pos)
                    Backspace();
            };
            _reactions["ControlW"] = () =>
            {
                while (!IsStartOfLine() && _buffer[_cursorPosition - 1] != ' ')
                    Backspace();
            };
            _reactions["ControlT"] = TransposeCharacters;

        }

        public string GetText() => _buffer.ToString();

        private int _cursorPosition = 0;
        private int _cursorLimit = 0;

        private StringBuilder _buffer = new StringBuilder();

        private List<string> _history;
        private int _historyIndex = 0;

        private ConsoleKeyInfo _current;
        private Dictionary<string, Action> _reactions;

        private string[] _suggestions;
        private int _suggestionStart = 0;
        private int _suggestionIndex = 0;

        private bool IsStartOfLine() => _cursorPosition == 0;
        private bool IsEndOfLine() => _cursorPosition == _cursorLimit;
        private bool IsStartOfBuffer() => Console.CursorLeft == 0;
        private bool IsEndOfBuffer() => Console.CursorLeft == Console.BufferWidth - 1;

        public void Handle(ConsoleKeyInfo keyInfo)
        {
            _current = keyInfo;

            if (_suggestions != null && _current.Key != ConsoleKey.Tab)
                ResetSuggestions();

            if (_reactions.TryGetValue(RepresentCurrent(), out var action))
            {
                action.Invoke();
            }
            else
            {
                AppendChar(_current.KeyChar);
            }
        }

        private void MoveLeft()
        {
            if (IsStartOfLine())
                return;

            if (IsStartOfBuffer())
            {
                Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }

            _cursorPosition--;
        }

        private void MoveRight()
        {
            if (IsEndOfLine())
                return;

            if (IsEndOfBuffer())
            {
                Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
            else
            {
                Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
            }

            _cursorPosition++;
        }

        private void MoveToStart()
        {
            while (!IsStartOfLine())
            {
                MoveLeft();
            }
        }

        private void MoveToEnd()
        {
            while (!IsEndOfLine())
            {
                MoveRight();
            }
        }

        private void ClearLine()
        {
            MoveToEnd();

            while (!IsStartOfLine())
            {
                Backspace();
            }
        }

        private void AppendChar(char c)
        {
            if (!IsEndOfLine())
            {
                var left = Console.CursorLeft;
                var top = Console.CursorTop;

                var str = _buffer.ToString().Substring(_cursorPosition);
                _buffer.Insert(_cursorPosition, c);

                Console.Write(c.ToString() + str);
                Console.SetCursorPosition(left, top);

                MoveRight();
            }
            else
            {
                _buffer.Append(c);
                Console.Write(c.ToString());
                _cursorPosition++;
            }
            _cursorLimit++;
        }

        private void AppendCurrent() => AppendChar(_current.KeyChar);

        private void AppendString(string str)
        {
            foreach (var character in str)
            {
                AppendChar(character);
            }
        }

        private void ReplaceLine(string str)
        {
            ClearLine();
            AppendString(str);
        }

        private void Backspace()
        {
            if (IsStartOfLine()) return;

            MoveLeft();

            var index = _cursorPosition;
            _buffer.Remove(index, 1);

            var replacement = _buffer.ToString().Substring(index);

            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            Console.Write($"{replacement} ");
            Console.SetCursorPosition(left, top);
            _cursorLimit--;
        }

        private string RepresentCurrent()
        {
            if (_current.Modifiers != ConsoleModifiers.Control && _current.Modifiers != ConsoleModifiers.Shift)
            {
                return _current.Key.ToString();
            }

            return _current.Modifiers.ToString() + _current.Key.ToString();
        }

        private void Delete()
        {
            if (IsEndOfLine())
                return;

            var index = _cursorPosition;

            _buffer.Remove(index, 1);
            string replacement = _buffer.ToString().Substring(index);

            var left = Console.CursorLeft;
            var top = Console.CursorTop;

            Console.Write(replacement);
            Console.SetCursorPosition(left, top);

            _cursorLimit--;
        }

        private void TransposeCharacters()
        {
            bool AlmostAtEnd() => (_cursorLimit - _cursorPosition) == 1;
            int ConditionalIncrement(Func<bool> expression, int index) => expression() ? index + 1 : index;
            int ConditionalDecrement(Func<bool> expression, int index) => expression() ? index - 1 : index;

            if (IsStartOfLine()) return;

            var firstIndex = ConditionalDecrement(IsEndOfLine, _cursorPosition - 1);
            var secondIndex = ConditionalDecrement(IsEndOfLine, _cursorPosition);

            var secondChar = _buffer[secondIndex];
            _buffer[secondIndex] = _buffer[firstIndex];
            _buffer[firstIndex] = secondChar;

            var left = ConditionalIncrement(AlmostAtEnd, Console.CursorLeft);
            var cursorPosition = ConditionalIncrement(AlmostAtEnd, _cursorPosition);

            ReplaceLine(_buffer.ToString());

            Console.SetCursorPosition(left, Console.CursorTop);
            _cursorPosition = cursorPosition;

            MoveRight();
        }

        private void InitialSuggestion()
        {
            while (_cursorPosition > _suggestionStart)
                Backspace();

            _suggestionIndex = 0;

            AppendString(_suggestions[_suggestionIndex]);
        }

        private void NextSuggestion()
        {
            while (_cursorPosition > _suggestionStart)
                Backspace();

            if (_suggestionIndex == _suggestions.Length)
                _suggestionIndex = 0;

            AppendString(_suggestions[_suggestionIndex]);
        }

        private void PreviousSuggestion()
        {
            while (_cursorPosition > _suggestionStart)
                Backspace();

            _suggestionIndex--;

            if (_suggestionIndex < 0)
                _suggestionIndex = _suggestions.Length - 1;

            AppendString(_suggestions[_suggestionIndex]);
        }

        private void ResetSuggestions()
        {
            _suggestions = null;
            _suggestionIndex = 0;
        }

        private void NextHistory()
        {
            if (_historyIndex < _history.Count)
            {
                _historyIndex++;
                if (_historyIndex == _history.Count)
                {
                    ClearLine();
                }
                else
                {
                    ReplaceLine(_history[_historyIndex]);
                }
            }
        }

        private void PreviousHistory()
        {
            if (_historyIndex > 0)
            {
                _historyIndex--;
                ReplaceLine(_history[_historyIndex]);
            }
        }

    }
}
