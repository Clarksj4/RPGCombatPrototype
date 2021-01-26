using UnityEngine;
using UnityEngine.UI;

public class StatusFrame : MonoBehaviour
{
    /// <summary>
    /// Gets the number of instances of this status currently
    /// applied to the pawn.
    /// </summary>
    public int StatusCount { get; private set; }
    /// <summary>
    /// Gets the name of the status applied to the pawn.
    /// </summary>
    public string StatusName { get; private set; }

    [SerializeField]
    private Image image;

    public void SetStatus(string statusName)
    {
        StatusName = statusName;
        StatusCount = 1;
        image.sprite = SpriteManager.Instance.GetSpriteByName(statusName);
        
        // Turn on and show as first status.
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public void IncrementStatusCount()
    {
        StatusCount++;
    }

    public void DecrementStatusCount()
    {
        StatusCount--;

        if (StatusCount == 0)
        {
            StatusName = null;
            image.sprite = null;
            gameObject.SetActive(false);
        }
    }
}
