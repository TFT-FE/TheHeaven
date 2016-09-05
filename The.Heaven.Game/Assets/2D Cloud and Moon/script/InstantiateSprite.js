#pragma strict

  
public var clouds : GameObject[];
public var seconds : int = 5;

function Start () {
  for(var i : int = 0; i < clouds.Length; i++) {
 	Instantiate(clouds[i],transform.position,Quaternion.identity);
 	seconds=Random.Range(1,5);
 	yield WaitForSeconds(seconds);
 	if (i==clouds.Length-1 )
 	i=0;
 }
 
}

function Update () {

}