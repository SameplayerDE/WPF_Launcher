using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Windows;

namespace WpfApp2
{
    public class LauncherHandler
    {
        public static LauncherHandler Instance { get; } = new LauncherHandler();
        private LauncherApplicationInformation _launcherApplicationInformation;
        private const string Hash = "ec7ee762b28eda6156a43eee968958d4bdef1564fefc9e5f3e50e4dff0ed8f80";

        static LauncherHandler()
        {
        }

        private LauncherHandler()
        {
            _launcherApplicationInformation = new LauncherApplicationInformation
            {
                LastTimeApplicationStartUpUnixTimestamp =
                    (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds
            };
            
            LauncherApplicationInformation.SaveToXml(_launcherApplicationInformation);
            LauncherApplicationInformation.SaveToJson(_launcherApplicationInformation);
        }

        public void StartUp()
        {
            //Nothing
            
        }

        public void TryStart()
        {
            if (VerifyFiles())
            {
                //Start Game
                Process.Start("C:/Program Files/Mozilla Firefox/firefox.exe");
                Application.Current.Shutdown();
                return;
            }

            throw new Exception();
        }
        
        public void TryUpdate()
        {
            if (IsServerAvailable())
            {
                
            }
            throw new Exception();
        }

        public bool IsServerAvailable()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            var ping = new Ping();
            var result = false;
            try
            {
                var reply = ping.Send("h073.de", 10 * 1000);
                result = reply?.Status == IPStatus.Success;
            }
            catch (Exception exception)
            {
                // ignored
            }
            //reply = ping.Send(new IPAddress(new byte[]{127,0,0,1}), 3000);
            return result;
        }

        public bool VerifyFiles()
        {
            return CompareByteArrays(ComputeHash("test.txt"), StringToByteArray(Hash));
        }

        private byte[] ComputeHash(string path)
        {
            using var sha256 = SHA256.Create();
            using var stream = File.OpenRead(path);
            return sha256.ComputeHash(stream);
        }

        private bool CompareByteArrays(byte[] input0, byte[] input1)
        {
            var result = false;
            if (input0.Length != input1.Length) return result;
            var i = 0;
            while (i < input0.Length && input0[i] == input1[i])
            {
                i += 1;
            }
            if (i == input0.Length)
            {
                result = true;
            }
            return result;
        }
        
        private static string ByteArrayToString(byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", "").ToLowerInvariant();
        }
        
        private static byte[] StringToByteArray(string hex) {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }
}