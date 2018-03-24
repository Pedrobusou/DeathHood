using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Player : MonoBehaviour {
	[SerializeField] private AudioClip sound;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private float soundRadius = 15f;
	[SerializeField] private LayerMask enemiesLayer;
	[SerializeField] private Collider[] enemies;

	//private ThirdPersonUserControl tpuc;
	private SphereCollider trigger;
	[SerializeField] private float walkingTriggerRadius = 2;
	[SerializeField] private float runningTriggerRadius = 4;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	private void Start() {
		audioSource = this.GetComponent<AudioSource>();
		//tpuc = this.GetComponent<ThirdPersonUserControl>();

		trigger = this.GetComponent<SphereCollider>();
		trigger.radius = walkingTriggerRadius;
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			reproduceSound();
		}

		//if (tpuc.getIsWalking())trigger.radius = walkingTriggerRadius;
		//else trigger.radius = runningTriggerRadius;
	}

	/// <summary>
	/// Reproduce sound and make enemies inside soundRadius lock the player.
	/// </summary>
	private void reproduceSound() {
		audioSource.PlayOneShot(sound);
		enemies = Physics.OverlapSphere(this.transform.position, soundRadius, enemiesLayer);

		foreach (Collider enemy in enemies) {
			enemy.GetComponent<EnemyAi>().lockTarget();
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Enemy")) {
			other.GetComponent<EnemyAi>().lockTarget();
		}
	}
}