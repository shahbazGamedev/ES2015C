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
}
