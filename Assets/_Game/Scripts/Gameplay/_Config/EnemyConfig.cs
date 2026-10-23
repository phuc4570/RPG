using System;
using UnityEngine;

[Serializable]
public class EnemyConfig : CharacterConfig<EnemyConfigData, EnemyTypeEnum>
{

}

[Serializable]
public class EnemyConfigData : CharacterConfigData<EnemyTypeEnum>
{

}