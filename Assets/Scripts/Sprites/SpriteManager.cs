using UnityEngine;

public class SpriteManager : MonoSingleton<SpriteManager>
{
    [SerializeField]
    private SpriteCatalogue SpriteCatalogue;

    /// <summary>
    /// Gets a sprite with the given name. Returns null
    /// if there is no sprite with that name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        return SpriteCatalogue.GetSpriteByName(name);
    }
}
