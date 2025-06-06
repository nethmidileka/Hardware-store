using HardwareStore.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HardwareStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public ActionResult<List<InventoryItem>> GetAllItems()
        {
            return Ok(_inventoryService.GetAllItems());
        }

        [HttpGet("{id}")]
        public ActionResult<InventoryItem> GetItemById(int id)
        {
            var item = _inventoryService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<InventoryItem> AddItem(InventoryItem item)
        {
            if (item == null)
            {
                return BadRequest("Item cannot be null.");
            }

            try
            {
                var newItem = _inventoryService.AddItem(item);
                return CreatedAtAction(nameof(GetItemById), new { id = newItem.ProductId }, newItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // Catching generic exception for unexpected errors
            {
                // Log the exception (not implemented here for brevity)
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, InventoryItem itemToUpdate)
        {
            if (itemToUpdate == null)
            {
                return BadRequest("Item to update cannot be null.");
            }

            if (id != itemToUpdate.ProductId)
            {
                return BadRequest("Product ID in the URL must match the Product ID in the request body.");
            }

            try
            {
                var updatedItem = _inventoryService.UpdateItem(itemToUpdate);
                if (updatedItem == null)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentNullException ex) // Should be caught by the initial null check, but good for safety
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // Catching generic exception for unexpected errors
            {
                // Log the exception
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var success = _inventoryService.DeleteItem(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
