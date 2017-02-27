using System;
using System.Collections.Generic;
using System.Text;

namespace Turbo.Events.Message
{
    public class TemplatedString
    {
        private readonly object[] _args;

        private readonly Action<int> _malformedMessageError;

        public TemplatedString(string template, params object[] args)
        {
            _args = args;
            Template = template;
            _malformedMessageError = Errors.GetError<int>(col => $"String is malformed: {Template.Show(col)}.");
        }

        public string Template { get; }

        public override string ToString()
        {
            var arguments = new Dictionary<int, object>();
            for (int i = 0; i < _args.Length; i++)
            {
                arguments.Add(i, _args[i]);
            }

            var result = new StringBuilder();
            var argumentNames = new Dictionary<string, int>();
            StringBuilder argumentName = null;

            var col = 0;
            foreach (var c in Template)
            {
                col++;

                if (c == '}' && argumentName == null)
                {
                    _malformedMessageError(col);
                }

                if (c == '{' && argumentName == null)
                {
                    argumentName = new StringBuilder();
                    continue;
                }

                if (argumentName != null)
                {
                    if (c == '{')
                    {
                        _malformedMessageError(col);
                    }

                    if (c == '}')
                    {
                        if (argumentName.Length == 0)
                        {
                            _malformedMessageError(col);
                        }

                        int number;
                        var name = argumentName.ToString();
                        if (!argumentNames.TryGetValue(name, out number))
                        {
                            number = argumentNames.Count + 1;
                            argumentNames.Add(name, number);
                        }

                        result.Append(arguments[number - 1]);
                        argumentName = null;
                        continue;
                    }

                    argumentName.Append(c);
                    continue;
                }

                result.Append(c);
            }

            return result.ToString();
        }
    }
}