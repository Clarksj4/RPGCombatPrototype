using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonImage : MonoBehaviour
{
    [SerializeField]
    private ActionButton actionButton = null;
    [Header("Action Image")]
    [SerializeField]
    private Image actionImage = null;
    [SerializeField]
    private Image greyscaleImage = null;

    public void OnActionButtonRefresh()
    {
        actionImage.sprite = actionButton.Action.Sprite;
        greyscaleImage.sprite = actionButton.Action.Sprite;
        greyscaleImage.gameObject.SetActive(!actionButton.Action.CanDo());
    }
}
