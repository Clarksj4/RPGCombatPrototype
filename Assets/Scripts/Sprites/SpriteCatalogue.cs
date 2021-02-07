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
    private Sprite[] sprites;

    /// <summary>
    /// Gets a sprite with the given name. Returns null
    /// if there is no sprite with that name.
    /// </summary>
    public Sprite GetSpriteByName(string name)
    {
        return sprites.FirstOrDefault(s => s.name == name);
    }
}
