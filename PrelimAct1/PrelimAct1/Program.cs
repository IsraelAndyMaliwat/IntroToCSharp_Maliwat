/*
    Lab: Codac Logistics - Driver Fuel & Performance Tracker
    ---------------------------------------------------
    Name: Israel Andy T. Maliwat

    Task Description:
    This console-based tool helps Codac Logistics track the daily fuel expenses 
    and delivery performance of a single vehicle over a 5-day work week.
    It gathers driver profiles, validates distance data, calculates fuel efficiency ratings,
    and provides a financial summary for the accounting department.

    Technical Features:
    - Data Types: string, int, double, decimal, bool
    - I/O: Console.ReadLine(), String Interpolation ($"")
    - Validation: while loop for distance input
    - Data Structure: 1D Array for 5 days of fuel expenses
    - Control Flow: for loops, if/else for efficiency ratings
*/


using System;

class Program
{
    static void Main(string[] args)
    {
        // Console formatting for better user experience
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==============================================");
        Console.WriteLine("   CODAC LOGISTICS - FUEL EXPENSE TRACKER    ");
        Console.WriteLine("==============================================");
        Console.ResetColor();
        Console.WriteLine();

        // TASK 1: DRIVER PROFILE & VALIDATION 
        // Using string for driver name because it is appropriate for text data
        Console.Write("Enter Driver's Full Name: ");
        string driverName = Console.ReadLine() ?? "Unknown Driver";
        
        // Using decimal for budget for financial calculations and to avoid precision issues with double/float
        decimal weeklyBudget = 0;
        bool validBudget = false;
        while (!validBudget)
        {
            Console.Write("Enter Weekly Fuel Budget (₱): "); 
            string budgetInput = Console.ReadLine() ?? "0";
            if (decimal.TryParse(budgetInput, out weeklyBudget) && weeklyBudget >= 0)
            {
                validBudget = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Please enter a valid positive budget amount!");
                Console.ResetColor();
            }
        }
        
        // Distance validation using while loop to ensure user enters a valid number within the specified range 
        double totalDistance = 0;
        bool validDistance = false;
        
        while (!validDistance)
        {
            Console.Write("Enter Total Distance Traveled this week (1.0 - 5000.0 km): ");
            string distanceInput = Console.ReadLine() ?? "0";
            
            // TryParse prevents crashes from invalid input
            if (double.TryParse(distanceInput, out totalDistance))
            {
                // Validate range: between 1.0 and 5000.0 inclusive
                if (totalDistance >= 1.0 && totalDistance <= 5000.0)
                {
                    validDistance = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Distance must be between 1.0 and 5000.0 kilometers!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: Please enter a valid number!");
                Console.ResetColor();
            }
        }
        
        Console.WriteLine();
        
        // TASK 2: FUEL EXPENSE TRACKING 
        // Array to store daily expenses for fixed-size collection of 5 days using decimal for monetary values of fuel expenses
        decimal[] fuelExpenses = new decimal[5];
        decimal totalFuelSpent = 0;
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- DAILY FUEL EXPENSES (Day 1-5) ---");
        Console.ResetColor();
        
        // For loop ideal for iterating through array with known number of iterations
        for (int day = 0; day < fuelExpenses.Length; day++)
        {
            bool validExpense = false;
            
            while (!validExpense)
            {
                Console.Write($"Enter fuel cost for Day {day + 1}: ₱"); 
                string expenseInput = Console.ReadLine() ?? "0";
                
                // TryParse with decimal for safe conversion
                if (decimal.TryParse(expenseInput, out fuelExpenses[day]) && fuelExpenses[day] >= 0)
                {
                    totalFuelSpent += fuelExpenses[day];
                    validExpense = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Please enter a valid positive amount!");
                    Console.ResetColor();
                }
            }
        }
        
        // TASK 3: PERFORMANCE ANALYSIS 
        
        // Calculate average daily expense
        decimal averageDailyExpense = totalFuelSpent / 5;
        
        // Fuel Efficiency Calculation
        // Using double for division since efficiency can have decimal places 
        // Cast to double because totalDistance is double and totalFuelSpent is decimal 
        
        // Added check for zero fuel spent to prevent division by zero
        double efficiency;
        if (totalFuelSpent == 0)
        {
            efficiency = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nWARNING: No fuel expenses recorded. Efficiency cannot be calculated.");
            Console.ResetColor();
        }
        else
        {
            efficiency = totalDistance / (double)totalFuelSpent;
        }
        
        // Determine efficiency rating using if/else if/else structure
        string efficiencyRating;
        if (efficiency > 15)
        {
            efficiencyRating = "High Efficiency";
        }
        else if (efficiency >= 10)
        {
            efficiencyRating = "Standard Efficiency";
        }
        else
        {
            efficiencyRating = "Low Efficiency / Maintenance Required";
        }
        
        // Budget status check - boolean flag for under/over budget
        bool isUnderBudget = totalFuelSpent <= weeklyBudget;
        
        // TASK 4: THE AUDIT REPORT 
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("==============================================");
        Console.WriteLine("            WEEKLY FUEL AUDIT REPORT         ");
        Console.WriteLine("==============================================");
        Console.ResetColor();
        Console.WriteLine();
        
        // Driver Information
        Console.WriteLine($"Driver: {driverName}");
        Console.WriteLine($"Weekly Budget: ₱{weeklyBudget:F2}"); 
        Console.WriteLine($"Total Distance: {totalDistance:F1} km");
        Console.WriteLine();
        
        // Daily Expense Breakdown
        Console.WriteLine("Daily Fuel Expenses:");
        Console.WriteLine("--------------------");
        // For loop to display all array elements
        for (int day = 0; day < fuelExpenses.Length; day++)
        {
            Console.WriteLine($"Day {day + 1}: ₱{fuelExpenses[day]:F2}"); 
        }
        
        Console.WriteLine("--------------------");
        Console.WriteLine($"Total Fuel Spent: ₱{totalFuelSpent:F2}"); 
        Console.WriteLine($"Average Daily Expense: ₱{averageDailyExpense:F2}"); 
        Console.WriteLine();
        
        // Efficiency Analysis
        if (totalFuelSpent > 0)
        {
            Console.WriteLine($"Fuel Efficiency: {efficiency:F2} km/₱"); 
        }
        else
        {
            Console.WriteLine("Fuel Efficiency: N/A (No fuel purchased)");
        }
        Console.WriteLine($"Efficiency Rating: {efficiencyRating}");
        Console.WriteLine();
        
        // Budget Status
        Console.Write("Budget Status: ");
        if (isUnderBudget)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"UNDER BUDGET (Saved ₱{weeklyBudget - totalFuelSpent:F2})"); 
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"OVER BUDGET (Exceeded by ₱{totalFuelSpent - weeklyBudget:F2})"); 
        }
        Console.ResetColor();
        
        // Boolean flag display for accounting department
        Console.WriteLine($"\nStayed Under Budget: {isUnderBudget}");
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==============================================");
        Console.WriteLine("     REPORT GENERATED FOR ACCOUNTING DEPT    ");
        Console.WriteLine("==============================================");
        Console.ResetColor();
        
        
        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine(); 
    }
}