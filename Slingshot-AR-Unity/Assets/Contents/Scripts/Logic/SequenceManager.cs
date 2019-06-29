using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{

    public enum Sequences
    {
        None,
        Title,
        Menu,
        Tutorial,
        Single,
        Host,
        Remote,
        Spectator,
        Result
    }

    [SerializeField]
    GameObject titlePrefab;

    [SerializeField]
    GameObject menuPrefab;

    [SerializeField]
    GameObject singlePrefab;

    [SerializeField]
    GameObject hostPrefab;

    [SerializeField]
    GameObject remotePrefab;

    [SerializeField]
    GameObject spectatorPrefab;

    [SerializeField]
    GameObject resultPrefab;

    public Sequences nowSequecne { get; private set; }

    private BaseSequence _sequence { get; set; }

    private GameObject sequencePrefab;


    #region MonoBehavior
    private void Start()
    {
        InitManager();
    }

    private void Update()
    {
        
    }
    #endregion

    #region public methods
    public void InitManager()
    {
        nowSequecne = Sequences.None;
        _sequence = null;
    }

    public void StartSequence()
    {
        switch (nowSequecne)
        {
            case Sequences.None:
                _sequence = new TitleSequence();
                sequencePrefab = Instantiate(titlePrefab, Vector3.zero, Quaternion.identity);
                break;
            case Sequences.Title:
                break;
            case Sequences.Menu:
                break;
            case Sequences.Tutorial:
                break;
            case Sequences.Single:
                break;
            case Sequences.Host:
                break;
            case Sequences.Remote:
                break;
            case Sequences.Spectator:
                break;
            case Sequences.Result:
                break;
            default:
                break;
        }
        if (_sequence != null)
        {
            Debug.Log("Start Sequence: " + _sequence.ToString());
            _sequence.StartSequence(FinishSequence, sequencePrefab);
        }
    }
    #endregion

    #region private methods
    /// <summary>
    /// After finish sequence, check next or not.
    /// </summary>
    void FinishSequence()
    {
        switch (nowSequecne)
        {
            case Sequences.None:
                nowSequecne = Sequences.Title;
                break;
            case Sequences.Title:
                nowSequecne = Sequences.Menu;
                Destroy(sequencePrefab);
                break;
            case Sequences.Menu:
                break;
            case Sequences.Tutorial:
                break;
            case Sequences.Single:
                break;
            case Sequences.Host:
                break;
            case Sequences.Remote:
                break;
            case Sequences.Spectator:
                break;
            case Sequences.Result:
                break;
        }

        StartSequence();


    }

    #endregion
}
