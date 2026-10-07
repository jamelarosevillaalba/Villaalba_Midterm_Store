using Villaalba_Midterm_Store.Data;
using Villaalba_Midterm_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Villaalba_Midterm_Store.Controllers;

public class CartController : Controller
{
    private readonly AppDbContext _db;
    public CartController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.CartItems.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Add(int productId)
    {
        var product = await _db.Products.FindAsync(productId);
        if (product == null) return NotFound();

        var existing = await _db.CartItems.FirstOrDefaultAsync(c => c.ProductId == productId);
        if (existing != null) existing.Quantity++;
        else _db.CartItems.Add(new CartItem
        {
            ProductId = product.Id,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = 1
        });
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", "Products");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateQuantity(int id, int quantity)
    {
        var item = await _db.CartItems.FindAsync(id);
        if (item != null && quantity >= 1) { item.Quantity = quantity; await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int id)
    {
        var item = await _db.CartItems.FindAsync(id);
        if (item != null) { _db.CartItems.Remove(item); await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}