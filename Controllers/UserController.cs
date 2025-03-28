using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static List<User> userlist = new List<User>();

    // GET: User
    public ActionResult Index()
    {
        // Return the list of users to the Index view
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Return 404 if user not found
        }
        return View(user); // Pass the user to the Details view
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Return the Create view
        return View();
    }

    // POST: User/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            // Add the new user to the list
            userlist.Add(user);
            return RedirectToAction(nameof(Index)); // Redirect to Index after creation
        }
        return View(user); // Return the view with validation errors
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Return 404 if user not found
        }
        return View(user); // Pass the user to the Edit view
    }

    // POST: User/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, User updatedUser)
    {
        if (ModelState.IsValid)
        {
            // Find the user by ID
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(); // Return 404 if user not found
            }

            // Update user properties
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;

            return RedirectToAction(nameof(Index)); // Redirect to Index after editing
        }
        return View(updatedUser); // Return the view with validation errors
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Return 404 if user not found
        }
        return View(user); // Pass the user to the Delete view
    }

    // POST: User/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        // Find the user by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            userlist.Remove(user); // Remove the user from the list
        }
        return RedirectToAction(nameof(Index)); // Redirect to Index after deletion
    }
}