using System.Linq;
using UnityEngine;

/// <summary>
/// Class for holding a reference to a collection of sprites.
/// </summary>
[CreateAssetMenu(fileName = "Sprite Catalogue", menuName = "Sprite Catalogue")]
public class SpriteCatalogue : ScriptableObject
{
    /// <summary>
    /// Just a big ol array of sprites.
    /// </summary>
    [SerializeField]
    private SpriteRef[] sprites;

    /// <summary>
    /// Gets a sprite with the given name. Returns null
    /// if there is no sprite with that name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        SpriteRef spriteRef = sprites.FirstOrDefault(s => s != null && s.Name == name);
        if (spriteRef != null)
            return spriteRef.Sprite;
        return null;
    }
}
