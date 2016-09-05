using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalulateResult : MonoBehaviour
{

    PlayerSelected Draw = new PlayerSelected();
    PlayerSelected win2 = new PlayerSelected();

    // Use this for initialization
    void Start()
    {
        Draw.ID = -1;
        Draw.Name = "None";
        Draw.Hand = RockPaperScissors.None;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public PlayerSelected TwoPlayers(PlayerSelected p1, PlayerSelected p2)
    {


        if (p1.Hand == p2.Hand)
            return Draw;

        else if (p1.Hand == RockPaperScissors.None && p2.Hand != RockPaperScissors.None)
        {
            return p2;
        }

        else if (p1.Hand != RockPaperScissors.None && p2.Hand == RockPaperScissors.None)
        {
            return p1;
        }

        else if (p1.Hand == RockPaperScissors.Paper)
        {

            if (p2.Hand == RockPaperScissors.Rock)
                return p1;
            else
                return p2;

        }

        else if (p1.Hand == RockPaperScissors.Rock)
        {

            if (p2.Hand == RockPaperScissors.Paper)
                return p2;
            else
                return p1;

        }

        else if (p1.Hand == RockPaperScissors.Scissors)
        {

            if (p2.Hand == RockPaperScissors.Paper)
                return p1;
            else
                return p2;

        }

        return Draw;
    }

    public List<PlayerSelected> ThreePlayers(PlayerSelected p1, PlayerSelected p2, PlayerSelected p3)
    {

        List<PlayerSelected> winners = new List<PlayerSelected>();

        //Draw Condition
        if (p1.Hand == p2.Hand && p2.Hand == p3.Hand)
            winners.Add(Draw);

        //Three players have chosen their choices
        else if (p1.Hand != RockPaperScissors.None && p2.Hand != RockPaperScissors.None && p3.Hand != RockPaperScissors.None)
        {
            //Draw Condition
            if (p1.Hand != p2.Hand && p2.Hand != p3.Hand && p3.Hand != p1.Hand)
                winners.Add(Draw);

            //Win Condition
            //Player 1 choose "Paper"
            else if (p1.Hand == RockPaperScissors.Paper)
            {

                //Player 1 choose "Paper" & Player 2 choose "Paper"
                if (p2.Hand == RockPaperScissors.Paper)
                {
                    if (p3.Hand == RockPaperScissors.Rock)
                    {
                        winners.Add(p1);
                        winners.Add(p2);
                    }
                    else if (p3.Hand == RockPaperScissors.Scissors)
                        winners.Add(p3);
                }

                //Player 1 choose "Paper" & Player 2 choose "Rock"
                else if (p2.Hand == RockPaperScissors.Rock)
                {
                    if (p3.Hand == RockPaperScissors.Paper)
                    {
                        winners.Add(p1);
                        winners.Add(p3);
                    }
                    else if (p3.Hand == RockPaperScissors.Rock)
                    {
                        winners.Add(p1);
                    }
                }

                //Player 1 choose "Paper" & Player 2 choose "Scissors"
                else if (p2.Hand == RockPaperScissors.Scissors)
                {
                    if (p3.Hand == RockPaperScissors.Paper)
                    {
                        winners.Add(p2);
                    }
                    else if (p3.Hand == RockPaperScissors.Scissors)
                    {
                        winners.Add(p2);
                        winners.Add(p3);
                    }
                }
            }

            //Player 1 choose "Rock"
            else if (p1.Hand == RockPaperScissors.Rock)
            {
                //Player 1 choose "Rock" & Player 2 choose "Paper"
                if (p2.Hand == RockPaperScissors.Paper)
                {
                    if (p3.Hand == RockPaperScissors.Paper)
                    {
                        winners.Add(p2);
                        winners.Add(p3);
                    }
                    else if (p3.Hand == RockPaperScissors.Rock)
                    {
                        winners.Add(p2);
                    }
                }

                //Player 1 choose "Rock" & Player 2 choose "Rock"
                else if (p2.Hand == RockPaperScissors.Rock)
                {
                    if (p3.Hand == RockPaperScissors.Paper)
                    {
                        winners.Add(p3);
                    }
                    else if (p3.Hand == RockPaperScissors.Scissors)
                    {
                        winners.Add(p1);
                        winners.Add(p2);
                    }
                }

                //Player 1 choose "Rock" & Player 2 choose "Scissors"
                else if (p2.Hand == RockPaperScissors.Scissors)
                {
                    if (p3.Hand == RockPaperScissors.Rock)
                    {
                        winners.Add(p1);
                        winners.Add(p3);
                    }
                    else if (p3.Hand == RockPaperScissors.Scissors)
                    {
                        winners.Add(p1);
                    }
                }
            }

            //Player 1 choose "Scissors"
            else if (p1.Hand == RockPaperScissors.Scissors)
            {
                //Player 1 choose "Scissors" and Player 2 choose "Paper"
                if (p2.Hand == RockPaperScissors.Paper)
                {
                    if (p3.Hand == RockPaperScissors.Paper)
                    {
                        winners.Add(p1);
                    }
                    else if (p3.Hand == RockPaperScissors.Scissors)
                    {
                        winners.Add(p1);
                        winners.Add(p3);
                    }
                }

                //Player 1 choose "Scissors" and Player 2 choose "Rock"
                else if (p2.Hand == RockPaperScissors.Rock)
                {
                    if (p3.Hand == RockPaperScissors.Rock)
                    {
                        winners.Add(p2);
                        winners.Add(p3);
                    }
                    else if (p3.Hand == RockPaperScissors.Scissors)
                    {
                        winners.Add(p2);
                    }
                }

                //Player 1 choose "Scissors" and Player 2 choose "Scissors"
                else if (p2.Hand == RockPaperScissors.Scissors)
                {
                    if (p3.Hand == RockPaperScissors.Paper)
                    {
                        winners.Add(p1);
                        winners.Add(p2);
                    }
                    else if (p3.Hand == RockPaperScissors.Rock)
                    {
                        winners.Add(p3);
                    }
                }
            }
        }

        //Two players didn't choose any choices
        else if (p1.Hand != RockPaperScissors.None && p2.Hand == RockPaperScissors.None && p3.Hand == RockPaperScissors.None)
        {

            winners.Add(p1);
        }
        else if (p1.Hand == RockPaperScissors.None && p2.Hand == RockPaperScissors.None && p3.Hand != RockPaperScissors.None)
        {

            winners.Add(p3);
        }
        else if (p1.Hand == RockPaperScissors.None && p2.Hand != RockPaperScissors.None && p3.Hand == RockPaperScissors.None)
        {

            winners.Add(p2);
        }

        //One player didn't choose a choice
        else if (p1.Hand != RockPaperScissors.None && p2.Hand != RockPaperScissors.None && p3.Hand == RockPaperScissors.None)
        {
            win2 = TwoPlayers(p1, p2);
            winners.Add(win2);
        }
        else if (p1.Hand != RockPaperScissors.None && p2.Hand == RockPaperScissors.None && p3.Hand != RockPaperScissors.None)
        {
            win2 = TwoPlayers(p1, p3);
            winners.Add(win2);
        }
        else if (p1.Hand == RockPaperScissors.None && p2.Hand != RockPaperScissors.None && p3.Hand != RockPaperScissors.None)
        {
            win2 = TwoPlayers(p2, p3);
            winners.Add(win2);
        }
        return winners;
    }


}
