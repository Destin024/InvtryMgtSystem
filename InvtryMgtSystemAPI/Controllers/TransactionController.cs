using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
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

        public IActionResult GetTransaction(int trabsactionId)
        {
            var transaction = _mapper.Map<TransactionDto>(_transactionRepository.GetTransaction(trabsactionId));

            if (trabsactionId == 0)
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

            //var InventoryResult = _ctx.ProductInventories.Where(o => o.Id == tran.ProductInventoryId).FirstOrDefault();
            //transaction.InitialQuantity = InventoryResult.TotalQuantity;
            //InventoryResult.TotalQuantity = InventoryResult.TotalQuantity - tran.QuantitySold;

            //[HttpGet]
            //[Route(template: "GetInventoryByStoreName")]
            //public async Task<IActionResult> GetAllInventoryByStoreName(string Storename)
            //{
            //    var store = await _ctx.ProductInventories.Where(o => o.Store.StoreName == Storename).FirstOrDefaultAsync();
            //    var getStore = await _ctx.ProductInventories.FindAsync(store);
            //    store.ProductId = getStore.ProductId;
            //    store.Product.Name = getStore.Product.Name;
            //    return Ok(getStore);
            //}

            //[HttpGet]
            //[Route(template: "GetInventoryByStoreId")]
            //public async Task<IActionResult> GetAllInventoryByStoreName(Guid StoreId)
            //{
            //    List<StoreInfoRequestDto> list = new List<StoreInfoRequestDto>();
            //    var result = await _ctx.ProductInventories.Include(o => o.Product)
            //        .Include(o => o.Store).Where(o => o.StoreID == StoreId).ToListAsync();
            //    foreach (var item in result)
            //    {
            //        list.Add(new StoreInfoRequestDto
            //        {
            //            Id = item.Id,
            //            Store = item.Store.StoreName,
            //            ProductId = item.ProductId,
            //            Product = item.Product.Name,
            //            TotalQuantity = item.TotalQuantity,
            //            Price = item.Price
            //        });
            //    }
            //    return Ok(list);
            //}

            if (!_transactionRepository.CreateTransaction(transactionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }
                
            
            return Ok("Successfully saved");
        }

        [HttpPut("{transactionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateTransaction(int transactionId, [FromBody] TransactionDto updateTransaction)
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

        public IActionResult DeleteTransaction(int transactionId)
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
