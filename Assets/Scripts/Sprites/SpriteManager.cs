using UnityEngine;

public class SpriteManager : MonoSingleton<SpriteManager>
{
    [SerializeField]
    private SpriteCatalogue[] SpriteCatalogues = null;

    /// <summary>
    /// Gets a sprite with the given name. Returns null
    /// if there is no sprite with that name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        foreach (SpriteCatalogue catalogue in SpriteCatalogues)
        {
            Sprite sprite = catalogue.GetSpriteByName(name);
            if (sprite != null)
                return sprite;
        }
        return null;
    }
}
