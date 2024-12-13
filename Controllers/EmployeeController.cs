using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS_EMS.Data;
using PS_EMS.Models;

namespace PS_EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext context)
        {
            _context = context;
        }

        // Get: api/Employees/list
        [HttpGet("list")]
        public ActionResult<IEnumerable<Employee>> GetEmployees([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var totalRecords = _context.Employees.Count();

                var employees = _context.Employees
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(new ListingApiResponse<List<Employee>>(employees, pageSize, pageNo, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { ErrorCode = ErrorCode.BAD_REQUEST, Message = ex.Message });
            }
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound(new ErrorResponse { ErrorCode = ErrorCode.RESOURCE_NOT_FOUND, Message = ErrorMessage.RESOURCE_NOT_FOUND });
            }

            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] Employee employee)
        {
            if (!Enum.IsDefined(typeof(Gender), employee.Gender))
            {
                return BadRequest(new ErrorResponse { ErrorCode = ErrorCode.INVALID_INPUT, Message = "Invalid gender value." });
            }

            if (!Enum.IsDefined(typeof(Department), employee.Department))
            {
                return BadRequest(new ErrorResponse { ErrorCode = ErrorCode.INVALID_INPUT, Message = "Invalid department value." });
            }

            _context.Employees.Add(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, new ErrorResponse { ErrorCode = ErrorCode.INTERNAL_SERVER_ERROR, Message = ErrorMessage.INTERNAL_SERVER_ERROR });
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/Employees
        [HttpPut()]
        public async Task<IActionResult> PutEmployee([FromBody] Employee employee)
        {
            if (!Enum.IsDefined(typeof(Gender), employee.Gender))
            {
                return BadRequest(new ErrorResponse { ErrorCode = ErrorCode.INVALID_INPUT, Message = "Invalid gender value." });
            }

            if (!Enum.IsDefined(typeof(Department), employee.Department))
            {
                return BadRequest(new ErrorResponse { ErrorCode = ErrorCode.INVALID_INPUT, Message = "Invalid department value." });
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound(new ErrorResponse { ErrorCode = ErrorCode.RESOURCE_NOT_FOUND, Message = ErrorMessage.RESOURCE_NOT_FOUND });
                }
                else
                {
                    return StatusCode(500, new ErrorResponse { ErrorCode = ErrorCode.INTERNAL_SERVER_ERROR, Message = ErrorMessage.INTERNAL_SERVER_ERROR });
                }
            }

            return Ok(new SuccessResponse { Success = true });
        }

        // DELETE: api/Employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new ErrorResponse { ErrorCode = ErrorCode.RESOURCE_NOT_FOUND, Message = ErrorMessage.RESOURCE_NOT_FOUND });
            }

            _context.Employees.Remove(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, new ErrorResponse { ErrorCode = ErrorCode.INTERNAL_SERVER_ERROR, Message = ErrorMessage.INTERNAL_SERVER_ERROR });
            }

            return Ok(new SuccessResponse { Success = true });
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
