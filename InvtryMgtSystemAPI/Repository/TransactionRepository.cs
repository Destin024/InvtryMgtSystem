using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Repository
{
    public class TransactionRepository:ITransactionRepository
    {
        private readonly DataInvntryContext _context;
        private readonly IMapper _mapper;

        public TransactionRepository(DataInvntryContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            return save();
        }

        public bool DeleteTransaction(Transaction transaction)
        {
            _context.Remove(transaction);
            return save();
        }

        public Transaction GetTransaction(Guid transactionId)
        {
            return _context.Transactions.Where(t => t.TransactionId == transactionId).FirstOrDefault();
        }

        public ICollection<Transaction> GetTransactions()
        {
            return _context.Transactions.OrderBy(t => t.TransactionId).ToList();
        }

        public bool save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TransactionExists(Guid transactionId)
        {
            return _context.Transactions.Any(s => s.TransactionId == transactionId);
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            _context.Update(transaction);
            return save();
        }
    }
}
