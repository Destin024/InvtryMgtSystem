using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        private readonly DataInvntryContext _ctx;

        public TransactionController(ITransactionRepository transactionRepository, IMapper mapper,DataInvntryContext ctx)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _ctx = ctx;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetTransaction()
        {
            var transactions = _mapper.Map<List<TransactionDto>>(_transactionRepository.GetTransactions());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(transactions);
        }

        [HttpGet("transactionId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetTransaction(Guid transactionId)
        {
            var transaction = _mapper.Map<TransactionDto>(_transactionRepository.GetTransaction(transactionId));

            if (transactionId == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(transaction);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult CreateTransaction([FromBody] TransactionDto createTransaction)
        {
            if (createTransaction == null)
            {
                return BadRequest(ModelState);
            }
            var transaction = _transactionRepository.GetTransactions().
                Where(t => t.TransactionId == createTransaction.TransactionId).FirstOrDefault();
            if (transaction != null)
            {
                ModelState.AddModelError("", "TransactionAlready Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionMap = _mapper.Map<Transaction>(createTransaction);

            var InventoryResult = _ctx.Inventories.Where(o => o.InventoryId == transaction.TransactionId).FirstOrDefault();
            transaction.RemainingQuantity = InventoryResult.InventoryQuantity;
            InventoryResult.InventoryQuantity = InventoryResult.InventoryQuantity - transaction.InitialQuantity;

            

            if (!_transactionRepository.CreateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }
                
            
            return Ok("Successfully saved");
        }

        [HttpGet]
        [Route(template: "GetTransactionByInventoryName")]
        public async Task<IActionResult> GetAllTransactionByInventoryName(string inventoryName)
        {
            var transaction = await _ctx.Inventories.Where(o => o.InventoryQuantity.ToString() == inventoryName).FirstOrDefaultAsync();
            var getTransaction = await _ctx.Transactions.FindAsync(transaction);
            transaction.InventoryQuantity = getTransaction.RemainingQuantity;
            transaction.InventoryQuantity = getTransaction.Inventory.InventoryQuantity;
            return Ok(getTransaction);
        }

        [HttpGet]
        [Route(template: "GetTransactionByInventoryId")]
        public async Task<IActionResult> GetAllTransactionByStoreId(Guid inventoryId)
        {
            List<InventoryDto> list = new List<InventoryDto>();
            var result = await _ctx.Transactions.Include(o => o.Inventory).Include(o=>o.InventoryId)
                .Where(o => o.TransactionId == inventoryId).ToListAsync();
            foreach (var item in result)
            {
                list.Add(new InventoryDto
                {
                    InventoryId = item.InventoryId,
                    InventoryQuantity = item.Inventory.InventoryQuantity
                });
            }
            return Ok(list);
        }

        [HttpPut("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateTransaction(Guid transactionId, [FromBody] TransactionDto updateTransaction)
        {
            if (updateTransaction == null)
            {
                return BadRequest(ModelState);
            }
            if (transactionId == null)
            {
                return BadRequest(ModelState);
            }
            if (!_transactionRepository.TransactionExists(transactionId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var transactionMap = _mapper.Map<Transaction>(updateTransaction);
            if (!_transactionRepository.UpdateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating store");
            }
            return NoContent();
        }

        [HttpDelete("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteTransaction(Guid transactionId)
        {
            if (!_transactionRepository.TransactionExists(transactionId))
            {
                return NotFound();
            }
            var transactionToDelete = _transactionRepository.GetTransaction(transactionId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_transactionRepository.DeleteTransaction(transactionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting Transaction");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }
    }
}
