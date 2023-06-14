using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext db; 

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = db.Dishes.ToList();
        return View("Index", AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        return View("NewDish");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if(!ModelState.IsValid){
            return View("NewDish");
        }

        db.Dishes.Add(newDish);

        db.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet("dishes/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if(dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("Details", dish);
    }

    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult EditDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if(dish == null)
        {
            return RedirectToAction("Index");
        }
        return View("EditDish",dish);
    }

    [HttpPost("dishes/{dishId}/update")]
    public IActionResult UpdateDish(Dish updatedDish,int dishId)
    {
        if(!ModelState.IsValid)
        {
            return EditDish(dishId);
        }

        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if(dish== null)
        {
            return RedirectToAction("Index");
        }

        dish.Name = updatedDish.Name;
        dish.Chef = updatedDish.Chef;
        dish.Tasteiness = updatedDish.Tasteiness;
        dish.Calories = updatedDish.Calories;
        dish.Description = updatedDish.Description;
        dish.UpdatedAt = DateTime.Now;

        db.SaveChanges();
        return RedirectToAction("ViewDish", new {dishId = dish.DishId});
    }

    [HttpPost("dishes/{dishId}/destroy")]
    public IActionResult DeleteDish(int dishId)
    {
        Dish? dish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if(dish != null)
        {
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
