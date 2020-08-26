using System;
using System.Collections.Generic;
using System.Linq;

public class MenuStack : Singleton<MenuStack>
{
    private Stack<Menu> menus = new Stack<Menu>();

    private HashSet<Menu> registry = new HashSet<Menu>();

    public void RegisterMenu(Menu menu) 
    {
        registry.Add(menu);
    }

    public void Show<TMenu>() 
        where TMenu : Menu
    {
        // Fine menu of type
        Menu menu = registry.FirstOrDefault(m => m is TMenu);

        if (menu != null)
            Show(menu);
    }

    public void Show(string name)
    {
        // Find menu by name
        Menu menu = registry.FirstOrDefault(m => m.name == name);

        if (menu != null)
            Show(menu);
    }

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

    public void CloseCurrent()
    {
        // Hide current menu
        if (menus.Count > 0)
            menus.Pop()?.Hide();
        
        // Show next menu in stack
        if (menus.Count > 0)
            menus.Peek()?.Show();
    }

    public void CloseAll()
    {
        // Close all the menus!
        while (menus.Count > 0)
            CloseCurrent();
    }
}
