using System;

using NUnit.Framework;

using SharpArch.Core;

namespace Tests.SharpArch.Core
{
    public partial class DesignByContractTests
    {
        private const string InvariantMessage = "Invariant failed.";


        [Test]
        public void InvariantThrowsCorrectly()
        {
            Assert.That(() => Check.Invariant(false),
                        Throws.TypeOf<InvariantException>());
        }


        [Test]
        public void InvariantWithLambdaAndMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Invariant(-1, i => i > 0, InvariantMessage),
                        Throws.TypeOf<InvariantException>()
                            .With.Message.ContainsSubstring(InvariantMessage));
        }


        [Test]
        public void InvariantWithLambdaMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Invariant(-1, i => i > 0, InvariantMessage, inner),
                        Throws.TypeOf<InvariantException>()
                            .With.Message.ContainsSubstring(InvariantMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void InvariantWithLambdaThrowsCorrectly()
        {
            Assert.That(() => Check.Invariant(-1, i => i > 0),
                        Throws.TypeOf<InvariantException>());
        }


        [Test]
        public void InvariantWithMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Invariant(false, InvariantMessage, inner),
                        Throws.TypeOf<InvariantException>()
                            .With.Message.ContainsSubstring(InvariantMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void InvariantWithMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Invariant(false, InvariantMessage),
                        Throws.TypeOf<InvariantException>()
                            .With.Message.ContainsSubstring(InvariantMessage));
        }
    }
}