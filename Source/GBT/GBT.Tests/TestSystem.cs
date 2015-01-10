using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SoftwareApproach.TestingExtensions;

namespace GBT.Tests
{
	[TestClass]
	public class TestSystem
	{
		[TestMethod]
		[Description("Basic test to ensure that the unit test system is working")]
		public void Can_Do_Unit_Tests()
		{
			// Arrange:
			int a = 4;
			int b = 5;

			// Act:
			var result = a + b;

			// Assert:
			result.ShouldEqual(9);
		}

		[TestMethod]
		[Description("Test to ensure that expected test exceptions are being correctly caught")]
		[ExpectedException(typeof(DivideByZeroException))]
		[DebuggerStepThroughAttribute()]
		public void Can_Catch_Expected_Exceptions()
		{
			// Arrange:
			int a = 4;
			int b = 0;

			// Act: generate a divide-by-0 exception
			var result = a / b;

			// Assert: shouldn't be able to reach here
			throw new InvalidOperationException();
		}
	}
}
