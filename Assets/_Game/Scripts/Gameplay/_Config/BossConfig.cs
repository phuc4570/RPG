using System;
using UnityEngine;

[Serializable]
public class BossConfig : CharacterConfig<BossConfigData, BossTypeEnum>
{

}

[Serializable]
public class BossConfigData : CharacterConfigData<BossTypeEnum>
{
    [SerializeField] private string bossName;

    public string BossName => bossName;
}