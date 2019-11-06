using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(Greet(null));
            System.Console.WriteLine(Greet("Bob"));
            System.Console.WriteLine(Greet("MAYA"));
            System.Console.WriteLine(Greet("Bob", "Mary"));
            System.Console.WriteLine(Greet("Joe", "Bar", "Foo"));
            System.Console.WriteLine(Greet("Joe", "BAR", "Foo"));            
        }

        public static string Greet (params string[] names)
        {
            var greetingEngine = new List<Greeting>() 
            { 
                new Greeting() 
                {
                    Predicate = () => names is null, 
                    Template = "Hello, my friend.",
                    GetTemplateParams = () => new string [] { "" }
                },

                new Greeting() 
                {
                    Predicate = () => names.Length == 1 && names[0].All(c => char.IsUpper(c)), 
                    Template = "HELLO {0}!",
                    GetTemplateParams = () => new string[] { names[0] }
                },

                new Greeting() 
                {
                    Predicate = () => names.Length == 1, 
                    Template = "Hello, {0}.",
                    GetTemplateParams = () => new string[] { names[0] }
                },
                
                new Greeting() 
                {
                    Predicate = () => names.Length == 2, 
                    Template = "Hello, {0} and {1}.",
                    GetTemplateParams = () => new string[] { names[0], names[1] }
                },

                new Greeting() 
                {
                    Predicate = () => 
                        names.Length > 2 && 
                        names.Any(name => 
                                        name.All(symbol => char.IsUpper(symbol))), 
                    Template = "Hello, {0} and {1}. HELLO {2}!",                    
                    GetTemplateParams = () => GetParamsForMultiplePersonsGreetingWithShouting(names)
                },

                new Greeting() 
                {
                    Predicate = () => names.Length > 2, 
                    Template = "Hello, {0}, and {1}.",                    
                    GetTemplateParams = () => new string[] 
                    { 
                        string.Join(", ", names.Take(names.Length - 1)), 
                        names.Last()
                    }
                },

               
            };

            foreach (var item in greetingEngine)
            {
                if(item.Predicate())
                {
                    var greetingMessage = string.Format(item.Template, item.GetTemplateParams());

                    return greetingMessage;
                }
            }

            throw new ArgumentException("Not implemented greeting for that input");
        }

        private static string[] GetParamsForMultiplePersonsGreetingWithShouting (string[] names)
        {            
            string[] uppercaseNames = names.Where(name => name.All(symbol => char.IsUpper(symbol))).ToArray();
            var regularNames = names.Except(uppercaseNames);

            return new string[] 
                    { 
                        string.Join(", ", regularNames.Take(names.Length - 1)), 
                        regularNames.Last(),                        
                        uppercaseNames.Last(),                        
                    };
        }
    }

    
    internal class Greeting
    {
        internal Func<bool> Predicate { get; set; }
        internal string Template { get; set; }

        internal Func<string[]> GetTemplateParams { get; set; }
    }
}
