using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Core.Dtos;
using SimpleBlog.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic; // Pentru IEnumerable în ActionResult

namespace SimpleBlog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET /api/users/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWithPostsDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserWithPostsDto>> GetUserWithPosts(int id)
        {
            var userDto = await _userService.GetUserWithPostsAsync(id);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto);
        }

        // --- GET /api/users ---
        [HttpGet] // Răspunde la GET /api/users
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResultDto<UserDto>))]
        public async Task<ActionResult<PagedResultDto<UserDto>>> GetUsers(
            [FromQuery] string? searchTerm = null,    // Parametru din query string
            [FromQuery] string? sortBy = null,      // ex: ?sortBy=Username
            [FromQuery] bool isAscending = true,   // ex: &isAscending=false
            [FromQuery] int pageNumber = 1,         // ex: &pageNumber=2
            [FromQuery] int pageSize = 10)          // ex: &pageSize=20
        {
            var pagedResult = await _userService.GetUsersAsync(searchTerm, sortBy, isAscending, pageNumber, pageSize);
            return Ok(pagedResult);
        }

        // --- PATCH /api/users/{id} ---
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Documentația Swagger rămâne
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Nu mai verificăm rezultatul boolean pentru "not found" aici.
            // Dacă user-ul nu e găsit, _userService.UpdateUserAsync va arunca NotFoundException,
            // care va fi prinsă de ErrorHandlingMiddleware.
            await _userService.UpdateUserAsync(id, updateUserDto);

            // Dacă ajungem aici, înseamnă că ori user-ul a fost găsit și actualizat (sau nu au fost modificări),
            // ori o altă excepție (care nu e NotFoundException și nu e prinsă specific în middleware
            // pentru un răspuns diferit) a fost aruncată și va rezulta într-un 500.
            // Presupunând că totul e ok, returnăm 204.
            return NoContent();
        }
    }
}