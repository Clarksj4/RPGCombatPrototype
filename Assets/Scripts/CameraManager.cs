using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Transform translationTransform;

    [Header("Movement")]
    [SerializeField] private float movementDuration = 0.5f;
    [SerializeField] private float orthographicSizePunchFactor = 1.2f;
    [SerializeField] private Ease movementEasing;
    [SerializeField] private Ease orthographicSizePunchOutEasing;
    [SerializeField] private Ease orthographicSizePunchReturnEasing;

    private Sequence lookAtSequence;

    private float startOrthographicSize;

    void Awake()
    {
        startOrthographicSize = camera.orthographicSize;
        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
    }

    private void HandleOnTurnStart(ITurnBased obj)
    {
        //Actor actor = obj as Actor;
        //Vector3 actorWorldPosition = actor.WorldPosition;
        //Vector3 freezeZPos = new Vector3(actorWorldPosition.x, actorWorldPosition.y, translationTransform.position.z);


        //lookAtSequence = DOTween.Sequence();
        //lookAtSequence.Append(camera.DOOrthoSize(camera.orthographicSize * orthographicSizePunchFactor, movementDuration / 2).SetEase(orthographicSizePunchOutEasing));
        //lookAtSequence.Insert(movementDuration / 2f, camera.DOOrthoSize(startOrthographicSize, movementDuration / 2).SetEase(orthographicSizePunchReturnEasing));
        //lookAtSequence.Insert(0f, translationTransform.DOMove(freezeZPos, movementDuration).SetEase(movementEasing));
        //lookAtSequence.Play();
    }
}
