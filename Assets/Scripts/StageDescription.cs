using UnityEngine;


[CreateAssetMenu(menuName = "StageDescription")]
public class StageDescription : ScriptableObject {
	[SerializeField] string introText;
	[SerializeField] AudioClip narration;
	[SerializeField] public OffsetStrategy strategy;
	[SerializeField] public GameObject prefab;

	public Stage Create() {
		return new Stage (introText.Split(' '), narration);
	}
}