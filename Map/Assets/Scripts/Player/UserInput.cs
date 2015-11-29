using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UserInput : MonoBehaviour
{

    private Player player;
	public GameObject SelectedArea;

    // Inicialitzem
    void Start()
    {
        player = GetComponentInParent<Player>();
		createSelectedArea ();
    }

    // Actualitzem a cada frame
    void Update()
    {
        if (player && player.human)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OpenPauseMenu();

            bool leftClick = Input.GetMouseButtonDown(0);
            bool rightClick = Input.GetMouseButtonDown(1);
            if (leftClick || rightClick)
                HandleMouseClick(leftClick, rightClick);

            if (Input.GetKey (KeyCode.M)) morirEnemigos(); 
            if (Input.GetKey (KeyCode.P)) morirJugador();

            MoveCamera();
            RotateCamera();
            MouseActivity();
        }

    }

    private void OpenPauseMenu()
    {
    }

	private void createSelectedArea()
	{
		SelectedArea = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
		SelectedArea.transform.position = new Vector3(0, 0, 0);
		SelectedArea.transform.localScale = new Vector3(0, 0, 0);
		SelectedArea.name = "SelectedArea";
		Destroy (SelectedArea.GetComponent<Collider> ());
	}

    private void HandleMouseClick(bool leftClick, bool rightClick)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // Click on non-UI element
        {
            RaycastHit hit = FindMouseTargetHit();
            RTSObject targetRtsElement = (hit.collider != null)
                ? hit.collider.gameObject.GetComponent<RTSObject>() : null;

            if (player.SelectedObject && player.SelectedObject.GetComponent<CivilUnit>()
			    && player.SelectedObject.GetComponent<CivilUnit>().waitingForBuildingLocationSelection)
			{
                player.SelectedObject.GetComponent<CivilUnit>().SetBuildingLocation();
			}

			else if (leftClick)
            {
				object[] obj = GameObject.FindSceneObjectsOfType(typeof (RTSObject));
				foreach (object o in obj)
				{
					RTSObject g = (RTSObject) o;
					g.SetSelection(false);
				}
                // Call the class to change the currently selected RTS element.
                // Note that this will be null if the user clicked on nothing or a non-RTS element object.
                player.ChangeSelectedRtsObject(targetRtsElement);
				if(targetRtsElement && targetRtsElement.IsOwnedBy(player))
				{
					targetRtsElement.SetSelection(true);
					SelectedArea.GetComponent<MeshRenderer>().enabled = true;
				}
				else if (player.SelectedObject != null && player.SelectedObject.IsOwnedBy(player))
				{
					player.SelectedObject.SetSelection(true);
					SelectedArea.GetComponent<MeshRenderer>().enabled = true;
				}
				else if (SelectedArea)
				{
					SelectedArea.GetComponent<MeshRenderer>().enabled = false;
				}
            }
            else if (rightClick && player.SelectedObject != null && player.SelectedObject.IsOwnedBy(player))
            {
                if (player.SelectedObject.GetType().Equals("CivilUnit"))
                {
                    player.SelectedObject.GetComponent<CivilUnit>().harvesting = false;
                }

                // If there is any building project for this object, deassign it before assigning other actions
                if (player.SelectedObject.IsBuilding())
                {
                    player.SelectedObject.AssignBuildingProject(null);
                }

				//Atacar
                if (player.SelectedObject.CanAttack() &&
                    targetRtsElement != null && targetRtsElement.owner != null &&
                    targetRtsElement.CanBeAttacked() &&
                    player.Team.IsEnemyOf(targetRtsElement.owner.Team))
                {
                    // If the player clicked over an unit or building and the selected unit
                    // can attack, start the attacking sequence
                    player.SelectedObject.AttackObject(targetRtsElement);
                }
				//Construir un edificio
				else if (player.SelectedObject.CanBuild()&& targetRtsElement != null && targetRtsElement.owner==player && targetRtsElement.CanBeBuilt())
				{
                    player.SelectedObject.AssignBuildingProject(targetRtsElement.GetComponent<Building>());
				}
                //Recolecto
				else if (player.SelectedObject.tag == "civil" && targetRtsElement != null && targetRtsElement.GetComponent<Resource>())
				{
                    //Empieza a recolectar el recurso seleccionado
                    player.SelectedObject.GetComponent<CivilUnit>().StartHarvest(targetRtsElement.GetComponent<Resource>());
				}
                else if (player.SelectedObject.CanMove())
                {
                    // Otherwise, if the unit can move, start the movement sequence
                    player.SelectedObject.MoveTo(hit.point);
                }
            }
        }
    }

    private RaycastHit FindMouseTargetHit()
    {
        RTSObject targetRtsElement = null;

        // Cast a ray in the direction clicked by the user to detect the currently clicked element,
        // and if it is a RTS element, then save its reference
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        // First, cast a ray without considering the currently selected element
        // (add it temporally to the "ignore raycast" layer), so if the user clicked
        // on a unit that is occluded by the current element, it will be selected instead
        int origSelectedObjectLayer = 0;
        if (player.SelectedObject != null)
        {
            origSelectedObjectLayer = player.SelectedObject.gameObject.layer;
            player.SelectedObject.gameObject.layer = 2; // IgnoreRaycast layer
        }

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            targetRtsElement = objectHit.GetComponent<RTSObject>();
        }

        if (player.SelectedObject != null)
        {
            player.SelectedObject.gameObject.layer = origSelectedObjectLayer; // Restore original layer
        }

        // If we didn't find a collision, repeat the raycast in order to test if the user
        // clicked again on the unit he had already selected
        if (targetRtsElement == null && Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;
            targetRtsElement = objectHit.GetComponent<RTSObject>();
        }

        return hit;
    }

    //Funcion para que todos los objectos del jugador mueran
    private void morirJugador(){
        GameObject[] games;
        string[] etiquetas = {"civil", "townCenter", "mility", "armyBuilding"};
        foreach(string eti in etiquetas){
            /*games = GameObject.FindGameObjectsWithTag(eti);
            foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
                Destroy(civil, 2);
            }*/
            GameObject game = GameObject.FindGameObjectWithTag(eti);
            Destroy(game, 2);
        }/*
        games = GameObject.FindGameObjectsWithTag("townCenter");
        foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            Destroy(civil, 2);
        }
        games = GameObject.FindGameObjectsWithTag("civil");
        foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            Destroy(civil, 2);
        }
        games = GameObject.FindGameObjectsWithTag("civil");
        foreach(var civil in games){ //Miro todos los Unit y si hay alguno de owner lo sumo
            Destroy(civil, 2);
        }
        GameObject game = GameObject.FindGameObjectWithTag("civil");
        Destroy(game, 2);
        game = GameObject.FindGameObjectWithTag("townCenter");
        Destroy(game, 2);
        game = GameObject.FindGameObjectWithTag("mility");
        Destroy(game, 2);*/
        Debug.Log("Matas a los tuyos");

    }

    //Funcion para que todos los objectos del enemigo mueran
    private void morirEnemigos(){
        GameObject game = GameObject.FindGameObjectWithTag("civil");
        if(game.GetComponent<CivilUnit>().owner!=player){
            Destroy(game, 4);
        }
        game = GameObject.FindGameObjectWithTag("townCenter");
        if(game.GetComponent<TownCenterBuilding>().owner!=player){
            Destroy(game, 4);
        }
    }

    private void MoveCamera()
    {
    }

    private void RotateCamera()
    {
    }

    private void MouseActivity()
    {
    }
}
