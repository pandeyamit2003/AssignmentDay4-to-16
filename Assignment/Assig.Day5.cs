using System;
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message) { }
}
public class InvalidAmountException : Exception
{
    public InvalidAmountException(string message) : base(message) { }
}
// Class of Bank Account
public class BankAccount
{
    public string AccountHolderName { get; set; }
    public double Balance { get; private set; }
    private const double MIN_BALANCE = 1000;
    public BankAccount(string name, double initialBalance)
    {
        AccountHolderName = name;
        if (initialBalance < MIN_BALANCE)
            throw new ArgumentException("Initial balance must be at least ₹1000");
        Balance = initialBalance;
    }
   
// Deposit Method
public void Deposit(double amount)
    {
        try
        {
            if (amount <= 0)
                throw new InvalidAmountException("Deposit amount must be greater than 0");
                Balance += amount;
            Console.WriteLine($"₹{amount} deposited successfully.");
        }
        catch (InvalidAmountException ex)
        {
            Console.WriteLine($"Deposit Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Current Balance: ₹{Balance}\n");
        }
    }
    // Withdraw Method
    public void Withdraw(double amount)
    {
        try
        {
            if (amount <= 0)
                throw new InvalidAmountException("Withdrawal amount must be greater than 0");
        if (amount > Balance)
                throw new InsufficientBalanceException("Withdrawal denied: Insufficient balance");
        if ((Balance - amount) < MIN_BALANCE)
                throw new InsufficientBalanceException("Cannot withdraw: Minimum balance of ₹1000 must be maintained");
                Balance -= amount;
            Console.WriteLine($"₹{amount} withdrawn successfully.");
        }
        catch (InvalidAmountException ex)
        {
            Console.WriteLine($"Withdraw Error: {ex.Message}");
        }
        catch (InsufficientBalanceException ex)
        {
            Console.WriteLine($"Withdraw Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"Current Balance: ₹{Balance}\n");
        }
    }
    // Check Balance
    public void CheckBalance()
    {
        Console.WriteLine($"Account Holder: {AccountHolderName}");
        Console.WriteLine($"Available Balance: ₹{Balance}\n");
    }
}
// Main Program
class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create Account
            BankAccount account = new BankAccount("John Doe", 5000);
            account.CheckBalance();
            // Valid Deposit
            account.Deposit(2000);
            // Invalid Deposit
            account.Deposit(-500);
            // Valid Withdraw
            account.Withdraw(1500);
            // Withdraw exceeding balance rule
            account.Withdraw(6000);
            // Withdraw violating minimum balance rule
            account.Withdraw(3000);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Account Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Transaction session ended.");
        }
    }
}