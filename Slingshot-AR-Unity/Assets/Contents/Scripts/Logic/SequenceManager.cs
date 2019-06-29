using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{

    public enum Sequences
    {
        None,
        Title,
        Pairing,
        Menu,
        Tutorial,
        Single,
        Host,
        Remote,
        Spectator,
        Game,
        Result
    }

    [SerializeField]
    GameObject titlePrefab;

    [SerializeField]
    GameObject pairingPrefab;
    
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
    GameObject gamePrefab;


    [SerializeField]
    GameObject resultPrefab;

    public Sequences nowSequecne { get; private set; }

    private BaseSequence _sequence { get; set; }

    private GameObject sequencePrefab;


    #region MonoBehavior
    private void Start()
    {

    }

    private void Update()
    {
        
    }
    #endregion

    #region public methods
    public void InitManager()
    {
        _sequence = null;
        nowSequecne = Sequences.Title;
    }

    public void StartSequence()
    {
        switch (nowSequecne)
        {
            case Sequences.None:
                break;
            case Sequences.Title:
                _sequence = new TitleSequence();
                sequencePrefab = Instantiate(titlePrefab, Vector3.zero, Quaternion.identity);
                break;
            case Sequences.Pairing:
                _sequence = new PairingSequence();
                sequencePrefab = Instantiate(pairingPrefab, Vector3.zero, Quaternion.identity);
                break;

            case Sequences.Menu:
                _sequence = new MenuSequence();
                sequencePrefab = Instantiate(menuPrefab, Vector3.zero, Quaternion.identity);
                break;
            case Sequences.Tutorial:
                break;
            case Sequences.Single:
                break;
            case Sequences.Host:
                _sequence = new HostSequence();
                sequencePrefab = Instantiate(hostPrefab, Vector3.zero, Quaternion.identity);
                break;
            case Sequences.Remote:
                _sequence = new RemoteSequence();
                sequencePrefab = Instantiate(remotePrefab, Vector3.zero, Quaternion.identity);
                break;
            case Sequences.Spectator:
                break;

            case Sequences.Game:
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
                break;
            case Sequences.Title:
                nowSequecne = Sequences.Pairing;
                Destroy(sequencePrefab);
                break;

            case Sequences.Pairing:
                nowSequecne = Sequences.Menu;
                Destroy(sequencePrefab);
                break;

            case Sequences.Menu:
                switch (GameManager.Instance._mode)
                {
                    case GameManager.Mode.None:
                        break;
                    case GameManager.Mode.Single:
                        nowSequecne = Sequences.Single;
                        break;
                    case GameManager.Mode.Host:
                        nowSequecne = Sequences.Host;
                        break;
                    case GameManager.Mode.Remote:
                        nowSequecne = Sequences.Remote;
                        break;
                }
                Destroy(sequencePrefab);
                break;
            case Sequences.Tutorial:
                break;
            case Sequences.Single:
            case Sequences.Host:
            case Sequences.Remote:
            case Sequences.Spectator:
                nowSequecne = Sequences.Game;
                break;

            case Sequences.Game:
                nowSequecne = Sequences.Result;
                break;

            case Sequences.Result:
                nowSequecne = Sequences.Title;
                break;
        }

        StartSequence();


    }

    #endregion
}
