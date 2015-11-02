#pragma strict

var target:Transform;
 
//wandering vars
public var wanderSpeed = 2.0;
public var wanderRotSpeed = 5.0;
public var wanderRadius = 10.0;
public var wanderRayDistance = 5.0;
public var wanderPauseMin = 2.0;
public var wanderPauseMax = 6.0;
private var basePosition : Vector3;
private var currentDestination : Vector3;
 
//chase vars
var chaseDistance : float = 10.0;
var chaseSpeed : float = 3.0;
var chaseRotSpeed : float = 5.0;
 
//attack vars
var attackDistance : float = 3.0;
var attackRate : float = 0.25;
 
//state setup
enum aiState{ wandering, chasing, attacking }
var state : aiState;
 
InvokeRepeating("StateLogic", 0.0, 0.01);
 
function Start(){
    if(target == null)
        target = GameObject.FindWithTag("Player").transform;
    ChooseNextDestination();
    yield StateMachine();
}
 
function StateLogic(){
    var distanceToTarget = (target.position - transform.position).sqrMagnitude;
    if(distanceToTarget <= attackDistance*attackDistance)
        state = aiState.attacking;
    else if(distanceToTarget <= chaseDistance*chaseDistance)
        state = aiState.chasing;
    else
        state = aiState.wandering;
}
 
function StateMachine(){
 
    while(true){
        switch(state){
            case aiState.wandering:
                yield Wander();
                break;
            case aiState.chasing:
                Chase();
                break;
            case aiState.attacking:
                yield Attack();
                break;
        }
        yield;
    }
}
 
function Wander(){
    RotateToward(currentDestination, wanderRotSpeed);
    MoveForward(wanderSpeed);
    //BroadcastMessage("PlayAnimation", "walk");
    var destPosZeroY = currentDestination;
    var currentPosZeroY = transform.position;
    destPosZeroY.y = 0;
    currentPosZeroY.y = 0;
    if((destPosZeroY - currentPosZeroY).magnitude < 1.0){
        yield WaitForSeconds(Random.Range(wanderPauseMin, wanderPauseMax));
        ChooseNextDestination();
    }
}
 
function ChooseNextDestination(){
    var randOffset : Vector2 = Random.insideUnitCircle * wanderRadius;
    currentDestination = basePosition + new Vector3(randOffset.x, transform.position.y, randOffset.y);
    Debug.DrawLine(transform.position, currentDestination, Color.white);
}
 
function Chase(){
    RotateToward(target.position, chaseRotSpeed);
    MoveForward(chaseSpeed);
}
 
function Attack(){
    //target.GetComponent(PlayerStatus).TakeDamage(20.0);
    yield WaitForSeconds(attackRate);
}
 
function RotateToward(targetPos : Vector3, rotSpeed : float){
    targetPos.y = transform.position.y;
    var rotation = Quaternion.LookRotation(targetPos - transform.position);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);    
}
 
    function MoveForward(moveSpeed : float){
        transform.Translate(Vector3.forward*Time.deltaTime*moveSpeed);
    }