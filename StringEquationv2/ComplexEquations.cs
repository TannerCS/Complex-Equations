using System;
using System.Text.RegularExpressions;

namespace StringEquationv2
{
    public class ComplexEquations
    {
        #region variables
        private Regex regex;
        private MatchCollection matches;
        #endregion

        #region public
        public string Calculate(string equation)
        {
            //Format equation
            equation = FormatEquation(equation);
            //First, solve for parentheses.
            equation = Parentheses(equation);
            //Second, solve for exponents.
            equation = Exponents(equation);
            //Third, multiply or divide.
            equation = MultiplyDivision(equation);
            //Finally, add or subtract.
            equation = AddSubtract(equation);

            //return final answer
            return equation;
        }
        #endregion

        #region private
        private string Parentheses(string equation)
        {
            //\(([^()]*)\)
            regex = new Regex(@"\(([^()]*)\)");
            matches = regex.Matches(equation);

            //If there are parentheses
            if (matches.Count != 0)
            {
                //Loop through the matches and solve them
                foreach (Match match in matches)
                {
                    //replace the existing parentheses with the new value
                    string newVal = equation.Replace(match.Value, Calculate(match.Value.TrimStart('(').TrimEnd(')')).ToString());
                    //calculate equation after solving. Double checking
                    newVal = Calculate(newVal);
                    equation = newVal;
                }
            }

            return equation;
        }

        private string Exponents(string equation)
        {
            regex = new Regex(@"(\-?[0-9]+\^\-?[0-9]+)");
            matches = regex.Matches(equation);

            //If there are equations to format
            if (matches.Count != 0)
            {
                //Loop through the matches and solve them
                foreach (Match match in matches)
                {
                    //create a temp equation
                    string newEquation = "";
                    //split exponent numbers
                    string[] nums = match.Value.Split("^");

                    //loop exponent number amount of times
                    for (double i = 0; i < double.Parse(nums[1]); i++)
                    {
                        //multiply exponential number
                        newEquation += nums[0] + "*";
                    }

                    //calculate equation
                    newEquation = Calculate(newEquation.TrimEnd('*'));

                    if (nums[0].Contains("-") && (double.Parse(nums[1])) % 2 == 0)
                    {
                        //replace existing equation with new value
                        equation = equation.Replace(match.Value, '-' + newEquation);
                    }
                    else
                    {
                        //replace existing equation with new value
                        equation = equation.Replace(match.Value, '+' + newEquation);
                    }
                }
            }
            //return final answer
            return equation;
        }

        private string MultiplyDivision(string equation)
        {
            foreach (char character in equation)
            {
                if (character == '*')
                {
                    regex = new Regex(@"(\-?[?0-9\.?]+)\*(\-?[?0-9\.?]+)");
                    matches = regex.Matches(equation);

                    //If there are equations to multiply
                    if (matches.Count != 0)
                    {
                        //Loop through the matches and solve them
                        foreach (Match match in matches)
                        {
                            string[] nums = match.Value.Split('*');
                            //replace the existing equation with the new value
                            equation = equation.Replace(match.Value, (Convert.ToDouble(nums[0]) * Convert.ToDouble(nums[1])).ToString());
                            //calculate equation after solving. Double checking
                            equation = Calculate(equation);
                        }
                    }
                }
                else if (character == '/')
                {
                    regex = new Regex(@"(\-?[?0-9\.?]+)\/(\-?[?0-9\.?]+)");
                    matches = regex.Matches(equation);

                    //If there are equations to divide
                    if (matches.Count != 0)
                    {
                        //Loop through the matches and solve them
                        foreach (Match match in matches)
                        {
                            string[] nums = match.Value.Split('/');
                            //replace the existing equation with the new value
                            equation = equation.Replace(match.Value, (Convert.ToDouble(nums[0]) / Convert.ToDouble(nums[1])).ToString());
                            //calculate equation after solving. Double checking
                            equation = Calculate(equation);
                        }
                    }
                }
            }


            return equation;
        }

        private string AddSubtract(string equation)
        {
            for (int i = 0; i < equation.Length; i++)
            {
                if (equation[i] == '+')
                {
                    regex = new Regex(@"(\-?[?0-9\.?]+)\+(\-?[?0-9\.?]+)");
                    matches = regex.Matches(equation);

                    //If there are equations to multiply
                    if (matches.Count != 0)
                    {
                        //Loop through the matches and solve them
                        foreach (Match match in matches)
                        {
                            string[] nums = match.Value.Split('+');
                            //replace the existing equation with the new value
                            equation = equation.Replace(match.Value, (Convert.ToDouble(nums[0]) + Convert.ToDouble(nums[1])).ToString());
                            //calculate equation after solving. Double checking
                            equation = Calculate(equation);
                        }
                    }
                }
                else if (equation[i] == '-' && i != 0 && int.TryParse(equation[i - 1].ToString(), out int result))
                {
                    regex = new Regex(@"(\-?[?0-9\.?]+)\-(\-?[?0-9\.?]+)");
                    matches = regex.Matches(equation);

                    //If there are equations to divide
                    if (matches.Count != 0)
                    {
                        //Loop through the matches and solve them
                        foreach (Match match in matches)
                        {
                            Regex rg = new Regex(@"(\-?[?0-9\.?]+)");
                            MatchCollection m = rg.Matches(equation);
                            //replace the existing equation with the new value
                            if (m.Count > 1)
                                equation = equation.Replace(match.Value, (Convert.ToDouble(m[0].Value) + Convert.ToDouble(Convert.ToDouble(m[1].Value))).ToString());
                            //calculate equation after solving. Double checking
                            equation = Calculate(equation);
                        }
                    }
                }
            }


            return equation;
        }

        private string FormatEquation(string equation)
        {
            equation = equation.Replace(" ", string.Empty);

            if (equation.Contains("--"))
            {
                equation = equation.Replace("--", "+");
            }

            regex = new Regex(@"([0-9]+\()");
            matches = regex.Matches(equation);

            //If there are equations to format
            if (matches.Count != 0)
            {
                //Loop through the matches and solve them
                foreach (Match match in matches)
                {
                    //insert multiplication symbol
                    equation = equation.Insert(equation.IndexOf(match.Value) + match.Value.Length - 1, "*");
                }
            }

            regex = new Regex(@"(\)[0-9]+)");
            matches = regex.Matches(equation);

            //If there are equations to format
            if (matches.Count != 0)
            {
                //Loop through the matches and solve them
                foreach (Match match in matches)
                {
                    //insert multiplication symbol
                    equation = equation.Insert(equation.IndexOf(match.Value) + 1, "*");
                }
            }
            return equation;
        }
        #endregion
    }
}
