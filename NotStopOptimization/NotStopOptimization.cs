using BepInEx;
using HarmonyLib;
using Spine.Unity;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NotStopOptimization
{
	[BepInPlugin("Nice2cu.NotStopOptimization", "劝君莫停优化", "1.0")]

	public class NotStopOptimization : BaseUnityPlugin
	{
	private static PlayerAnimControl playerAnimControl;

		public void Start()
		{
			Harmony.CreateAndPatchAll(typeof(NotStopOptimization), null);
			playerAnimControl = PlayerAnimControl.instance;
		}

		public void Update()
		{

			if (RedSoulTrialControl.GetTrial(TrialType.QuanJunMoTing))
			{
				var scene = SceneManager.GetActiveScene();
				GameObject[] gameObjects = scene.GetRootGameObjects();
				if (scene.isLoaded)
				{
					for (int i = 0; i < gameObjects.Length; i++)
					{
						//黑色太阳
						if (gameObjects[i].name == "TheEyeOfHell(Clone)")
						{
							GameObject sun = GameObject.Find("TheEyeOfHell(Clone)");
							//场景中如果存在TheEyeOfHell，则将IsFight设置为true,反之为false
							if (sun)
							{
								playerAnimControl.IsFight = true;
								//Console.WriteLine("IsFight:" + playerAnimControl.IsFight);
							}
							else
							{
								playerAnimControl.IsFight = false;
								//Console.WriteLine("IsFight:" + playerAnimControl.IsFight);
							}
						}
					}
				}
			}
            //场景测试
    //        var key = new BepInEx.Configuration.KeyboardShortcut(KeyCode.F10);
    //        if (key.IsDown())
    //        {
    //            var sceneName = SceneManager.GetActiveScene().name;
				//AudioController.Instance.CvPlay("V_LINGLONG_14", 1f, gameObject, null);
				//Console.WriteLine(sceneName);
    //        }


        }


		//剑来
		[HarmonyPostfix]
		[HarmonyPatch(typeof(DragonBossControl), "Update")]
		public static void DragonBossControl_Update_Postfix(DragonBossControl __instance)
		{
			var isOppression = Traverse.Create(__instance).Field("isOppression").GetValue();
			var isOppressionAfter = Traverse.Create(__instance).Field("isOppressionAfter").GetValue();

			if ((bool)isOppression)
			{
				playerAnimControl.IsFight = false;
				//Console.WriteLine("IsFight:" + playerAnimControl.IsFight);
			}
			else if ((bool)isOppressionAfter)
			{
				playerAnimControl.IsFight = true;
				//Console.WriteLine("IsFight:" + playerAnimControl.IsFight);
			}
		}
	}
}