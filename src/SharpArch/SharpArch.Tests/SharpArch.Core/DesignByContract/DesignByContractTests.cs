using System;
using System.Diagnostics;

using NUnit.Framework;

using SharpArch.Core;

namespace Tests.SharpArch.Core
{
    [TestFixture]
    public partial class DesignByContractTests
    {
        private static string ConvertToPercentage(decimal fractionalPercentage)
        {
            Check.Require(fractionalPercentage > 0, PreconditionMessage);

            decimal convertedValue = fractionalPercentage * 100;

            // Yes, I could have enforced this outcome in the precondition, but then you
            // wouldn't have seen the Check.Ensure in action, now would you?
            Check.Ensure(convertedValue >= 0 && convertedValue <= 100,
                         PostconditionMessage + convertedValue);

            return Math.Round(convertedValue) + "%";
        }


        [Test]
        public void CanGetPastPreconditionAndPostCondition()
        {
            Assert.AreEqual("20%", ConvertToPercentage(.2m));
        }


        [Test]
        public void SettingUseAssertionsToTrueShouldNotThrow()
        {
            Check.UseAssertions = true;
            // Clear all trace listeners to avoid the ugly message box popping up
            Trace.Listeners.Clear();
            Assert.That(() => Check.Require(false), Throws.Nothing);
            Check.UseAssertions = false;
        }
    }
}