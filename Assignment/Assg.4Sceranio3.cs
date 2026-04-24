using System;
using System.Collections.Generic;
using System.Linq;
class Transaction
{
    public string TransactionId;
    public string AccountId;
    public double Amount;
}
class BankingSystem
{
    private List<Transaction> transactionHistory = new List<Transaction>();
    private Dictionary<string, double> accountBalances = new
    Dictionary<string, double>();
    private Queue<Transaction> pendingTransactions = new
    Queue<Transaction>();
    private Stack<Transaction> rollbackStack = new Stack<Transaction>();
    private HashSet<string> transactionIds = new HashSet<string>();
    // Create Account
    public void CreateAccount(string accountId, double initialBalance)
    {
        if (!accountBalances.ContainsKey(accountId))
        {
            accountBalances[accountId] = initialBalance;
            Console.WriteLine($"Account {accountId} created with balance{ initialBalance}");
        }
        else
        {
            Console.WriteLine("Account already exists.");
        }
    }
    // Add Transaction (to queue)
    public void AddTransaction(Transaction tx)
    {
        if (!transactionIds.Add(tx.TransactionId))
        {
            Console.WriteLine("Duplicate transaction ID rejected.");
            return;
        }
        pendingTransactions.Enqueue(tx);
        Console.WriteLine($"Transaction {tx.TransactionId} added to queue.");
    }
    // Process Transactions (FIFO)
    public void ProcessNextTransaction()
    {
        if (pendingTransactions.Count == 0)
        {
            Console.WriteLine("No pending transactions.");
            return;
        }
        Transaction tx = pendingTransactions.Dequeue();
        if (!accountBalances.ContainsKey(tx.AccountId))
        {
            Console.WriteLine("Account not found.");
            return;
        }
        double currentBalance = accountBalances[tx.AccountId];
        // Check sufficient balance for withdrawal
        if (tx.Amount < 0 && currentBalance + tx.Amount < 0)
        {
            Console.WriteLine("Insufficient balance. Transaction failed.");
            return;
        }
        // Apply transaction
        accountBalances[tx.AccountId] += tx.Amount;
        transactionHistory.Add(tx);
        rollbackStack.Push(tx);
        Console.WriteLine($"Processed Transaction {tx.TransactionId}, NewBalance: { accountBalances[tx.AccountId]}");
}
    // Rollback last transaction (LIFO)
    public void RollbackLastTransaction()
    {
        if (rollbackStack.Count == 0)
        {
            Console.WriteLine("No transaction to rollback.");
            return;
        }
        Transaction lastTx = rollbackStack.Pop();
        // Reverse the transaction
        accountBalances[lastTx.AccountId] -= lastTx.Amount;
        transactionHistory.Remove(lastTx);
        Console.WriteLine($"Rolled back Transaction {lastTx.TransactionId},Balance Restored: { accountBalances[lastTx.AccountId]}");
       }
    // Show Balance
    public void ShowBalance(string accountId)
    {
        if (accountBalances.ContainsKey(accountId))
        {
            Console.WriteLine($"Balance for {accountId}:{accountBalances[accountId]}");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }
        // Show Transaction History
public void ShowTransactionHistory()
    {
        Console.WriteLine("\nTransaction History:");
        foreach (var tx in transactionHistory)
        {
            Console.WriteLine($"{tx.TransactionId} | {tx.AccountId} |{ tx.Amount}");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        BankingSystem bank = new BankingSystem();
        // Create Accounts
        bank.CreateAccount("ACC1", 1000);
        bank.CreateAccount("ACC2", 500);
        // Add Transactions
        bank.AddTransaction(new Transaction{TransactionId = "TXN1",AccountId = "ACC1",Amount = 20}); 
        bank.AddTransaction(new Transaction{TransactionId = "TXN2",AccountId = "ACC1",Amount = -300});
        bank.AddTransaction(new Transaction{TransactionId = "TXN3",AccountId = "ACC2",Amount = -600}); 
        bank.AddTransaction(new Transaction{TransactionId = "TXN1",AccountId = "ACC1",Amount = 100}); 
        
        // Process Transactions
        bank.ProcessNextTransaction();
        bank.ProcessNextTransaction();
        bank.ProcessNextTransaction();
        
        // Show Balances
        bank.ShowBalance("ACC1");
        bank.ShowBalance("ACC2");
        
        // Rollback
        bank.RollbackLastTransaction();
        
        // Show Balance after rollback
        bank.ShowBalance("ACC1");
        
        // Show History
        bank.ShowTransactionHistory();
        Console.WriteLine("\nExecution Completed.");
    }
}