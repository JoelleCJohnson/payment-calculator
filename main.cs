using System;
using static System.Console;
using System.Globalization;

class Program {
  public static void Main (string[] args) {
    //declaring variables
    double loanAmount, interest, paymentAmount;
    int years;
    string answer;
    
    //do-while so that the loop is executed initially
    do{
      //Recieving initial input
      Write("Enter loan amount >>");
      loanAmount = Convert.ToDouble(ReadLine());
      Write("Interest rate as a decimal value >>");
      interest = Convert.ToDouble(ReadLine());
      Write("Number of years to finance >>");
      years = Convert.ToInt32(ReadLine());

      //Calling Column headers:
      WriteLine(columnHeaders());
      
      //saving paymentAmount to be passed to other methods
      paymentAmount = CalculateMonthlyCharges(years, loanAmount, interest);

      //Calling methods
      ReturnAmortizationSchedule(paymentAmount, interest, loanAmount, years);
      OutputMonthlyPayment(paymentAmount);
      TotalInterestPaid(interest, loanAmount, paymentAmount, years);

      //Gives option to run the loop again
      Write("Do you want to do another calculation? Y/N");
      answer = ReadLine();

    //Will run loop again if input is "Y"
    }while(answer == "Y");
  }
  //I feel I made my Methods pretty self explanitory
  static string columnHeaders(){
    string header = "Month          Int     Prin      New\nNum            Paid    Paid      Balance";
    return header;
  }
  
  static double CalculateMonthlyCharges(int years, double loanAmount, double interest){
    int payments = 12 * years;
    double term = Math.Pow((1 + interest / 12), payments);
    double balance = loanAmount; 
    double paymentAmount = loanAmount * interest / 12 * term / (term - 1.0);
    return paymentAmount;
  }
  
  static void ReturnAmortizationSchedule(double paymentAmount, double interest, double loanAmount, int years){
    //initialize local variables
    int x = 1;
    double monthlyInterest = 0;
    double principle = 0; 
    int payments = 12 * years;

      //while loop to iterate math through each month of the loan
    while(x <= payments){

      monthlyInterest = interest / 12 * loanAmount; 
      principle = paymentAmount - monthlyInterest;
      loanAmount -= principle;

      //convert to strings & format to American currency values
      string interestString = monthlyInterest.ToString("C", CultureInfo.GetCultureInfo("en-US"));
      string principleString = principle.ToString("C", CultureInfo.GetCultureInfo("en-US"));
      string balanceString = loanAmount.ToString("C", CultureInfo.GetCultureInfo("en-US"));
      
      //outputs math functions in correct column
      WriteLine("{0}         {1}      {2}      {3}", x, interestString, principleString, balanceString);

      //increments control loop variable
      x++;
    }
  }
  
  static void TotalInterestPaid(double interest, double loanAmount, double paymentAmount, int years){
    //initialize variables
    int x = 1;
    int payments = 12 * years;
    double totalInterest = 0;

    //while loop to add interest from every month of the loan
    while(x <= payments){
    
      double monthlyInterest = interest / 12 * loanAmount; 
      double principle = paymentAmount - monthlyInterest;
      loanAmount -= principle;
      
    //holds total interest
      totalInterest += monthlyInterest;

    //increment loop control variable 
      ++x;
      }
    
    //convert to string in local currency formatting
    string totalInterestString = totalInterest.ToString("C", CultureInfo.GetCultureInfo("en-US"));

    //output
    WriteLine("Total interest paid: {0}", totalInterestString);
  }
  
  static void OutputMonthlyPayment(double paymentAmount){
    //output with proper formatting
    WriteLine("Monthly payment: {0}", paymentAmount.ToString("C", CultureInfo.GetCultureInfo("en-US")));
    }
}