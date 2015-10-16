using UnityEngine;

public class MilitaryUnit : Unit
{

    private Quaternion aimRotation;

    /*** Metodes per defecte de Unity ***/

    protected override void Start()
    {
        base.Start();
        objectName = "Military Unit";
    }

    protected override void Update()
    {
        base.Update();
        if (aiming)
        {

        }
    }

    protected override void OnGUI()
    {
        base.OnGUI();
    }


    // Metode per disparar
    protected override void UseWeapon()
    {
        base.UseWeapon();
    }

    // Metode per apuntar
    protected override void AimAtTarget()
    {
        base.AimAtTarget();
        aimRotation = Quaternion.LookRotation(target.transform.position - transform.position);
    }

    /// <summary>
    /// Gets the attack strengh that the unit has when it is attacking.
    /// </summary>
    /// <returns>The number of attack sthengh points.</returns>
    public float GetAttackStrengh()
    {
        return 321;
    }

    /// <summary>
    /// Gets the defense points that the unit has when it is being attacked.
    /// </summary>
    /// <returns>The number of defense points.</returns>
    public float GetDefense()
    {
        return 654;
    }

    /// <summary>
    /// Gets the distance at which the unit can attack, if it is a range unit (e.g. an archer). Otherwise, null.
    /// </summary>
    /// <returns>The distance at which the unit can attack, or null.</returns>
    public float? GetAttackRange()
    {
        return 987;
    }
}
