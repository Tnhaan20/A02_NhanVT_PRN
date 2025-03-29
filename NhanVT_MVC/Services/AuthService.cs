using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace NhanVT_MVC.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _jsRuntime;

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
                var isAdmin = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "isAdmin");
                return isAdmin == "true";
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
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "email");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "roleId");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "accountId");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "isAdmin");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Logout: {ex.Message}");
            }
        }
    }
}