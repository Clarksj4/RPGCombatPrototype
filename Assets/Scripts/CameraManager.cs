using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private bool focusOnCurrentActor = true;

    [Header("Components")]
    [SerializeField]
    private new Camera camera = null;
    [SerializeField]
    private Transform translationTransform = null;

    [Header("Movement")]
    [SerializeField] private float movementDuration = 0.5f;
    [SerializeField] private float orthographicSizePunchFactor = 1.2f;
    [SerializeField] private Ease movementEasing = Ease.Unset;
    [SerializeField] private Ease orthographicSizePunchOutEasing = Ease.Unset;
    [SerializeField] private Ease orthographicSizePunchReturnEasing = Ease.Unset;

    private Sequence lookAtSequence = null;

    private float startOrthographicSize = 0f;

    void Awake()
    {
        startOrthographicSize = camera.orthographicSize;
        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
    }

    private void HandleOnTurnStart(ITurnBased obj)
    {
        if (focusOnCurrentActor)
        {
            Pawn actor = obj as Pawn;
            Vector3 actorWorldPosition = actor.WorldPosition;
            Vector3 freezeZPos = new Vector3(actorWorldPosition.x, actorWorldPosition.y, translationTransform.position.z);


            lookAtSequence = DOTween.Sequence();
            lookAtSequence.Append(camera.DOOrthoSize(camera.orthographicSize * orthographicSizePunchFactor, movementDuration / 2).SetEase(orthographicSizePunchOutEasing));
            lookAtSequence.Insert(movementDuration / 2f, camera.DOOrthoSize(startOrthographicSize, movementDuration / 2).SetEase(orthographicSizePunchReturnEasing));
            lookAtSequence.Insert(0f, translationTransform.DOMove(freezeZPos, movementDuration).SetEase(movementEasing));
            lookAtSequence.Play();
        }
    }
}
