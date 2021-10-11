using System;

namespace Calculator1
{

    public class Calculator
    {
        public double Total;
        public string LastOperation;
        public string TotalText;
        public string InputText;
        public int OperatorType;
        public int OperatorPosition;

        public Calculator()
        {
            this.Total = 0;
            this.LastOperation = "";
            this.TotalText = "";
            this.InputText = "";
            this.OperatorPosition = -1;
            this.OperatorType = -1;
        }

        public Calculator(double total, string lastoperation)
        {
            this.Total = total;
            this.LastOperation = lastoperation;
            this.TotalText = total.ToString();
            this.InputText = "";
            this.OperatorPosition = -1;
            this.OperatorType = -1;
        }

        public bool WriteMenu() // Writes everything on the screen
        {
            Console.Clear();
            Console.WriteLine("Basic Calculator - E for exit");
            Console.WriteLine("Total: {0}", TotalText);
            Console.WriteLine("Last operation: {0}", LastOperation);
            Console.Write("> ");
            return true;
        }

        public bool FindOperator(string inputtext) // Finds the + - * / 
        {
            if (inputtext.Length > 0)
                this.InputText = inputtext; // Uses input parameter
            if (this.InputText.Length <= 0)
            {
                this.Total = 0;
                TotalText = Total.ToString();
                LastOperation = "";
                return false;
            }

            char[] symbols = { '*', '/', '+', '-' };     // Where is the symbol?
            OperatorPosition = this.InputText.IndexOfAny(symbols);
            if (OperatorPosition == -1)
            {
                Double.TryParse(this.InputText, out this.Total); // No symbol. Tries parsing the string and returns
                TotalText = Total.ToString();
                LastOperation = TotalText;
                return false;
            }


            for (int i = 0; i < symbols.Length; i++)    // Which symbol is it?
            {
                if (this.InputText[OperatorPosition] == symbols[i])
                    OperatorType = i;
            }

            return true;
        }

        public string GetInput() //Gets the calculation from the user
        {
            this.InputText = Console.ReadLine().ToUpper(); // In case of e or c

            if (this.InputText.Length <= 0)
                if (this.LastOperation.Length > 0)
                    this.InputText = this.LastOperation; // Reuses last operation
                else
                    return this.InputText; //To avoid other exceptions if strings are empty


            if (this.InputText[0] == 'E')
                throw new Exception("Please Exit");
            if (this.InputText[0] == 'C')
                throw new Exception("Memory Cleared");

            return this.InputText;
        }

        public double[] ParseNumbers(string inputtext, int operatorposition)  // Finds and returns 2 numbers
        {
            double[] numren = new double[2];

            if (inputtext.Length > 0)
                this.InputText = inputtext;

            else if (this.InputText.Length <= 0)
                throw new Exception("Bad Input Data");  // Both strings empty

            if (operatorposition >= 0 && operatorposition < this.InputText.Length) // Uses parameter if valid
                this.OperatorPosition = operatorposition;
            else if (this.OperatorPosition < 0 || this.OperatorPosition >= this.InputText.Length)
                throw new Exception("Bad Input Data");


            if (this.OperatorPosition == 0) // Operatorsymbol first, for example "+2"
            {
                numren[0] = Total;  //last total is first number
            }
            else if (!Double.TryParse(this.InputText.Substring(0, this.OperatorPosition), out numren[0])) //Can't parse first number
                throw new Exception("Bad Input Data");

            if (!Double.TryParse(this.InputText.Substring(this.OperatorPosition + 1), out numren[1])) //Can't parse second number
                throw new Exception("Bad Input Data");

            return numren;
        }

        public double Multiplication(double[] numbers)
        {
            if (numbers.Length == 0)
                throw new Exception("Where are the numbers?"); //Empty array

            Total = numbers[0];
            for (int i = 1; i < numbers.Length; i++) //Add all of them
                Total *= numbers[i];
            return Total;

        }

        public double Division(double[] numbers)
        {
            if (numbers.Length == 0)
                throw new Exception("Where are the numbers?"); // Empty array

            Total = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (i % 2 == 0)                 /*    a / b / c == (a * c) / b  */    /* a and c can be 0 but not b */
                    Total *= numbers[i];
                else if (numbers[i] == 0)
                    throw new Exception("You can't divide by zero");
                else Total /= numbers[i];
            }

            return Total;
        }

        public double Addition(double[] numbers)
        {
            if (numbers.Length == 0)
                throw new Exception("Where are the numbers?");

            Total = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
                Total += numbers[i];
            return Total;
        }

        public double Subtraction(double[] numbers)
        {
            if (numbers.Length == 0)
                throw new Exception("Where are the numbers?");

            Total = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
                Total -= numbers[i];
            return Total;
        }



    }

    public class Program
    {
        static void Main(string[] args)
        {

            Calculator calc = new Calculator(); // One new calculator please

            // Writes out the calculator first time

            while (true)
            {
                try
                {
                    calc.WriteMenu(); // Writes the text on the  screen including the last calculations result

                    calc.GetInput(); //Requests input from user

                    if (calc.InputText.Length <= 0) // Empty input returns to beginning of loop
                        continue;

                    if (!calc.FindOperator("")) //Tries to find an operator
                        continue;


                    double[] numbers = calc.ParseNumbers(calc.InputText, calc.OperatorPosition);
                    switch (calc.OperatorType)
                    {
                        case 0: //Multiplication
                            calc.Multiplication(numbers);
                            break;

                        case 1: // Division
                            calc.Division(numbers);
                            break;

                        case 2: // Addition
                            calc.Addition(numbers);
                            break;

                        case 3: // Subtraction
                            calc.Subtraction(numbers);
                            break;

                        default: //exit
                            return;
                    }

                    calc.LastOperation = calc.InputText;       //Stores the last operation
                    calc.TotalText = calc.Total.ToString();

                }
                catch (Exception e)
                {
                    if (e.Message == "Please Exit")
                        return;

                    calc.Total = 0;
                    calc.LastOperation = "";
                    calc.TotalText = e.Message;
                    calc.InputText = "";
                    calc.OperatorPosition = -1;
                    calc.OperatorType = -1;

                }
            }


        }
    }
}

