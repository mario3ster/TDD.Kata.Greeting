using System;
using System.Linq;

namespace TDD.Kata.Greeting
{
    public class Program
    {
        // static void Main(string[] args)
        // {
           
        // }

        public static string Greet(params string[] name)
        {            
            if(name is null) 
            {
                return "Hello, my friend.";
            }

            if (name.Length > 1)  return BuildGreetToManyPersons(name);
            
            if(name[0].All(c => char.IsUpper(c))) {
                return string.Format("HELLO {0}!", name[0]);
            }

            return string.Format("Hello, {0}.", name[0]);
        }

        private static string BuildGreetToManyPersons(string[] names)
        {
            if(names.Length > 2)
            {
                var namesWithUpperCases = names.Select(x => x.All(c => char.IsUpper(c)));

                if(namesWithUpperCases.Count() > 0)
                {
                    
                    return ProcessMoreThanTwoNamesWithUpperCase(names);
//                    return greeting;
                }

                

                return ProcessMoreThanTwoNames(names);
                
            }
                        
            return string.Format("Hello, {0}.", string.Join(" and ", names));
        }

        private static string ProcessMoreThanTwoNames(string[] names)
        {
            string[] namesWithOutLastOne = names.Take(names.Length - 1).ToArray();
            string lastName = names[names.Length - 1];

            var greeting = string.Format("Hello, {0}, and {1}.", string.Join(", ", namesWithOutLastOne), lastName);

            return greeting;
        }

          private static string ProcessMoreThanTwoNamesWithUpperCase(string[] names)
        {
            // string[] namesWithOutLastOne = names.Take(names.Length - 1).ToArray();
            // string lastName = names[names.Length - 1];

            // var greeting = string.Format("Hello, {0}, and {1}.", string.Join(", ", namesWithOutLastOne), lastName);

            // return greeting;
            return "";
        }
    }
}
