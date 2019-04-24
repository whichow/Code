using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace BuildPipline
{
    public class AndroidChannelBuilder : Builder
    {
        private ConfigData channelData;

        protected override void Build()
        {
            if (HasArg("-channel"))
            {
                BuildChannel(GetArg("-channel"));
            }
            else
            {
                BuildAllChannels();
            }
        }

        protected override void PreBuild()
        {
            SetupBuildSettings();
            CopySDK();
            ReplacePoster();
            ReplaceSplash();
            SetIcon();
            SetUmengChannel();
            SetPosterDir();
            SetSDKChannel();
        }

        protected override void PostBuild()
        {
            DeleteSDK();
        }

        private void BuildAllChannels()
        {
            foreach (var item in configData)
            {
                var channel = item["Channel"].AsString();
                BuildChannel(channel);
            }
        }

        private void BuildChannel(string channel)
        {
            this.channelData = GetConfigByChannel(channel);

            PreBuild();
            BuildAPK();
            PostBuild();
        }

        private ConfigData GetConfigByChannel(string channel)
        {
            foreach (var item in configData)
            {
                if (item["Channel"].AsString() == channel)
                {
                    return item;
                }
            }
            return null;
        }

        private void BuildAPK()
        {
            var buildScenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            string output;
            if (HasArg("-o"))
            {
                output = GetArg("-o");
            }
            else
            {
                output = channelData["Package"].AsString() + "_v" + channelData["Version"].AsString() + "_" + channelData["Channel"].AsString() + "_" + System.DateTime.Now.ToString("yyyyMMdd") + ".apk";
            }

            string error = BuildPipeline.BuildPlayer(buildScenes, output, BuildTarget.Android, BuildOptions.None);
            if (error != null && error.Length > 0)
            {
                Debug.LogError(error);
            }
        }

        private void SetupBuildSettings()
        {
            var keystore = channelData["Keystore"].AsString();
            if (!string.IsNullOrEmpty(keystore))
            {
                keystore = keystore.Replace("$KEYSTORE", variables["KEYSTORE"].AsString());
                PlayerSettings.Android.keystoreName = keystore;
                PlayerSettings.Android.keystorePass = channelData["KeyPass"].AsString();
                PlayerSettings.Android.keyaliasName = channelData["Alias"].AsString();
                PlayerSettings.Android.keyaliasPass = channelData["Pass"].AsString();
            }

#if UNITY_5_6_OR_NEWER
            PlayerSettings.applicationIdentifier = channelData["Package"].AsString();
#else
            PlayerSettings.bundleIdentifier = channelData["Package"].AsString();
#endif
            PlayerSettings.bundleVersion = channelData["Version"].AsString();
            PlayerSettings.Android.bundleVersionCode = channelData["VersionCode"].AsInt();
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, channelData["Define"].AsString());
        }

        private void CopySDK()
        {
            var sdk = channelData["SDK"].AsString();
            if (string.IsNullOrEmpty(sdk))
            {
                return;
            }
            sdk = sdk.Replace("$SDK", variables["SDK"].AsString());

            DeleteSDK();
            FileUtils.CopyDirectory(sdk, "Assets/SDK/" + channelData["Channel"].AsString(), true);
        }

        private void DeleteSDK()
        {
            FileUtils.DeleteExistDirectory("Assets/SDK/" + channelData["Channel"].AsString());
        }

        private void ReplacePoster()
        {
            var poster = channelData["Poster"].AsString();
            if (string.IsNullOrEmpty(poster))
            {
                return;
            }
            poster = poster.Replace("$POSTER", variables["POSTER"].AsString());

            var posterDir = "Assets/Plugins/Android/res/drawable";
            if (Directory.Exists(posterDir))
            {
                var postFiles = FileUtils.GetFilesWithoutExtension(posterDir, "dialog_device");
                FileUtils.DeleteFiles(postFiles);
                File.Copy(poster, Path.Combine(posterDir, Path.GetFileName(poster)));
            }

            posterDir = "Assets/Plugins/Android/libs/Resources/DevicePoster";
            if (Directory.Exists(posterDir))
            {
                var postFiles = FileUtils.GetFilesWithoutExtension(posterDir, "dialog_device");
                FileUtils.DeleteFiles(postFiles);
                File.Copy(poster, Path.Combine(posterDir, Path.GetFileName(poster)));
            }
        }

        private void ReplaceSplash()
        {
            var splash = channelData["Splash"].AsString();
            if (string.IsNullOrEmpty(splash))
            {
                return;
            }
            splash = splash.Replace("$SPLASH", variables["SPLASH"].AsString());
            var splashFile = "Assets/Frameworks/Splash/Plugins/Android/OrbbecSplash.aar";
            File.Copy(splash, splashFile, true);
        }

        private void SetIcon()
        {

        }

        private void SetUmengChannel()
        {
            var umengChannel = channelData["UmengChannel"].AsString();
            if (string.IsNullOrEmpty(umengChannel))
            {
                return;
            }

            var manifest = "Assets/Plugins/Android/AndroidManifest.xml";
            var text = File.ReadAllText(manifest);

            text = Regex.Replace(text, "(?<=android:name=\"umeng_channel\" android:value=\").*(?=\")", umengChannel);
            File.WriteAllText(manifest, text);
        }

        private void SetPosterDir()
        {
            var posterDir = channelData["PosterDir"].AsString();
            if (string.IsNullOrEmpty(posterDir))
            {
                return;
            }

            var manifest = "Assets/Plugins/Android/AndroidManifest.xml";
            var text = File.ReadAllText(manifest);

            text = Regex.Replace(text, "(?<=android:name=\"poster_dir\" android:value=\").*(?=\")", posterDir);
            File.WriteAllText(manifest, text);
        }

        private void SetSDKChannel()
        {
            var sdkChannel = channelData["SDKChannel"].AsString();
            if (string.IsNullOrEmpty(sdkChannel))
            {
                return;
            }

            var manifest = "Assets/Plugins/Android/AndroidManifest.xml";
            var text = File.ReadAllText(manifest);

            text = Regex.Replace(text, "(?<=android:name=\"sdk_channel\" android:value=\").*(?=\")", sdkChannel);
            File.WriteAllText(manifest, text);
        }
    }
}