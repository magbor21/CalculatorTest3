using System;
using Xunit;
using Calculator1;


namespace Calculator1.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Constructor() //Tests the constructor
        {
            double total = 10;
            string lastOperation = "3*5";

            Calculator testCalc = null;
            testCalc = new Calculator(total, lastOperation);

            Assert.NotNull(testCalc);
            Assert.Equal(testCalc.TotalText, total.ToString());
        }

        [Theory]            // Tests the Operator finder
        [InlineData("1,102", -1)]
        [InlineData("-5,1+2", 0)]
        [InlineData("1*1+2", 1)]
        [InlineData("1,+22", 2)]
        [InlineData("1,1/2", 3)]
        [InlineData("1,10+2", 4)]
        [InlineData("1,102+", 5)]
        public void FindOperatorTest(string mathsProblem, int position)
        {
            Calculator testCalc2 = new Calculator();
            testCalc2.FindOperator(mathsProblem);
            Assert.Equal(position, testCalc2.OperatorPosition);

        }

        [Theory]    // Tests the parser
        [InlineData("1,102+5", 5, 1.102, 5)]
        [InlineData("25/0", 2, 25, 0)]
        [InlineData("+26", 0, 0, 26)]
        public void ParsenumbersTest(string inputstring, int position, double left, double right)
        {
            Calculator testcalc3 = new Calculator();
            double[] result = testcalc3.ParseNumbers(inputstring, position);

            Assert.Equal(result[0], left);
            Assert.Equal(result[1], right);

        }

        [Fact]
        public void MathOpsTest() //Tests the mathematical operations with 2 and 6 numbers
        {
            Calculator testcalc4 = new Calculator();

            double[] twoNumbers = { 45, 3 };
            double[] sixnumbers = { 1, 2, 3, 4, 5, 6 };

            double result = 0;

            result = testcalc4.Addition(twoNumbers);
            Assert.Equal(result, 48);
            result = testcalc4.Addition(sixnumbers);
            Assert.Equal(result, 21);

            result = testcalc4.Subtraction(twoNumbers);
            Assert.Equal(result, 42);
            result = testcalc4.Subtraction(sixnumbers);
            Assert.Equal(result, -19);

            result = testcalc4.Multiplication(twoNumbers);
            Assert.Equal(result, 135);
            result = testcalc4.Multiplication(sixnumbers);
            Assert.Equal(result, 720);

            result = testcalc4.Division(twoNumbers);
            Assert.Equal(result, 15);
            result = testcalc4.Division(sixnumbers);
            Assert.Equal(result, 0.3125);

        }
        
        
    }
}
