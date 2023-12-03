using System;
using UnityEngine;

namespace LethalAPI.UI.Menus;

public abstract class LethalCompanyMenu : IDisposable
{
    public abstract GameObject Root { get; }
    
    public delegate void OnMenuOpenHandle(LethalCompanyMenu menu);
    public static OnMenuOpenHandle OnMenuOpen;
    
    public delegate void OnMenuCloseHandle(LethalCompanyMenu menu);
    public static OnMenuCloseHandle OnMenuClose;
    
    public abstract void Dispose();

    public virtual void Open()
    {
        if (Root == null) return;
        
        Root.SetActive(true);
        OnMenuOpen?.Invoke(this);
    }

    public virtual void Close()
    {
        if (Root == null) return;

        Root.SetActive(false);
        OnMenuClose?.Invoke(this);
    }
}