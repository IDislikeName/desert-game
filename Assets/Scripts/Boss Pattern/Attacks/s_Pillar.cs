using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Pillar : s_Attack
{
    private int amountToSpawn = 4;
    float channelTime;

    public s_Pillar(StateMachine stateMachine, Boss boss, GameObject pillarPrefab) : base(stateMachine, boss)
    {
        weapon = pillarPrefab;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter pillar attack");
        channelTime = 5f;

        //pick location, create weapon
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 spawnPos = RandomPointOnXZCircle(boss.transform.position, 9f);
            SpawnWeapon(weapon, spawnPos);
        }
    }

    public override void UpdateLogicState()
    {
        base.UpdateLogicState();
        channelTime -= Time.deltaTime;
        if (channelTime <= 0f)
        {
            stateMachine.ChangeState(boss.idleMovementState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit pillar attack");
        base.ExitState();
    }

    public override void SpawnWeapon(GameObject weapon, Vector3 position)
    {
        GameObject.Instantiate(weapon, position, Quaternion.identity);
    }

    private Vector3 RandomPointOnXZCircle(Vector3 center, float radius)
    {
        float angle = Random.Range(0, 2f * Mathf.PI);
        return center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
    }
}
