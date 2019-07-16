using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour {

	public int maxWait = 15;
	public int minWait = 5;

	private NavMeshAgent agent;
	private Animator animator;

	private List<Vector3> navPoints = new List<Vector3>();
	private Vector3 currentDestination = Vector3.zero;
	private bool isChilling = false;

	// Use this for initialization
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		animator = GetComponent<Animator> ();

		agent.radius = 1;
		agent.speed = 2;
	}

	void Start(){
		GameObject navPointsGo = GameObject.Find ("AI Nav Points");
		Transform[] transforms = navPointsGo.transform.GetComponentsInChildren<Transform> ();
		foreach (var point in transforms) {
			navPoints.Add (point.position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("Speed_f", agent.velocity.magnitude);

		if (isChilling) {
			return;
		}

		if (!agent.hasPath) {
			setNewPath ();	
		} else if (pathComplete()) {
			StartCoroutine ("chill");
		}
	}

	private bool pathComplete(){
		return agent.pathStatus == NavMeshPathStatus.PathComplete;
	}

	private void setNewPath(){
		int index = Random.Range (0, navPoints.Count);
		currentDestination = navPoints[index];
		if (agent.enabled) {
			agent.SetDestination(currentDestination);
		}
	}

	IEnumerator chill(){
		isChilling = true;
		yield return new WaitForSeconds(1);
		animator.SetInteger ("Animation_int", randomAnimationValue ());
		yield return new WaitForSeconds(Random.Range (minWait, maxWait));
		animator.SetInteger ("Animation_int", 0);
		yield return new WaitForSeconds(1);
		isChilling = false;
		setNewPath ();
	}

	void OnDrawGizmosSelected() {
		if (!isChilling) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere (transform.position, .3f);
		}
	}

	int randomAnimationValue(){
		return Random.Range (1, 10);
	}
}
