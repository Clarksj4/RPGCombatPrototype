using System;
using System.Collections.Generic;
using System.Linq;

public class MenuStack : Singleton<MenuStack>
{
    private Stack<Menu> menus = new Stack<Menu>();

    private HashSet<Menu> registry = new HashSet<Menu>();

    /// <summary>
    /// Registers the given menu so that it can be
    /// found by type or name.
    /// </summary>
    public void RegisterMenu(Menu menu) 
    {
        registry.Add(menu);
    }

    /// <summary>
    /// Shows the menu with the given type, hiding any
    /// currently visible menu.
    /// </summary>
    public void Show<TMenu>() 
        where TMenu : Menu
    {
        // Fine menu of type
        Menu menu = registry.FirstOrDefault(m => m is TMenu);

        if (menu != null)
            Show(menu);
    }

    /// <summary>
    /// Shows the menu with the given name, hiding any
    /// currently visible menu.
    /// </summary>
    public void Show(string name)
    {
        // Find menu by name
        Menu menu = registry.FirstOrDefault(m => m.name == name);

        if (menu != null)
            Show(menu);
    }

    /// <summary>
    /// Shows the given menu, hiding any currently visible
    /// menu
    /// </summary>
    public void Show(Menu menu)
    {
        if (menu == null)
            throw new ArgumentException("Tried to show a null menu");

        if (menus.Contains(menu))
            throw new ArgumentException("Menu is already showing");

        // Hide current Menu (but leave it in the stack)
        if (menus.Count > 0)
            menus.Peek()?.Hide();

        // Add and show new menu
        menus.Push(menu);
        menu.Show();
    }

    /// <summary>
    /// Hides the currently visible menu and show the next
    /// menu in the stack.
    /// </summary>
    public void Hide()
    {
        HideCurrent();
        ShowNext();
    }

    public void HideAll()
    {
        // Hide current then just dismiss the rest because
        // they're already hidden.
        HideCurrent();
        menus.Clear();
    }

    private void HideCurrent()
    {
        // Hide current menu
        if (menus.Count > 0)
            menus.Pop()?.Hide();
    }

    private void ShowNext()
    {
        // Show next menu in stack
        if (menus.Count > 0)
            menus.Peek()?.Show();
    }
}
