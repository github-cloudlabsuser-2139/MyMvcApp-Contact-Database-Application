using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    // Lista estática para almacenar usuarios en memoria
    public static List<User> userlist = new List<User>
    {
        new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" }
    };

    // GET: User
    public ActionResult Index()
    {
        // Devuelve la vista con la lista de usuarios
        return View(userlist);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        // Devuelve la vista para crear un nuevo usuario
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // Genera un nuevo ID para el usuario
                user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1;

                // Agrega el nuevo usuario a la lista
                userlist.Add(user);

                // Redirige al índice después de guardar
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, vuelve a mostrar la vista con los datos ingresados
            return View(user);
        }
        catch
        {
            // Maneja cualquier excepción y vuelve a mostrar la vista
            return View(user);
        }
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                // Busca el usuario existente por ID
                var existingUser = userlist.FirstOrDefault(u => u.Id == id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Actualiza los datos del usuario
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;

                // Redirige al índice después de guardar
                return RedirectToAction("Index");
            }

            // Si el modelo no es válido, vuelve a mostrar la vista con los datos ingresados
            return View(user);
        }
        catch
        {
            // Maneja cualquier excepción y vuelve a mostrar la vista
            return View(user);
        }
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // Busca el usuario por ID
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            // Busca y elimina el usuario por ID
            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                userlist.Remove(user);
            }

            // Redirige al índice después de eliminar
            return RedirectToAction("Index");
        }
        catch
        {
            // Maneja cualquier excepción y vuelve a mostrar la vista
            return View();
        }
    }
}
