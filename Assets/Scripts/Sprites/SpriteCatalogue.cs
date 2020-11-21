using System.Linq;
using UnityEngine;

/// <summary>
/// Class for holding a reference to a collection of sprites.
/// </summary>
public class SpriteCatalogue
{
    /// <summary>
    /// Just a big ol array of sprites.
    /// </summary>
    private Sprite[] sprites;

    /// <summary>
    /// Creates a sprite catalogue containing all of
    /// the sprites in the Resources folder.
    /// </summary>
    public static SpriteCatalogue FromResources()
    {
        SpriteCatalogue catalogue = new SpriteCatalogue();
        catalogue.sprites = Resources.LoadAll<Sprite>("Sprites");
        return catalogue;
    }

    /// <summary>
    /// Gets a sprite with the given name. Returns null
    /// if there is no sprite with that name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        return sprites.FirstOrDefault(s => s.name == name);
    }
}
