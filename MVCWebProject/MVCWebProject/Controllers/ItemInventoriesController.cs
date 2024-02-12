using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebProject.Models.DB;

namespace MVCWebProject.Controllers
{
    public class ItemInventoriesController : Controller
    {
        private readonly CC21_Team4_Sem1Context _context;

        public ItemInventoriesController(CC21_Team4_Sem1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> ItemSearch(string itemName)
        {

            string strSQL = "SELECT * FROM ItemInventories WHERE ItemName LIKE '" + itemName + "%'";
            var search = _context.ItemInventories.FromSqlRaw(strSQL).ToList();

            return View(search);

        }
        public async Task<IActionResult> CategoryFilter(string category)
        {

            string strSQL = "SELECT * FROM ItemInventories WHERE Category LIKE '" + category + "%'";
            var filter = _context.ItemInventories.FromSqlRaw(strSQL).ToList();

            return View(filter);

        }


        // GET: ItemInventories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemInventories.ToListAsync());
        }

        // GET: ItemInventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventory = await _context.ItemInventories
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (itemInventory == null)
            {
                return NotFound();
            }

            return View(itemInventory);
        }

        // GET: ItemInventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemID,ItemName,Category,Quantity,Price")] ItemInventory itemInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemInventory);
        }

        // GET: ItemInventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventory = await _context.ItemInventories.FindAsync(id);
            if (itemInventory == null)
            {
                return NotFound();
            }
            return View(itemInventory);
        }

        // POST: ItemInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemID,ItemName,Category,Quantity,Price")] ItemInventory itemInventory)
        {
            if (id != itemInventory.ItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemInventoryExists(itemInventory.ItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemInventory);
        }

        // GET: ItemInventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemInventory = await _context.ItemInventories
                .FirstOrDefaultAsync(m => m.ItemID == id);
            if (itemInventory == null)
            {
                return NotFound();
            }

            return View(itemInventory);
        }

        // POST: ItemInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemInventory = await _context.ItemInventories.FindAsync(id);
            _context.ItemInventories.Remove(itemInventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemInventoryExists(int id)
        {
            return _context.ItemInventories.Any(e => e.ItemID == id);
        }
    }
}
