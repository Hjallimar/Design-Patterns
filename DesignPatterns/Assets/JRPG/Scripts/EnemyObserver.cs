using System.Collections.Generic;
using UnityEngine;

public class EnemyObserver
{
    private static EnemyObserver instance;

    private static EnemyObserver Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new EnemyObserver();
            }
            return instance;
        }
    }

    private List<EnemyBehaviour> activeEnemies = new List<EnemyBehaviour>();

    public static void AssignEnemy(EnemyBehaviour enemy)
    {
        Instance.activeEnemies.Add(enemy);
    }

    public static void EnemyAttack()
    {
        foreach(EnemyBehaviour enemy in instance.activeEnemies)
        {
            if(enemy.Active != false)
            {
                Transform target = CharacterObserver.GetEnemyTarget();
                if(target != null)
                {
                    PlayerMoveCommand move = new PlayerMoveCommand();
                    move.AssignMove(enemy.transform, enemy.transform.position, target.position);
                    PlayerCommand.AddCommand(move);

                    AttackCommand attack = new AttackCommand();
                    attack.AssignCommand(10, target.gameObject);
                    attack.AssignAnimation(enemy.gameObject, "Attack", 0f);
                    PlayerCommand.AddCommand(attack);

                    PlayerMoveCommand move2 = new PlayerMoveCommand();
                    move2.AssignMove(enemy.transform, target.position, enemy.transform.position);
                    PlayerCommand.AddCommand(move2);
                }
            }
        }
    }

    public static bool EnemiesDefeated()
    {
        bool defeat = true;
        foreach(EnemyBehaviour enemy in instance.activeEnemies)
        {
            if (enemy.Active == true)
                defeat = false;
        }
        return defeat;
    }

}
