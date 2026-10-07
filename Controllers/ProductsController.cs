using Villaalba_Midterm_Store.Data;
using Villaalba_Midterm_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Villaalba_Midterm_Store.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _db;
    public ProductsController(AppDbContext db) => _db = db;

    public async Task<IActionResult> Index() => View(await _db.Products.ToListAsync());

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Product p)
    {
        if (!ModelState.IsValid) return View(p);
        _db.Products.Add(p);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var p = await _db.Products.FindAsync(id);
        return p == null ? NotFound() : View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Product p)
    {
        if (!ModelState.IsValid) return View(p);
        _db.Products.Update(p);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p != null) { _db.Products.Remove(p); await _db.SaveChangesAsync(); }
        return RedirectToAction(nameof(Index));
    }
}