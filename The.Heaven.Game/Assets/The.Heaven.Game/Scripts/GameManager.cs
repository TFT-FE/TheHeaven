using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public enum RockPaperScissors { Rock, Paper, Scissors, None };

public class PlayerSelected { public string Name; public int ID; public RockPaperScissors Hand = RockPaperScissors.None; public int Rusult; }

public class GameManager : Photon.MonoBehaviour, IPunTurnManagerCallbacks
{

    #region Public Attribute

    public PlayerSelected[] mPlayerSelected_RPS = new PlayerSelected[4];
    public Text mTimeCount;
    #endregion

    #region Private Attribute

    private PunTurnManager turnManager;
    #endregion

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        turnManager = this.gameObject.AddComponent<PunTurnManager>();
        turnManager.TurnManagerListener = this;
        turnManager.TurnDuration = 5f;

    }

    void OnAwake()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.room.playerCount > 1)
        {
            if (this.turnManager.IsOver)
            {
                return;
            }   
            if (this.turnManager.Turn > 0 && this.mTimeCount != null)
            {
                mTimeCount.text = turnManager.RemainingSecondsInTurn.ToString("F0");
            }

        }

        // remote player's selection is only shown, when the turn is complete (finished by both)
        if (this.turnManager.IsCompletedByAll)
        {

        }
        else
        {
            // if the turn is not completed by all, we use a random image for the remote hand
            if (this.turnManager.Turn > 0 && !this.turnManager.IsCompletedByAll)
            {
                // alpha of the remote hand is used as indicator if the remote player "is active" and "made a turn"
                PhotonPlayer remote = PhotonNetwork.player.GetNext();
                if (this.turnManager.GetPlayerFinishedTurn(remote))
                {

                }
                if (remote != null && remote.isInactive)
                {

                }
            }
        }
    }

    #endregion

    #region TurnManager Callbacks

    /// <summary>Called when a turn begins (Master Client set a new Turn number).</summary>
    public void OnTurnBegins(int turn)
    {

    }

    public void OnTurnCompleted(int obj)
    {

    }
    // when a player moved (but did not finish the turn)
    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)
    {
        Debug.Log("OnPlayerMove: " + photonPlayer + " turn: " + turn + " action: " + move);
        throw new NotImplementedException();
    }

    // when a player made the last/final move in a turn
    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)
    {
        Debug.Log("OnTurnFinished: " + photonPlayer + " turn: " + turn + " action: " + (RockPaperScissors)(byte)move);
        GetInputFromEachPlayer(photonPlayer, ref mPlayerSelected_RPS, move);
    }
    public void OnTurnTimeEnds(int obj)
    {

    }

    #endregion

    #region Private Methods
    private void GetInputFromEachPlayer(PhotonPlayer photonPlayer, ref PlayerSelected[] player, object move)
    {
        PlayerSelected playerSelected = new PlayerSelected();

        if (photonPlayer.isLocal)
        {
            playerSelected.Hand = (RockPaperScissors)(byte)move;
            playerSelected.ID = photonPlayer.ID;
            player[0] = playerSelected;
            Debug.Log(" ID : " + player[0].ID + " , Selected : " + player[0].Hand + " ");
        }
        else if (photonPlayer.GetNext().isLocal)
        {
            playerSelected.Hand = (RockPaperScissors)(byte)move;
            playerSelected.ID = photonPlayer.ID;
            player[1] = playerSelected;
            Debug.Log(" ID : " + player[1].ID + " , Selected : " + player[1].Hand + " ");
        }
        else if (photonPlayer.GetNext().GetNext().isLocal)
        {
            playerSelected.Hand = (RockPaperScissors)(byte)move;
            playerSelected.ID = photonPlayer.ID;
            player[2] = playerSelected;
            Debug.Log(" ID : " + player[2].ID + " , Selected : " + player[2].Hand + " ");
        }
        else
        {
            playerSelected.Hand = (RockPaperScissors)(byte)move;
            playerSelected.ID = photonPlayer.ID;
            player[3] = playerSelected;
            Debug.Log(" ID : " + player[3].ID + " , Selected : " + player[3].Hand + " ");
        }

    }

    #endregion

    #region Public Methods

    /// <summary>Call to start the turn (only the Master Client will send this).</summary>
    public void StartTurn()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.turnManager.BeginTurn();
        }
    }

    public void MakeTurn(RockPaperScissors selection)
    {
        this.turnManager.SendMove((byte)selection, true);
    }
    #endregion

    #region Handling of Buttons

    public void RockClick()
    {
        MakeTurn(RockPaperScissors.Rock);
    }

    public void PaperClick()
    {
        MakeTurn(RockPaperScissors.Paper);
    }

    public void ScissorsClick()
    {
        MakeTurn(RockPaperScissors.Scissors);
    }
    #endregion

}
