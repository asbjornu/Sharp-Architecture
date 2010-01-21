using System;

using NUnit.Framework;

using SharpArch.Core;

using AssertionException = SharpArch.Core.AssertionException;

namespace Tests.SharpArch.Core
{
    public partial class DesignByContractTests
    {
        private const string AssertionMessage = "Assert failed.";


        [Test]
        public void AssertionThrowsCorrectly()
        {
            Assert.That(() => Check.Assert(false),
                        Throws.TypeOf<AssertionException>());
        }


        [Test]
        public void AssertionWithLambdaAndMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Assert(-1, i => i > 0, AssertionMessage),
                        Throws.TypeOf<AssertionException>()
                            .With.Message.ContainsSubstring(AssertionMessage));
        }


        [Test]
        public void AssertionWithLambdaMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Assert(-1, i => i > 0, AssertionMessage, inner),
                        Throws.TypeOf<AssertionException>()
                            .With.Message.ContainsSubstring(AssertionMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void AssertionWithLambdaThrowsCorrectly()
        {
            Assert.That(() => Check.Assert(-1, i => i > 0),
                        Throws.TypeOf<AssertionException>());
        }


        [Test]
        public void AssertionWithMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Assert(false, AssertionMessage, inner),
                        Throws.TypeOf<AssertionException>()
                            .With.Message.ContainsSubstring(AssertionMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void AssertionWithMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Assert(false, AssertionMessage),
                        Throws.TypeOf<AssertionException>()
                            .With.Message.ContainsSubstring(AssertionMessage));
        }
    }
}