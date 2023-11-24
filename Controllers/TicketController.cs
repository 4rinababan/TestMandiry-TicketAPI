using Microsoft.AspNetCore.Mvc;
using Test_Mandiri.DTO;
using Test_Mandiri.IService;
using Test_Mandiri.Pagination;

namespace Test_Mandiri.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketController(ITicketService ticketService)
        {
            _service = ticketService;
        }

        [HttpGet("GetAll")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<ActionResult<List<Tickets>>> GetAll([FromQuery] PaginationFilter filter)
        {
            try
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.search.ToLower());
                var responseContext = string.IsNullOrEmpty(filter.search)
                    ? await Task.Run(() => _service.GetAll())
                    : await Task.Run(() => _service.FindByContains(filter.search));

                var totalRecords = responseContext.Count;
                var totalPages = (int)Math.Ceiling((double)totalRecords / filter.PageSize);
                var paging = responseContext
                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList();

                return Ok(new PaginationResponse<List<Tickets>>(paging, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords, "success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Tickets> GetById(Guid id)
        {
            var ticket = _service.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost("AddTicket")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Tickets> AddTicket([FromBody] TicketDto request)
        {
            var newTicket = _service.AddTicket(request);
            return CreatedAtAction(nameof(GetById), new { id = newTicket.TicketId }, newTicket);
        }

        [HttpPut("UpdateTicket")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Tickets> UpdateTicket([FromBody] TicketDto request)
        {
            var updatedTicket = _service.UpdateTicket(request);
            if (updatedTicket == null)
            {
                return NotFound();
            }

            return Ok(updatedTicket);
        }

        [HttpDelete("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<bool> RemoveById(Guid id)
        {
            var isRemoved = _service.RemoveById(id);
            if (!isRemoved)
            {
                return NotFound();
            }

            return Ok($"Id : {id} Deleted");
        }
    }
}
