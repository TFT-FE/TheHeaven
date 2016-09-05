#pragma strict
var R : float ;
function Start () {
  R   = Random.Range(0.04,0.14);
      yield WaitForSeconds(10);
      if (gameObject.name.Contains("Clone"))
    Destroy (gameObject);
}

function Update () {
	transform.position.x= transform.position.x + R;
}