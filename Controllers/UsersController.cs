using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // Mock data (in-memory list)
        private static List<User> users = new List<User>
{
    new User
    {
        Id = 1,
        FirstName = "Alice",
        LastName = "Nguyen",
        Email = "alice.nguyen@company.com",
        PhoneNumber = "123456789",
        Department = "HR",
        Role = "Manager"
    },
    new User
    {
        Id = 2,
        FirstName = "Bob",
        LastName = "Tran",
        Email = "bob.tran@company.com",
        PhoneNumber = "987654321",
        Department = "IT",
        Role = "Developer"
    },
    new User
    {
        Id = 3,
        FirstName = "Charlie",
        LastName = "Pham",
        Email = "charlie.pham@company.com",
        PhoneNumber = "555123456",
        Department = "IT",
        Role = "Support Engineer"
    },
    new User
    {
        Id = 4,
        FirstName = "Diana",
        LastName = "Le",
        Email = "diana.le@company.com",
        PhoneNumber = "444555666",
        Department = "Finance",
        Role = "Accountant"
    },
    new User
    {
        Id = 5,
        FirstName = "Ethan",
        LastName = "Vo",
        Email = "ethan.vo@company.com",
        PhoneNumber = "222333444",
        Department = "Marketing",
        Role = "Coordinator"
    }
};

        // GET: Retrieve all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await Task.FromResult(users);
        }

        // GET: Retrieve a specific user by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return await Task.FromResult(user);
        }

        // POST: Add a new user
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Find the last ID in the list, or start at 1 if empty
            int nextId = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            user.Id = nextId;

            if (users.Any(u => u.Email == user.Email))
                return Conflict("A user with this email already exists.");

            users.Add(user);
            return await Task.FromResult(
                CreatedAtAction(nameof(GetById), new { id = user.Id }, user)
            );
        }

        // PUT: Update an existing user's details
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            // Prevent duplicate email conflicts
            if (users.Any(u => u.Email == updatedUser.Email && u.Id != id))
                return Conflict("Another user with this email already exists.");

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Department = updatedUser.Department;
            user.Role = updatedUser.Role;

            return await Task.FromResult(NoContent());
        }

        // DELETE: Remove a user by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            users.Remove(user);
            return await Task.FromResult(NoContent());
        }
    }
}
