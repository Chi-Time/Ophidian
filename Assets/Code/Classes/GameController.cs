using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _Score = 0;

    private void Awake ()
    {
        EventManager.Instance.AddListener<ScoreIncreased> (OnScoreIncreased);
    }

    private void OnScoreIncreased (ScoreIncreased e)
    {
        _Score += e.Value;
    }

    private void OnDestroy ()
    {
        EventManager.Instance.RemoveListener<ScoreIncreased> (OnScoreIncreased);
    }
}
