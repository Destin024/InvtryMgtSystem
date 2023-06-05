using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetTransactions();
        Transaction GetTransaction(Guid transactionId);
        bool TransactionExists(Guid transactionId);
        bool CreateTransaction(Transaction transaction);
        bool UpdateTransaction(Transaction transaction);
        bool DeleteTransaction(Transaction transaction);
        bool save();
    }
}
