using Essence;
using UnityEngine;

public class BossEnemy : Enemy
{
    protected override void HandleEnemyDie()
    {
        Bus<IStageClearEvent>.Raise(new IStageClearEvent());
        base.HandleEnemyDie();
    }
}
