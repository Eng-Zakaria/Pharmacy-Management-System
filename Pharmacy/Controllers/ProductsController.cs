using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        [Route("[controller]/List")]
        [HttpGet]

        // GET: Products
        public async Task<IActionResult> Index(string search,
            string? sortType, string? sortOrder, int pageSize = 10,
            int pageNumber = 1)
        {

            IQueryable<Product> products = _context.Products.Include(p =>p.Category).AsQueryable();  //sol3

            if (string.IsNullOrWhiteSpace(search) == false)
            {
                search = search.Trim();
                products = _context.Products.Where(p => p.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(sortType)
                && !string.IsNullOrWhiteSpace(sortOrder))
            {
                if (sortType == "Name")
                {
                    if (sortOrder == "asc")
                        products = products.OrderBy(p => p.Name);
                    else if (sortOrder == "desc")
                        products = products.OrderByDescending(p => p.Name);
                }
                if (sortType == "Category")
                {
                    if (sortOrder == "asc")
                        products = products.OrderBy(p => p.Category);
                    else if (sortOrder == "desc")
                        products = products.OrderByDescending(p => p.Category);
                }
                if (sortType == "ExpiryDate")
                {
                    if (sortOrder == "asc")
                        products = products.OrderBy(p => p.ExpiryDate);
                    else if (sortOrder == "desc")
                        products = products.OrderByDescending(p => p.ExpiryDate);
                }

                if (sortType == "Price")
                {
                    if (sortOrder == "asc")
                        products = products.OrderBy(p => p.Price);
                    else if (sortOrder == "desc")
                        products = products.OrderByDescending(p => p.Price);
                }


            }

            products = products.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            ViewBag.CurrentSearch = search;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;



            //products = _context.Products.Include(p => p.Category);    //  sol2
            //var applicationDbContext = _context.Products.Include(p => p.Category);  // sol1
            return View(await products.ToListAsync());
        }

        


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,ProductNoItems,AddedAt,ActiveSubstance,Dose,CompanyName,ExpiryDate,Description,Price,ImageUrl")] Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile == null)
                {
                    product.ImageUrl = "\\images\\No_Image.png";
                }
                else
                {
                    string imgExtension = Path.GetExtension(imageFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgUrl = "\\images\\" + imgName;

                    product.ImageUrl = imgUrl;
                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imageFile.CopyTo(imgStream);
                    imgStream.Dispose();

                }
                product.AddedAt = DateTime.Now;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId,ProductNoItems,AddedAt,ActiveSubstance,Dose,CompanyName,ExpiryDate,Description,Price,ImageUrl")] Product product, IFormFile? imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    if (product.ImageUrl != "\\images\\No_Image.png")
                    {
                        string oldPath = _webHostEnvironment.WebRootPath + product.ImageUrl;
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }

                        string imgExtension = Path.GetExtension(imageFile.FileName);
                        Guid imgGuid = Guid.NewGuid();
                        string imgName = imgGuid + imgExtension;
                        string imgUrl = "\\images" + imgName;

                        product.ImageUrl = imgUrl;
                        string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                        FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                        imageFile.CopyTo(imgStream);
                        imgStream.Dispose();

                    }
                }
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                if (product.ImageUrl != "\\images\\No_Image.png")
                {
                    string imgPath = _webHostEnvironment.WebRootPath + product.ImageUrl;
                    if (System.IO.File.Exists(imgPath))
                    {
                        System.IO.File.Delete(imgPath);
                    }

                }
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.Id == id);
        }
    }
}
