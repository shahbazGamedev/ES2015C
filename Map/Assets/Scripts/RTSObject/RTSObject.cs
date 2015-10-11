using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RTSObject : MonoBehaviour {

    // Variables publiques generals
    public string objectName = "GenericObject";     // Nom del objecte
    public int cost = 100, sellValue = 10, hitPoints = 100, maxHitPoints = 100; // Cost, valor, punts de vida i vida maxima
    public int ObjectId { get; set; }               // Identificador unic del objecte
    public enum ResourceType { Gold, Wood, Food, Unknown }    // Declarem els tipus de recursos

    // Variables accessibles per a les subclasses
    protected Player player;                        // A quin player correspon
    protected string[] actions = { };               // Accions que pot realitzar
    protected bool currentlySelected = false;       // Indica si esta seleccionat
    protected Rect playingArea = new Rect(0.0f, 0.0f, 0.0f, 0.0f);  // Area de actuacio de la unitat
    protected float healthPercentage = 1.0f;        // Percentatge de vida
    protected RTSObject target = null;              // Posible objectiu
    protected bool attacking = false, movingIntoPosition = false;   // Booleans de dos dels estats comuns als objectes
    protected List<RTSObject> nearbyObjects;        // Llista de objectes propers

    protected Animator anim;                        // Referencia al component animator.
    protected Rigidbody objectRigidbody;            // Referencia al component Rigidbody.

    /*** Metodes per defecte de Unity ***/

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        objectRigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        SetPlayer();
    }

    protected virtual void Update()
    {
        if (attacking && !movingIntoPosition) PerformAttack();
    }

    protected virtual void OnGUI()
    {
        if (currentlySelected) DrawGUI();
    }

    /*** Metodes publics ***/

    // Metode per declarar el Player
    public void SetPlayer()
    {
        player = transform.root.GetComponentInChildren<Player>();
    }

    // Metode per obtenir el Player
    public Player GetPlayer()
    {
        return player;
    }

    // Metode per declarar la seleccio del objecte
    public virtual void SetSelection(bool selected, Rect playingArea)
    {
        currentlySelected = selected;
        if (selected)
        {
            this.playingArea = playingArea;
        }
    }

    // Metode per declarar el area de joc del objecte
    public void SetPlayingArea(Rect playingArea)
    {
        this.playingArea = playingArea;
    }

    // Metode per obtenir les accions del objecte
    public string[] GetActions()
    {
        return actions;
    }

    // Metode per realitzar la accio corresponent
    public virtual void PerformAction(string actionToPerform)
    {
    }

    // Metode per declarar accions al fer click al boto del ratoli
    public virtual void MouseClick(GameObject hitObject, Vector3 hitPoint, Player controller)
    {
    }

    // Metode per saber si el Player es el propietari del objecte
    public bool IsOwnedBy(Player owner)
    {
        if (player && player.Equals(owner))
            return true;
        return false;
    }

    // Metode per saber si el objecte pot atacar o no
    public virtual bool CanAttack()
    {
        return false;
    }

    // Metode per saber si pot el objecte moures o no
    public virtual bool CanMove()
    {
        return false;
    }

    // Metode per extreure vida al objecte
    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0) Destroy(gameObject);
    }

    /*** Metodes privats ***/

    // Metode que canvia la seleccio del Player a un nou objecte
    private void ChangeSelection(RTSObject rtsObject, Player controller)
    {
        SetSelection(false, playingArea);
        if (controller.SelectedObject) controller.SelectedObject.SetSelection(false, playingArea);
        controller.SelectedObject = rtsObject;
    }

    // Metode que dibuixa el GUI del objecte
    private void DrawGUI()
    {
    }

    // Metode que realitza el atac
    private void PerformAttack()
    {
        if (!target)
        {
            attacking = false;
            return;
        }
    }

    /*** Metodes interns accessibles per les subclasses ***/

    // Metode per calcular la vida actual del objecte
    protected virtual void CalculateCurrentHealth()
    {
        healthPercentage = (float)hitPoints / (float)maxHitPoints;
    }
}
