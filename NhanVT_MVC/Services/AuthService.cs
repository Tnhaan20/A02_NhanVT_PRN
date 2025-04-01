using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace NhanVT_MVC.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string ADMIN_EMAIL = "admin@FUNewsManagementSystem.org";

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // Check if we're running on server prerendering
        private bool IsPrerendering()
        {
            try
            {
                // This will throw during prerendering
                _jsRuntime.InvokeAsync<bool>("eval", "true");
                return false;
            }
            catch
            {
                return true;
            }
        }

        public async Task<bool> IsAuthenticated()
        {
            if (IsPrerendering()) return false;
            
            try
            {
                var email = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "email");
                return !string.IsNullOrEmpty(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsAuthenticated: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsAdmin()
        {
            if (IsPrerendering()) return false;
            
            try
            {
                // Get current user email - this is the most important identifier
                var email = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "email");
                
                // If no email, user isn't logged in at all
                if (string.IsNullOrEmpty(email))
                {
                    Console.WriteLine("Admin check: No user logged in");
                    return false;
                }
                
                // Primary check: Is the current email the admin email?
                if (email == ADMIN_EMAIL)
                {
                    Console.WriteLine("Admin check: Admin email confirmed");
                    return true;
                }
                
                // Not the admin email, definitely not an admin regardless of isAdmin flag
                Console.WriteLine($"Admin check: User email {email} is not admin email");
                
                // Clear any incorrect isAdmin flag for non-admin emails
                var isAdminFlag = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "isAdmin");
                if (isAdminFlag == "true")
                {
                    // This is the bug - non-admin has admin flag set to true
                    Console.WriteLine("Bug detected: Non-admin user has admin flag set to true. Clearing...");
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAdmin", "false");
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in IsAdmin: {ex.Message}");
                return false;
            }
        }

        public async Task<int?> GetUserRole()
        {
            if (IsPrerendering()) return null;
            
            try
            {
                var roleStr = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "roleId");
                if (int.TryParse(roleStr, out int role))
                    return role;
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserRole: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetUserEmail()
        {
            if (IsPrerendering()) return string.Empty;
            
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "email") ?? string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserEmail: {ex.Message}");
                return string.Empty;
            }
        }

        public async Task<int?> GetUserId()
        {
            if (IsPrerendering()) return null;
            
            try
            {
                var idStr = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "accountId");
                if (int.TryParse(idStr, out int id))
                    return id;
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserId: {ex.Message}");
                return null;
            }
        }

        public async Task Logout()
        {
            if (IsPrerendering()) return;
            
            try
            {
                // Clear all user data from localStorage
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "email");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "roleId");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accountId");
                
                // For the isAdmin flag, first remove it then explicitly set to false
                // This double approach ensures it's definitely cleared
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "isAdmin");
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAdmin", "false");
                
                Console.WriteLine("Logout completed: All authentication data cleared");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Logout: {ex.Message}");
            }
        }
    }
}