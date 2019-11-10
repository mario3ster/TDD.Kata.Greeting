namespace TDD.Kata.Greeting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Program
    {
        internal interface IGreetable
        {
            string Greet();
        }

        internal class NullNameGreeting : IGreetable
        {
            public string Greet()
            {
                return "Hello, my friend.";
            }
        }

        internal class SingleNameGreeting : IGreetable
        {
            string name;

            public SingleNameGreeting(string _name)
            {
                name = _name;
            }

            public virtual string Greet()
            {
                if (name.All(c => char.IsUpper(c)))
                {
                    return string.Format("HELLO {0}!", name);
                }

                return string.Format("Hello, {0}.", name);
            }
        }

        internal class MultipleNamesGreeting : IGreetable
        {
            protected string[] names;

            public MultipleNamesGreeting(string[] _names)
            {
                names = _names;
            }

            protected MultipleNamesGreeting() {}

            public virtual string Greet()
            {
                string greeting = String.Empty;
                string[] upperCaseNames = names.Where(name => name.All(c => Char.IsUpper(c))).ToArray();
                string[] regularNames = names.Except(upperCaseNames).ToArray();

                if (regularNames.Length == 2)
                {
                    greeting = string.Format("Hello, {0} and {1}.", regularNames[0], regularNames[1]);
                }
                else if (regularNames.Length > 2)
                {
                    string lastOne = regularNames.Last();
                    string[] allWithoutLastOne = regularNames.Take(regularNames.Length - 1).ToArray();
                    string allWithoutLastOneAsString = String.Join(", ", allWithoutLastOne);

                    greeting = String.Format("Hello, {0}, and {1}.", allWithoutLastOneAsString, lastOne);
                }

                var greetingBuilder = new StringBuilder(greeting);

                foreach (var uCaseName in upperCaseNames)
                {
                    greetingBuilder.Append(string.Format(" AND HELLO {0}!", uCaseName));
                }

                return greetingBuilder.ToString();
            }
        }

        internal class MultipleCommaSeparatedNames : MultipleNamesGreeting, IGreetable
        {
            public MultipleCommaSeparatedNames(string[] _names)
            {
                var parsedNames = new List<string>();

                foreach(var name in _names)
                {
                    if(name.Contains(","))
                    {
                        string[] subnames = name.Split(",");

                        foreach (var subname in subnames)
                        {
                            parsedNames.Add(subname.Trim());
                        }

                        continue;
                    }   

                    parsedNames.Add(name);
                }

                base.names = parsedNames.ToArray();
            }

            public override string Greet()
            {
                return base.Greet();
            }
        }
        public static string Greet(params string[] names)
        {
            if (names is null)
            {
                return (new NullNameGreeting()).Greet();
            }

            if (names.Length == 1)
            {
                return (new SingleNameGreeting(names[0])).Greet();
            }

            return (new MultipleCommaSeparatedNames(names)).Greet();
        }

        static void Main(string[] args)
        {
        }
    }
}