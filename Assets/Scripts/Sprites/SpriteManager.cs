using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    private SpriteCatalogue SpriteCatalogue
    {
        get
        {
            // Lazy loading
            if (spriteCatalogue == null)
                spriteCatalogue = SpriteCatalogue.FromResources();
            return spriteCatalogue;
        }
    }
    private SpriteCatalogue spriteCatalogue;

    /// <summary>
    /// Gets a sprite with the given name. Returns null
    /// if there is no sprite with that name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        return SpriteCatalogue.GetSpriteByName(name);
    }
}
