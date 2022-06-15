using Dishes.Models;
using Microsoft.AspNetCore.Mvc;

public class DishesController : Controller
{
  private MyContext _context;

  public DishesController(MyContext context)
  {
    _context = context;
  }

  [HttpGet("/dishes/new")]
  public IActionResult New()
  {
    return View("New");
  }

  [HttpPost("/dishes/create")]
  public IActionResult Create(Dish newDish)
  {
    if (ModelState.IsValid == false)
    {
      return New();
    }

    _context.Dishes.Add(newDish);
    // _context doesn't update until we run SaveChanges
    // after SaveChanges, then our newDish will have a DishId
    _context.SaveChanges();

    return RedirectToAction("All");
  }

  [HttpGet("/dishes/detail/{dishID}")]
  public IActionResult Detail(int dishID)
  {
    Dish? dishDetails = _context.Dishes.FirstOrDefault(d => d.DishId == dishID);

    if (dishDetails == null)
    {
      return RedirectToAction("All");
    }

    return View(dishDetails);
  }

  [HttpPost("/dishes/delete/{dishID}")]
  public IActionResult Delete(int dishID)
  {
    Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishID);

    if (dish != null)
    {
      _context.Dishes.Remove(dish);
      _context.SaveChanges();
    }

    return RedirectToAction("All");
  }

  [HttpGet("/dishes/edit/{dishID}")]
  public IActionResult Edit(int dishID)
  {
    Dish? dish = _context.Dishes.FirstOrDefault(d => d.DishId == dishID);

    if (dish != null)
    {
      return View("Edit", dish);
    }

    return RedirectToAction("All");
  }

  [HttpPost("/dishes/update/{dishID}")]
  public IActionResult Update(Dish editedDish, int dishID)
  {
    if (ModelState.IsValid)
    {
      Dish? updateDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishID);

      if (updateDish != null)
      {
        updateDish.ChefName = editedDish.ChefName;
        updateDish.DishName = editedDish.DishName; 
        updateDish.Calories = editedDish.Calories; 
        updateDish.Tastiness = editedDish.Tastiness;
        updateDish.Description = editedDish.Description;
        updateDish.UpdatedAt = DateTime.Now;

        _context.Dishes.Update(updateDish);
        _context.SaveChanges();
      }

      return RedirectToAction("Detail", updateDish);
    }
    else
    {
      return Edit(dishID);
    }
  }

  [HttpGet("/dishes/all")]
  public IActionResult All()
  {
    List<Dish> allDishes = _context.Dishes.ToList();

    IEnumerable<Dish> dishesSorted = allDishes.OrderByDescending(d => d.UpdatedAt);

    return View("All", dishesSorted);
  }
}