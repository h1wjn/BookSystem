using System;
using System.Threading.Tasks;
using System.Threading;
using BooksSystem.Models;
using BooksSystem.Data;

public class AuthenticationService
{
    private readonly SynchronizationContext _syncContext;
    public bool IsLoggedIn { get; private set; } = false;

    public Customer _currentCustomer;
    public Customer CurrentCustomer => _currentCustomer;
    public event Action OnChange;
    public BooksContext _dbContext;

    public AuthenticationService()
    {
        _syncContext = SynchronizationContext.Current;
    }

    public void NotifyStateChanged()
    {
        _syncContext?.Post(_ => OnChange?.Invoke(), null);
    }

    public async Task LoginAsync(Customer customer)
    {
        _currentCustomer = customer;
        await Task.Delay(10); 
        IsLoggedIn = true;
    }

    public void Login()
    {
        // 设置登录状态并通知组件更新
        IsLoggedIn = true;
        NotifyStateChanged();
    }

    public void Logout()
    {
        // 重置登录状态并通知组件更新
        IsLoggedIn = false;
        NotifyStateChanged();
    }
}
