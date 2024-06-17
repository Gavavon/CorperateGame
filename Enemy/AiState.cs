using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId 
{
	Idle,           //Default state that does nothing
	Alerted,        //When the player gets near the enemy
	Combat,			//While the enemy is engaged with the player in combat
	Stunned,		//When the enemy gets stunned it takes them out of combat temporarily
	Death		    //activate ragdoll and kill enemy
}

//rip out all the states we don't need and redevelop all the behaviour trees

public interface AiState
{
    AiStateId GetId();
    void Enter(AiAgent agent);
	void Update(AiAgent agent);
    void Exit(AiAgent agent);
}
