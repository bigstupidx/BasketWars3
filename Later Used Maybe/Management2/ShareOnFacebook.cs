using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShareOnFacebook : MonoBehaviour {

	public void StartShare(){
		//StartCoroutine("Share");
		Share();
	}

	public void Share(){
		if(FacebookBinding.canUserUseFacebookComposer()){
			var parameters = new Dictionary<string,string>
			{
				{ "link", "https://itunes.apple.com/app/basketwars-world-at-war-basketball/id825100068?ls=1&mt=8" },
				{ "name", "BasketWars" },
				{ "picture", "https://s3.mzstatic.com/us/r30/Purple4/v4/fa/9b/2a/fa9b2aff-a7b9-11cf-1fd9-4a38d87aa2bd/mzl.vtaufpjd.png?downloadKey=1413223756_4186864c69b9243337fbcf50aac73e72" },
				{ "caption", "I scored " + GameManager.m_baskets_made + " points on " + LevelNameConverter.GetLevelName() }
			};
			FacebookBinding.showDialog( "stream.publish", parameters );
		}

		/*if(FacebookBinding.canUserUseFacebookComposer())
		{
			// ensure the image exists before attempting to add it!
			var pathToImage = FacebookGUIManager.screenshotFilename;
			Application.CaptureScreenshot(pathToImage);
		//yield return null;
			pathToImage = Application.persistentDataPath + "/" + pathToImage;
			if( System.IO.File.Exists( pathToImage ) ){		
				//FacebookBinding.showFacebookShareDialog( parameters );
				FacebookBinding.showFacebookComposer( "I scored " + GameManager.m_baskets_made + " points on this level! ", pathToImage, "https://itunes.apple.com/app/basketwars-world-at-war-basketball/id825100068?ls=1&mt=8" );
			}else{
				Debug.LogError("No image path!");
			}
		}*/
	}
}
