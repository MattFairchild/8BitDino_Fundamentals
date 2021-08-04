/*  (c) 2019 matthew fairchild
 *  
 *  interface for all ships that holds properties that all ships share such as health and functions that work on that
 */

using System;

public interface IDestructable
{
    //----------------------------------- VARIABLES -----------------------------------------
    #region VARIABLES

    /// <summary>
    /// max possible amount oh health the ship can have
    /// </summary>
    float MaxHealth
    {
        get;
    }

    /// <summary>
    /// max possible amount oh shield the ship can have
    /// </summary>
    float MaxShield
    {
        get;
    }

    /// <summary>
    /// current health
    /// </summary>
    float Health
    {
        get;
    }

    /// <summary>
    /// current shield
    /// </summary>
    float Shield
    {
        get;
    }

    /// <summary>
    /// delegate that gets called when the ship reaches 0 health
    /// </summary>
    Action OnDeath
    {
        get;
        set;
    }

    #endregion

    //-------------------------------- ISHIP FUNCTIONS --------------------------------------
    #region IShip FUNCTIONS

    /// <summary>
    /// take a given amount of damage. will first deplete shield before health, and call death event if health reches 0
    /// </summary>
    /// <param name="n_dmg">the amount of damage to take. should be a positive value, since it will be subtracted</param>
    void take_damage(float n_dmg);

    /// <summary>
    /// heals the ship by a given amount. does not affect shield, only health can increase up to MaxHealth
    /// </summary>
    /// <param name="n_heal">the amount to heal</param>
    void heal(float n_heal);

    /// <summary>
    /// adds to the current shield of the object. Clamped at MaxShield
    /// </summary>
    /// <param name="n_shield"></param>
    void increase_shield(float n_shield);

    #endregion
}
