using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;

    private Sequence lookAtSequence;

    void Awake()
    {
        TurnManager.Instance.OnTurnStart += HandleOnTurnStart;
    }

    private void HandleOnTurnStart(ITurnBased obj)
    {
        Actor actor = obj as Actor;
        Vector3 actorWorldPosition = actor.WorldPosition;
        Vector3 freezeZPos = new Vector3(actorWorldPosition.x, actorWorldPosition.y, transform.position.z);

        lookAtSequence = DOTween.Sequence();
        lookAtSequence.Append(transform.DOMove(freezeZPos, 0.5f).SetEase(Ease.OutQuad));
        lookAtSequence.Play();
    }
}
