using UnityEngine;

public class Mushroom : Enemy
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new MushroomStateFactory(this);
    }
}
