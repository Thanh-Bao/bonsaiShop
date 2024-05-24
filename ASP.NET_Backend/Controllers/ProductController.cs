using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var username = User.FindFirst("username")?.Value;

        if (string.IsNullOrEmpty(username))
        {
            return Forbid();
        }

        return await _context.Product
                             .Where(p => p.CreatedBy.username == username)
                             .ToListAsync();
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(string id)
    {
        var username = User.FindFirst("username")?.Value;

        if (string.IsNullOrEmpty(username))
        {
            return Forbid();
        }

        var product = await _context.Product
                                    .Where(p => p.Id == id && p.CreatedBy.username == username)
                                    .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
public async Task<ActionResult<Product>> PostProduct(Product product)
{
    // Lấy username từ JWT token
    var username = User.FindFirst("username")?.Value;

        Console.WriteLine("HUHUHUHUUHUH" + username);

    // Kiểm tra xem username có hợp lệ không
    if (string.IsNullOrEmpty(username))
    {
        return Forbid();
    }

    // Tìm kiếm người dùng dựa trên username
    var user = await _context.User.FirstOrDefaultAsync(u => u.username == username);

    // Nếu không tìm thấy người dùng, trả về lỗi
    if (user == null)
    {
        return Forbid();
    }

    // Gán giá trị cho CreatedById và CreatedBy
    product.CreatedById = user.username;
    product.CreatedBy = user;

    // Kiểm tra validation cho description
    if (string.IsNullOrWhiteSpace(product.description) || product.description.Length < 255 || product.description.Length > 1024)
    {
        return BadRequest(new { errors = new { Description = new[] { "Description must be between 255 and 1024 characters." } } });
    }

    // Thêm product vào database
    _context.Product.Add(product);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
}

 

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var username = User.FindFirst("username")?.Value;

        if (string.IsNullOrEmpty(username))
        {
            return Forbid();
        }

        var product = await _context.Product
                                    .Where(p => p.Id == id && p.CreatedBy.username == username)
                                    .FirstOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(string id)
    {
        return _context.Product.Any(e => e.Id == id);
    }
}
