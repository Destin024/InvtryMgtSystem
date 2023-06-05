using AutoMapper;
using Azure;
using Duende.IdentityServer.Models;
using InvtryMgtSystemAPI.Data.Dto;
using InvtryMgtSystemAPI.Interfaces;
using InvtryMgtSystemAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvtryMgtSystemAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreController(IStoreRepository storeRepository,IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult GetStores()
        {

            var stores = _mapper.Map<List<StoreDto>>(_storeRepository.GetStores());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(stores);
        }
        [HttpGet("storeId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult GetStore(Guid storeId)
        {
            var store = _mapper.Map<StoreDto>(_storeRepository.GetStore(storeId));

            if (storeId == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(store);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateStore([FromBody]StoreDto createStore)
        {
            if (createStore == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Models.Response { Status = StatusCodes.Status404NotFound, Message = "Bad Request"});
            }
            var store = _storeRepository.GetStores()
                .Where(s => s.Name.Trim().ToUpper() == createStore.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (store != null)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new Models.Response { Status = StatusCodes.Status422UnprocessableEntity,Message = "Store Already Exists"} );
            }
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Models.Response { Status =StatusCodes.Status400BadRequest, Message ="Invalid Request"});
            }
            var storeMap = _mapper.Map<Store>(createStore);

            if (!_storeRepository.CreateStore(storeMap))
            {
                return StatusCode(StatusCodes.Status500InternalServerError,new Models.Response { Status= StatusCodes.Status500InternalServerError, Message = "Something went Wrong while Creating Store" });
            }
            return Ok(new Models.Response { Status = StatusCodes.Status200OK, Message ="Store Successfully Created"});
        }
        [HttpPut("Store/{storeid:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult UpdateStore(Guid storeId, [FromBody]StoreDto updateStore)
        {
            if (updateStore == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,new Models.Response{Status =StatusCodes.Status400BadRequest, Message ="Object Not found"});
            }
            if (storeId == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Models.Response {Status =StatusCodes.Status400BadRequest, Message ="store id not found"});
            }
            if (!_storeRepository.StoreExists(storeId))
            {
                return StatusCode(StatusCodes.Status404NotFound, new Models.Response{Status=StatusCodes.Status404NotFound,Message ="Store not found"});
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(StatusCodes.Status401Unauthorized);
            }
            var storeMap = _mapper.Map<Store>(updateStore);
            if (!_storeRepository.UpdateStore(storeMap))
            {
               //ModelState.AddModelError("", "Something went wrong while updating store");
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response{Status=StatusCodes.Status500InternalServerError,Message="Something went wrong while updating the store"});
            }
            return Ok(new Models.Response{Status =StatusCodes.Status204NoContent,Message="Store successfully updated"});
        }

        [HttpDelete("{storeId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteStore(Guid storeId)
        {
            if (!_storeRepository.StoreExists(storeId))
            {
                return StatusCode(StatusCodes.Status404NotFound,new Models.Response{Status =StatusCodes.Status404NotFound,Message = "Id not found" });
            }
            var userToDelete = _storeRepository.GetStore(storeId);

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Models.Response{Status= StatusCodes.Status400BadRequest,Message="Invalid Request"});
            }
            if (!_storeRepository.DeleteStore(userToDelete))
            {
                //ModelState.AddModelError("", "Something went wrong while deleting store");
                return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response{Status = StatusCodes.Status500InternalServerError, Message="Something went wrong while deleting store"});
            }
            return NoContent();
        }
    }
}
