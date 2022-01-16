using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NativeShereScore : MonoBehaviour
{
	public void Share()
	{
	
		StartCoroutine(TakeScreenshotAndShare());
	}

	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared_img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath)
			.SetSubject("Novo Record em Helicopter").SetText("Baixa aí, bora jogar!").SetUrl("https://play.google.com/store/apps/details?id=com.TraineeCodeplays.Helicopter")
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
	}
}
