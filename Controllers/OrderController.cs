using Microsoft.AspNetCore.Mvc;
using Test_Mandiri.DTO;
using Test_Mandiri.IService;
using Test_Mandiri.Pagination;

namespace Test_Mandiri.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService orderService)
        {
            _service = orderService;
        }

        [HttpGet("GetAll")]
        [ApiExplorerSettings(GroupName = "v1")]
        public async Task<ActionResult<List<Orders>>> GetAll([FromQuery] PaginationFilter filter)
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

                return Ok(new PaginationResponse<List<Orders>>(paging, validFilter.PageNumber, validFilter.PageSize, totalPages, totalRecords, "success"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Orders> GetById(Guid id)
        {
            var order = _service.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("AddOrder")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Orders> AddOrder([FromBody] OrderDto request)
        {
            var newOrder = _service.AddOrder(request);
            return CreatedAtAction(nameof(GetById), new { id = newOrder.OrderId }, newOrder);
        }

        [HttpPut("UpdateOrder")]
        [ApiExplorerSettings(GroupName = "v1")]
        public ActionResult<Orders> UpdateOrder([FromBody] OrderDto request)
        {
            var updatedOrder = _service.UpdateOrders(request);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return Ok(updatedOrder);
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

            return Ok(isRemoved);
        }
    }
}
