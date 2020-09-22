using System;

public interface ITakeDmg
{
    IPlrBird Plr { get; }
    Action OnDie { get; set; }
}