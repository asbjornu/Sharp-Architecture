using System;

using NUnit.Framework;

using SharpArch.Core;

namespace Tests.SharpArch.Core
{
    public partial class DesignByContractTests
    {
        private const string PostconditionMessage =
            "convertedValue was expected to be within 0-100, but was ";

        [Test]
        public void CanEnforcePostcondition()
        {
            Assert.Throws<PostconditionException>(() => ConvertToPercentage(2m));
        }


        [Test]
        public void PostconditionThrowsCorrectly()
        {
            Assert.That(() => Check.Ensure(false),
                        Throws.TypeOf<PostconditionException>());
        }


        [Test]
        public void PostconditionWithLambdaAndMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Ensure(-1, i => i > 0, PostconditionMessage),
                        Throws.TypeOf<PostconditionException>()
                            .With.Message.ContainsSubstring(PostconditionMessage));
        }


        [Test]
        public void PostconditionWithLambdaMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Ensure(-1, i => i > 0, PostconditionMessage, inner),
                        Throws.TypeOf<PostconditionException>()
                            .With.Message.ContainsSubstring(PostconditionMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void PostconditionWithLambdaThrowsCorrectly()
        {
            Assert.That(() => Check.Ensure(-1, i => i > 0),
                        Throws.TypeOf<PostconditionException>());
        }


        [Test]
        public void PostconditionWithMessageAndInnerThrowsCorrectly()
        {
            FormatException inner = new FormatException();
            Assert.That(() => Check.Ensure(false, PostconditionMessage, inner),
                        Throws.TypeOf<PostconditionException>()
                            .With.Message.ContainsSubstring(PostconditionMessage)
                            .And.Property("InnerException").EqualTo(inner));
        }


        [Test]
        public void PostconditionWithMessageThrowsCorrectly()
        {
            Assert.That(() => Check.Ensure(false, PostconditionMessage),
                        Throws.TypeOf<PostconditionException>()
                            .With.Message.ContainsSubstring(PostconditionMessage));
        }
    }
}