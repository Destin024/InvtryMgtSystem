using AutoMapper;
using InvtryMgtSystemAPI.Data;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        private readonly DataInvntryContext _ctx;

        public InventoryController(IInventoryRepository inventoryRepository,IMapper mapper,DataInvntryContext ctx)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
            _ctx = ctx;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetInventory()
        {
            var inventories = _mapper.Map<List<InventoryDto>>(_inventoryRepository.GetInventories());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(inventories);
        }
        [HttpGet("inventoryId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetInventory(Guid inventoryId)
        {
            var inventory = _mapper.Map<InventoryDto>(_inventoryRepository.GetInventory(inventoryId));

            if (inventoryId == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(inventory);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        public IActionResult CreateInventory([FromBody]InventoryDto createInventory)
        {
            if (createInventory == null)
            {
                return BadRequest(ModelState);
            }
            var inventory = _inventoryRepository.GetInventories()
                .Where(i => i.InventoryQuantity == createInventory.InventoryQuantity).FirstOrDefault();
            if (inventory != null)
            {
                ModelState.AddModelError("", "Inventory Already Exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventoryMap = _mapper.Map<Inventory>(createInventory);

            

            if (!_inventoryRepository.CreateInventory(inventoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving ");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully saved");
        }

        [HttpPut("{inventoryId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Updateinventory(Guid inventoryId,[FromBody]InventoryDto updatedInventory)
        {
            if (updatedInventory == null)
            {
                return BadRequest(ModelState);
            }
            if (inventoryId == null)
            {
                return BadRequest(ModelState);
            }
            if (!_inventoryRepository.InventoryExists(inventoryId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventiryMap = _mapper.Map<Inventory>(updatedInventory);

            if (!_inventoryRepository.UpdateInventory(inventiryMap))
            {
                ModelState.AddModelError("", "Something went wrong while Updating inventory");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{inventoryId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteInventory(Guid inventoryId)
        {
            if (!_inventoryRepository.InventoryExists(inventoryId))
            {
                return NotFound();
            }
            var inventoryToDelete = _inventoryRepository.GetInventory(inventoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_inventoryRepository.DeleteInventory(inventoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting inventory");
                return StatusCode(400, ModelState);
            }
            return NoContent();
        }
    }
}
