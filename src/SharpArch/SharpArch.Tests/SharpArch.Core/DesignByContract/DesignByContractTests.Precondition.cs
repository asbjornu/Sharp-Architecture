using System;

using NUnit.Framework;

using SharpArch.Core;

namespace Tests.SharpArch.Core
{
    public partial class DesignByContractTests
    {
        private const string PreconditionMessage = "fractionalPercentage must be > 0";


        [Test]
        public void CanEnforcePrecondition()
        {
            Assert.Throws<PreconditionException>(() => ConvertToPercentage(-.2m));
        }


        [Test]
        public void PreconditionThrowsCorrectly()
        {
            Assert.That(() => Check.Require(false),
                        Throws.TypeOf<PreconditionException>());
        }


        [Test]
        public void PreconditionWithLambdaAndMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Require(-1, i => i > 0, PreconditionMessage),
                        Throws.TypeOf<PreconditionException>()
                            .With.Message.ContainsSubstring(PreconditionMessage));
        }


        [Test]
        public void PreconditionWithLambdaMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Require(-1, i => i > 0, PreconditionMessage, inner),
                        Throws.TypeOf<PreconditionException>()
                            .With.Message.ContainsSubstring(PreconditionMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void PreconditionWithLambdaThrowsCorrectly()
        {
            Assert.That(() => Check.Require(-1, i => i > 0),
                        Throws.TypeOf<PreconditionException>());
        }


        [Test]
        public void PreconditionWithMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Require(false, PreconditionMessage, inner),
                        Throws.TypeOf<PreconditionException>()
                            .With.Message.ContainsSubstring(PreconditionMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void PreconditionWithMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Require(false, PreconditionMessage),
                        Throws.TypeOf<PreconditionException>()
                            .With.Message.ContainsSubstring(PreconditionMessage));
        }
    }
}