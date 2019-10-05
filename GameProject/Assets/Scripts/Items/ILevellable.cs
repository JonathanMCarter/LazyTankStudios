using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevellable
{
    void onLevelUp(int level);
    int getDamageFromLevel(int level);
    int getLevel();
    int maxLevel();
    int expNeeded(int level);
    int getExp();
}
