using NUnit.Framework;

namespace TDD.Kata.Greeting.Tests
{
    using TDD.Kata.Greeting;
    
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void When_NameNotNull_Expect_CorrectMessage()
        {
            var message = Program.Greet("Bob");

            Assert.AreEqual(message, "Hello, Bob.");            
        }

        [Test]
        public void When_NameIsNull_Expect_DefautMessage()
        {
            var message = Program.Greet(null);

            Assert.AreEqual("Hello, my friend.", message);
        }

        [Test]
        public void When_NameIsUppercase_Expect_Shouting()
        {
            var message = Program.Greet("JERRY");

            Assert.AreEqual("HELLO JERRY!", message);
        }

        [Test]
        public void When_TwoNames_Expect_GreetBoth()
        {            
            var message = Program.Greet("Jill", "Jane" );

            Assert.AreEqual("Hello, Jill and Jane.", message);
        }

        [Test]
        public void When_ArbitraryNumberOfNames_Expect_GreetEverybody()
        {            
            var message = Program.Greet("Joe", "Bar", "Foo");

            Assert.AreEqual("Hello, Joe, Bar, and Foo.", message);            
        }

        [Test]
        public void When_ArbitraryNumberOfNamesMixedWithShouting_Expect_GreetingAndShouting()
        {            
            var message = Program.Greet(new string[] { "Amy", "BRIAN", "Charlotte" });

            Assert.AreEqual("Hello, Amy and Charlotte. AND HELLO BRIAN!", message);
        }
    }
}