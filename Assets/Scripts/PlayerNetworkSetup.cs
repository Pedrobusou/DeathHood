using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {
	[SerializeField]
	Behaviour[] componentsToDisable;

	Camera sceneCamera;

	void Start() {
		if (!this.isLocalPlayer) {
			foreach (Behaviour component in this.componentsToDisable)
				component.enabled = false;
		} else {
			this.sceneCamera = Camera.main;
			if (this.sceneCamera)
				this.sceneCamera.gameObject.SetActive(false);
		}
	}

	void OnDisable() {
		if (sceneCamera)
			sceneCamera.gameObject.SetActive(true);
	}
}