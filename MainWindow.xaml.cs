using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Net.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;

namespace BePaR
{
    public partial class MainWindow : Window
    {
        private const string CURRENT_VERSION_XWaNTeD = "1.0.0";

        private bool vasl_hastim_XWaNTeD = false;
        private bool dare_mikhore_XWaNTeD = false;
        private bool halate_bazi_XWaNTeD = true;
        private Process? motor_XWaNTeD;
        private static readonly HttpClient client_XWaNTeDDev = new HttpClient();
        private HttpListener? gosh_bede_XWaNTeD;

        private string masir_token_XWaNTeD = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "auth.dat");

        public MainWindow()
        {
            InitializeComponent();
            KhamooshKardanMotor_XWaNTeD();
        }

        private async void BalaOomadan_XWaNTeD(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeIn_XWaNTeD = new DoubleAnimation { From = 0, To = 1, Duration = TimeSpan.FromSeconds(0.6) };
            DoubleAnimation slideUp_XWaNTeD = new DoubleAnimation { From = 30, To = 0, Duration = TimeSpan.FromSeconds(0.6), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
            Ghab_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, fadeIn_XWaNTeD);
            Ghab_XWaNTeD.RenderTransform.BeginAnimation(TranslateTransform.YProperty, slideUp_XWaNTeD);

            DoubleAnimation liquidGlow_XWaNTeD = new DoubleAnimation { From = 0.05, To = 0.15, Duration = TimeSpan.FromSeconds(3), AutoReverse = true, RepeatBehavior = RepeatBehavior.Forever };
            Glow_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, liquidGlow_XWaNTeD);

            bool bayad_up_she_XWaNTeD = await BaresiUpdate_XWaNTeD();
            if (bayad_up_she_XWaNTeD) return;

            await BaresiVoroodKhodkar_XWaNTeD();
        }

        private async Task<bool> BaresiUpdate_XWaNTeD()
        {
            try
            {
                long alan_zamane_XWaNTeD = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                string linke_up_XWaNTeD = $"http://YOUR_DOMAIN_HERE/version.json?t={alan_zamane_XWaNTeD}";
                string javabe_json_XWaNTeD = await client_XWaNTeDDev.GetStringAsync(linke_up_XWaNTeD);

                using (JsonDocument doc_XWaNTeD = JsonDocument.Parse(javabe_json_XWaNTeD))
                {
                    JsonElement rishe_XWaNTeD = doc_XWaNTeD.RootElement;
                    string akharin_ver_XWaNTeD = rishe_XWaNTeD.GetProperty("version").GetString() ?? "1.0.0";
                    string linke_dl_XWaNTeD = rishe_XWaNTeD.GetProperty("url").GetString() ?? "";

                    bool bayad_up_she_XWaNTeD = false;
                    if (Version.TryParse(akharin_ver_XWaNTeD, out Version? ver_jadid_XWaNTeD) && Version.TryParse(CURRENT_VERSION_XWaNTeD, out Version? ver_alan_XWaNTeD))
                    {
                        bayad_up_she_XWaNTeD = ver_jadid_XWaNTeD > ver_alan_XWaNTeD;
                    }
                    else
                    {
                        bayad_up_she_XWaNTeD = akharin_ver_XWaNTeD != CURRENT_VERSION_XWaNTeD;
                    }

                    if (bayad_up_she_XWaNTeD && !string.IsNullOrEmpty(linke_dl_XWaNTeD))
                    {
                        SafheUpdate_XWaNTeD.Visibility = Visibility.Visible;
                        await BebinUpdateBia_XWaNTeD(linke_dl_XWaNTeD);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }

        private async Task BebinUpdateBia_XWaNTeD(string linke_dl_XWaNTeD)
        {
            try
            {
                string pooshe_XWaNTeD = AppDomain.CurrentDomain.BaseDirectory;
                string file_movaghat_XWaNTeD = System.IO.Path.Combine(pooshe_XWaNTeD, "update_temp.exe");
                
                string masir_asli_XWaNTeD = Environment.ProcessPath ?? System.Reflection.Assembly.GetExecutingAssembly().Location;
                string masir_bat_XWaNTeD = System.IO.Path.Combine(pooshe_XWaNTeD, "updater.bat");

                long alan_zamane_XWaNTeD = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                string linke_dl_safe_XWaNTeD = linke_dl_XWaNTeD.Contains("?") ? $"{linke_dl_XWaNTeD}&t={alan_zamane_XWaNTeD}" : $"{linke_dl_XWaNTeD}?t={alan_zamane_XWaNTeD}";

                byte[] byte_ha_XWaNTeD = await client_XWaNTeDDev.GetByteArrayAsync(linke_dl_safe_XWaNTeD);
                File.WriteAllBytes(file_movaghat_XWaNTeD, byte_ha_XWaNTeD);

                VazeiatUpdate_XWaNTeD.Text = "دانلود شد داش، الان ری‌استارت میشم...";
                await Task.Delay(1000);

                int pid_XWaNTeD = Process.GetCurrentProcess().Id;

                string dastoore_bat_XWaNTeD = $@"@echo off
:WaitLoop_XWaNTeD
taskkill /F /PID {pid_XWaNTeD} > NUL 2>&1
del /f /q ""{masir_asli_XWaNTeD}""
if exist ""{masir_asli_XWaNTeD}"" (
    timeout /t 1 /nobreak > NUL
    goto WaitLoop_XWaNTeD
)
move /y ""{file_movaghat_XWaNTeD}"" ""{masir_asli_XWaNTeD}""
start """" ""{masir_asli_XWaNTeD}""
del ""%~f0""";

                File.WriteAllText(masir_bat_XWaNTeD, dastoore_bat_XWaNTeD);

                ProcessStartInfo psi_XWaNTeD = new ProcessStartInfo()
                {
                    FileName = masir_bat_XWaNTeD,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "runas" 
                };
                Process.Start(psi_XWaNTeD);
                Application.Current.Shutdown();
            }
            catch
            {
                SafheUpdate_XWaNTeD.Visibility = Visibility.Collapsed;
            }
        }

        private async Task BaresiVoroodKhodkar_XWaNTeD()
        {
            if (File.Exists(masir_token_XWaNTeD))
            {
                try
                {
                    string token_XWaNTeD = File.ReadAllText(masir_token_XWaNTeD);
                    await ErtbatBaApi_XWaNTeD(token_XWaNTeD, false);
                }
                catch { File.Delete(masir_token_XWaNTeD); }
            }
        }

        private async void ZadanDokmeVorood_XWaNTeD(object sender, RoutedEventArgs e)
        {
            DokmeVorood_XWaNTeD.IsEnabled = false;
            KhatayeVorood_XWaNTeD.Text = "مشتی برو تو مرورگر تایید کن و برگرد...";
            KhatayeVorood_XWaNTeD.Foreground = new SolidColorBrush(Colors.Gray);
            KhatayeVorood_XWaNTeD.Visibility = Visibility.Visible;

            if (gosh_bede_XWaNTeD != null && gosh_bede_XWaNTeD.IsListening)
            {
                gosh_bede_XWaNTeD.Stop();
            }

            gosh_bede_XWaNTeD = new HttpListener();
            gosh_bede_XWaNTeD.Prefixes.Add("http://127.0.0.1:48211/");

            try
            {
                gosh_bede_XWaNTeD.Start();
                Process.Start(new ProcessStartInfo("http://YOUR_DOMAIN_HERE/?p=app_login") { UseShellExecute = true });

                var etesal_XWaNTeD = await gosh_bede_XWaNTeD.GetContextAsync();
                var darkhast_XWaNTeD = etesal_XWaNTeD.Request;
                string? token_daryafti_XWaNTeD = darkhast_XWaNTeD.QueryString["token"];

                string html_XWaNTeD = """
                <!DOCTYPE html>
                <html lang="fa" dir="rtl">
                <head>
                    <meta charset="utf-8">
                    <title>بپر - تایید ورود</title>
                    <style>
                        @import url('https://fonts.googleapis.com/css2?family=Vazirmatn:wght@400;700;900&display=swap');
                        body { background: #060608; color: #fff; font-family: 'Vazirmatn', Tahoma, sans-serif; display: flex; justify-content: center; align-items: center; height: 100vh; margin: 0; overflow: hidden; }
                        .card { background: rgba(15,15,20,0.8); border: 1px solid rgba(138,43,226,0.3); border-radius: 24px; padding: 40px; text-align: center; box-shadow: 0 20px 50px rgba(138,43,226,0.2); animation: popIn 0.6s cubic-bezier(0.175, 0.885, 0.32, 1.275); position: relative; z-index: 10; max-width: 400px; width: 90%; }
                        .glow { position: absolute; width: 300px; height: 300px; background: radial-gradient(circle, rgba(138,43,226,0.4) 0%, transparent 70%); top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 1; pointer-events: none; }
                        @keyframes popIn { 0% { transform: scale(0.8); opacity: 0; } 100% { transform: scale(1); opacity: 1; } }
                        .icon { width: 80px; height: 80px; background: rgba(74,222,128,0.1); border-radius: 50%; display: flex; justify-content: center; align-items: center; margin: 0 auto 20px; border: 2px solid #4ADE80; box-shadow: 0 0 20px rgba(74,222,128,0.4); animation: pulse 2s infinite; }
                        @keyframes pulse { 0% { box-shadow: 0 0 0 0 rgba(74,222,128,0.4); } 70% { box-shadow: 0 0 0 20px rgba(74,222,128,0); } 100% { box-shadow: 0 0 0 0 rgba(74,222,128,0); } }
                        svg { width: 40px; height: 40px; stroke: #4ADE80; stroke-width: 3; fill: none; stroke-linecap: round; stroke-linejoin: round; }
                        h1 { font-size: 26px; margin: 0 0 10px; color: #fff; font-weight: 900; }
                        p { font-size: 15px; color: #8A8A93; margin-bottom: 30px; line-height: 1.8; }
                        .btn { display: inline-block; background: linear-gradient(135deg, #8A2BE2 0%, #4A0E8F 100%); padding: 14px 35px; border-radius: 14px; color: #fff; text-decoration: none; font-weight: bold; cursor: pointer; transition: all 0.3s; border: none; font-family: inherit; font-size: 15px; outline: none; }
                        .btn:hover { box-shadow: 0 8px 25px rgba(138,43,226,0.5); transform: translateY(-2px); }
                    </style>
                </head>
                <body>
                    <div class="glow"></div>
                    <div class="card">
                        <div class="icon">
                            <svg viewBox="0 0 24 24"><polyline points="20 6 9 17 4 12"></polyline></svg>
                        </div>
                        <h1>بنازم! لاگین شدی رفت 🚀</h1>
                        <p>اکانتت با موفقیت وصل شد ستون.<br>الان میتونی این تب رو با خیال راحت ببندی و برگردی تو برنامه تا عشق کنی.</p>
                        <button class="btn" onclick="window.close()">بستن این صفحه</button>
                    </div>
                    <script>setTimeout(function(){window.close();}, 5000);</script>
                </body>
                </html>
                """;
                
                byte[] buffer_XWaNTeD = System.Text.Encoding.UTF8.GetBytes(html_XWaNTeD);
                etesal_XWaNTeD.Response.ContentLength64 = buffer_XWaNTeD.Length;
                var khorooji_XWaNTeD = etesal_XWaNTeD.Response.OutputStream;
                await khorooji_XWaNTeD.WriteAsync(buffer_XWaNTeD, 0, buffer_XWaNTeD.Length);
                khorooji_XWaNTeD.Close();
                gosh_bede_XWaNTeD.Stop();

                if (!string.IsNullOrEmpty(token_daryafti_XWaNTeD))
                {
                    KhatayeVorood_XWaNTeD.Text = "توکن دریافت شد، در حال اتصال...";
                    await ErtbatBaApi_XWaNTeD(token_daryafti_XWaNTeD, true);
                }
                else
                {
                    KhatayeVorood_XWaNTeD.Text = "توکنی دریافت نشد، دوباره امتحان کن!";
                    KhatayeVorood_XWaNTeD.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4444"));
                    DokmeVorood_XWaNTeD.IsEnabled = true;
                }
            }
            catch (HttpListenerException)
            {
                DokmeVorood_XWaNTeD.IsEnabled = true;
            }
            catch (Exception)
            {
                KhatayeVorood_XWaNTeD.Text = "ارتباط با مرورگر برقرار نشد!";
                KhatayeVorood_XWaNTeD.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4444"));
                DokmeVorood_XWaNTeD.IsEnabled = true;
            }
        }

        private async Task ErtbatBaApi_XWaNTeD(string token_XWaNTeD, bool zakhire_she_XWaNTeD)
        {
            try
            {
                string apiUrl_XWaNTeD = "http://YOUR_DOMAIN_HERE/api.php";

                var body_XWaNTeD = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("token", token_XWaNTeD)
                });

                HttpResponseMessage javab_XWaNTeD = await client_XWaNTeDDev.PostAsync(apiUrl_XWaNTeD, body_XWaNTeD);
                string matn_javab_XWaNTeD = await javab_XWaNTeD.Content.ReadAsStringAsync();

                using (JsonDocument doc_XWaNTeD = JsonDocument.Parse(matn_javab_XWaNTeD))
                {
                    JsonElement rishe_XWaNTeD = doc_XWaNTeD.RootElement;
                    string vaz_XWaNTeD = rishe_XWaNTeD.GetProperty("status").GetString() ?? "";
                    string payam_XWaNTeD = rishe_XWaNTeD.GetProperty("message").GetString() ?? "";

                    if (vaz_XWaNTeD == "success")
                    {
                        int rooz_mande_XWaNTeD = rishe_XWaNTeD.GetProperty("days_left").GetInt32();
                        Etebar_XWaNTeD.Text = $"اشتراک: {rooz_mande_XWaNTeD} روز مونده";

                        if (zakhire_she_XWaNTeD)
                        {
                            File.WriteAllText(masir_token_XWaNTeD, token_XWaNTeD);
                        }

                        DoubleAnimation fadeOutLogin_XWaNTeD = new DoubleAnimation { To = 0, Duration = TimeSpan.FromSeconds(0.6), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
                        fadeOutLogin_XWaNTeD.Completed += (s, ev) => { SafheVorood_XWaNTeD.Visibility = Visibility.Collapsed; };
                        SafheVorood_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, fadeOutLogin_XWaNTeD);
                    }
                    else
                    {
                        KhatayeVorood_XWaNTeD.Text = payam_XWaNTeD;
                        KhatayeVorood_XWaNTeD.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4444"));
                        if (File.Exists(masir_token_XWaNTeD)) File.Delete(masir_token_XWaNTeD);
                        DokmeVorood_XWaNTeD.IsEnabled = true;
                    }
                }
            }
            catch
            {
                KhatayeVorood_XWaNTeD.Text = "ارتباط با سرور سایت قطعه، نتت رو چک کن!";
                KhatayeVorood_XWaNTeD.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4444"));
                DokmeVorood_XWaNTeD.IsEnabled = true;
            }
        }

        private void RoshanKardanMotor_XWaNTeD()
        {
            string pooshe_XWaNTeD = AppDomain.CurrentDomain.BaseDirectory;
            string masir_core_XWaNTeD = System.IO.Path.Combine(pooshe_XWaNTeD, "sing-box.exe");

            if (!File.Exists(masir_core_XWaNTeD))
            {
                MessageBox.Show("Please ensure sing-box.exe is in the application folder.", "Core Missing", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!File.Exists(System.IO.Path.Combine(pooshe_XWaNTeD, "wintun.dll")))
            {
                MessageBox.Show("Please ensure wintun.dll is in the application folder.", "Driver Missing", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string ip_server_XWaNTeD = "YOUR_SERVER_IP";
            string key_server_XWaNTeD = "YOUR_REALITY_PUBLIC_KEY";
            string uuid_server_XWaNTeD = "YOUR_UUID_HERE";
            string sni_server_XWaNTeD = "YOUR_SNI_DOMAIN";
            string short_id_server_XWaNTeD = "YOUR_SHORT_ID";

            string json_dns_XWaNTeD = halate_bazi_XWaNTeD ?
                @"{
                  ""servers"": [
                    { ""tag"": ""remote-dns"", ""address"": ""1.1.1.1"", ""detour"": ""proxy"" },
                    { ""tag"": ""local-dns"", ""address"": ""local"", ""detour"": ""direct"" }
                  ],
                  ""rules"": [
                    { ""domain_suffix"": [ ""example-game-domain.com"" ], ""server"": ""remote-dns"" },
                    { ""outbound"": ""any"", ""server"": ""local-dns"" }
                  ],
                  ""independent_cache"": true,
                  ""strategy"": ""ipv4_only""
                }"
                :
                @"{
                  ""servers"": [ { ""tag"": ""remote-dns"", ""address"": ""1.1.1.1"", ""detour"": ""proxy"" } ],
                  ""rules"": [ { ""outbound"": ""any"", ""server"": ""remote-dns"" } ],
                  ""independent_cache"": true,
                  ""strategy"": ""ipv4_only""
                }";

            string ghavanin_masir_XWaNTeD = halate_bazi_XWaNTeD ?
                @"{ ""network"": [""udp""], ""outbound"": ""proxy"" },
                  { ""domain_suffix"": [ ""example-game-domain.com"" ], ""outbound"": ""proxy"" },
                  { ""ip_is_private"": true, ""outbound"": ""direct"" },
                  { ""outbound"": ""direct"" }"
                :
                @"{ ""ip_is_private"": true, ""outbound"": ""direct"" },
                  { ""outbound"": ""proxy"" }";

            string json_kolli_XWaNTeD = $$"""
            {
              "log": { "level": "error" },
              "dns": {{json_dns_XWaNTeD}},
              "inbounds": [
                {
                  "type": "tun",
                  "tag": "tun-in",
                  "interface_name": "BePaRTun",
                  "inet4_address": "172.19.0.1/30",
                  "mtu": 1420,
                  "auto_route": true,
                  "strict_route": true,
                  "endpoint_independent_nat": true,
                  "stack": "system",
                  "sniff": true
                }
              ],
              "outbounds": [
                {
                  "type": "vless",
                  "tag": "proxy",
                  "server": "{{ip_server_XWaNTeD}}",
                  "server_port": 443,
                  "uuid": "{{uuid_server_XWaNTeD}}",
                  "flow": "xtls-rprx-vision",
                  "network": "tcp",
                  "tls": {
                    "enabled": true,
                    "server_name": "{{sni_server_XWaNTeD}}",
                    "utls": { "enabled": true, "fingerprint": "chrome" },
                    "reality": { "enabled": true, "public_key": "{{key_server_XWaNTeD}}", "short_id": "{{short_id_server_XWaNTeD}}" }
                  }
                },
                { "type": "direct", "tag": "direct" },
                { "type": "dns", "tag": "dns-out" }
              ],
              "route": {
                "rules": [
                  { "protocol": "dns", "outbound": "dns-out" },
                  {{ghavanin_masir_XWaNTeD}}
                ],
                "auto_detect_interface": true
              }
            }
            """;

            motor_XWaNTeD = new Process();
            motor_XWaNTeD.StartInfo.FileName = masir_core_XWaNTeD;
            motor_XWaNTeD.StartInfo.Arguments = "run -c stdin"; 
            motor_XWaNTeD.StartInfo.WorkingDirectory = pooshe_XWaNTeD;
            motor_XWaNTeD.StartInfo.UseShellExecute = false; 
            motor_XWaNTeD.StartInfo.RedirectStandardInput = true;
            motor_XWaNTeD.StartInfo.CreateNoWindow = true;
            motor_XWaNTeD.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            motor_XWaNTeD.Start();

            using (StreamWriter sw_XWaNTeD = motor_XWaNTeD.StandardInput)
            {
                sw_XWaNTeD.Write(json_kolli_XWaNTeD);
            }
        }

        private void KhamooshKardanMotor_XWaNTeD()
        {
            try
            {
                if (motor_XWaNTeD != null && !motor_XWaNTeD.HasExited)
                {
                    motor_XWaNTeD.Kill();
                    motor_XWaNTeD.Dispose();
                }
                foreach (var proc_XWaNTeD in Process.GetProcessesByName("sing-box"))
                {
                    proc_XWaNTeD.Kill();
                }
                
                string masir_ghadimi_XWaNTeD = AppDomain.CurrentDomain.BaseDirectory + "config.json";
                if (File.Exists(masir_ghadimi_XWaNTeD)) File.Delete(masir_ghadimi_XWaNTeD);
            }
            catch { }
        }

        protected override void OnClosed(EventArgs e)
        {
            KhamooshKardanMotor_XWaNTeD();
            base.OnClosed(e);
            Environment.Exit(0);
        }

        private void HarekatMoosh_XWaNTeD(object sender, MouseEventArgs e)
        {
            if (Ghab_XWaNTeD.ActualWidth == 0 || Ghab_XWaNTeD.ActualHeight == 0) return;
            Point noghte_XWaNTeD = e.GetPosition(Ghab_XWaNTeD);
            double X_nesbi_XWaNTeD = Math.Max(0, Math.Min(1, noghte_XWaNTeD.X / Ghab_XWaNTeD.ActualWidth));
            double Y_nesbi_XWaNTeD = Math.Max(0, Math.Min(1, noghte_XWaNTeD.Y / Ghab_XWaNTeD.ActualHeight));
            SpotlightBrush.Center = new Point(X_nesbi_XWaNTeD, Y_nesbi_XWaNTeD);
            SpotlightBrush.GradientOrigin = new Point(X_nesbi_XWaNTeD, Y_nesbi_XWaNTeD);
        }

        private void BalaGereftan_XWaNTeD(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void BastanBarname_XWaNTeD(object sender, RoutedEventArgs e)
        {
            KhamooshKardanMotor_XWaNTeD();
            Application.Current.Shutdown();
        }

        private void KoochikKardan_XWaNTeD(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ZadanTaghirHalat_XWaNTeD(object sender, RoutedEventArgs e)
        {
            if (vasl_hastim_XWaNTeD || dare_mikhore_XWaNTeD) return;

            halate_bazi_XWaNTeD = !halate_bazi_XWaNTeD;
            System.Windows.Shapes.Path? shekl_halat_XWaNTeD = (System.Windows.Shapes.Path?)TaghirHalat_XWaNTeD.Template.FindName("IconHalat_XWaNTeD", TaghirHalat_XWaNTeD);

            if (halate_bazi_XWaNTeD)
            {
                _ = EivazKardanMatn_XWaNTeD(MatnHalat_XWaNTeD, "گیمینگ مشتی", "#EEEEEE");
                if (shekl_halat_XWaNTeD != null) shekl_halat_XWaNTeD.Data = Geometry.Parse("M2 12 C2 7 5 4 12 4 C19 4 22 7 22 12 C22 17 19 20 17 20 C15 20 14 17 12 17 C10 17 9 20 7 20 C5 20 2 17 2 12 Z M7 9 L7 15 M4 12 L10 12 M15 10 A1 1 0 1 0 15 10.01 M19 14 A1 1 0 1 0 19 14.01 M19 10 A1 1 0 1 0 19 10.01 M15 14 A1 1 0 1 0 15 14.01");
            }
            else
            {
                _ = EivazKardanMatn_XWaNTeD(MatnHalat_XWaNTeD, "نت آزاد مشتی", "#EEEEEE");
                if (shekl_halat_XWaNTeD != null) shekl_halat_XWaNTeD.Data = Geometry.Parse("M12 22C17.5228 22 22 17.5228 22 12C22 6.47715 17.5228 2 12 2C6.47715 2 2 6.47715 2 12C2 17.5228 6.47715 22 12 22Z M2 12H22 M12 2C14.5013 4.73835 15.9228 8.29203 16 12C15.9228 15.708 14.5013 19.2616 12 22C9.49872 19.2616 8.07725 15.708 8 12C8.07725 8.29203 9.49872 4.73835 12 2Z");
            }
        }

        private async Task EivazKardanMatn_XWaNTeD(TextBlock onsor_XWaNTeD, string matn_jadid_XWaNTeD, string rang_hex_XWaNTeD)
        {
            DoubleAnimation mahv_XWaNTeD = new DoubleAnimation { To = 0, Duration = TimeSpan.FromMilliseconds(150) };
            onsor_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, mahv_XWaNTeD);
            await Task.Delay(150);
            onsor_XWaNTeD.Text = matn_jadid_XWaNTeD;
            onsor_XWaNTeD.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(rang_hex_XWaNTeD));
            DoubleAnimation peyda_XWaNTeD = new DoubleAnimation { To = 1, Duration = TimeSpan.FromMilliseconds(200) };
            onsor_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, peyda_XWaNTeD);
        }

        private async void ZadanDokmeVasl_XWaNTeD(object sender, RoutedEventArgs e)
        {
            if (dare_mikhore_XWaNTeD) return;
            dare_mikhore_XWaNTeD = true;
            vasl_hastim_XWaNTeD = !vasl_hastim_XWaNTeD;

            RotateTransform charkh_XWaNTeD = (RotateTransform)((Image)DokmeAsli_XWaNTeD.Template.FindName("LogoDokme_XWaNTeD", DokmeAsli_XWaNTeD)).RenderTransform;
            System.Windows.Shapes.Path? shekl_halat_XWaNTeD = (System.Windows.Shapes.Path?)TaghirHalat_XWaNTeD.Template.FindName("IconHalat_XWaNTeD", TaghirHalat_XWaNTeD);

            if (vasl_hastim_XWaNTeD)
            {
                _ = EivazKardanMatn_XWaNTeD(MatnVazeiat_XWaNTeD, "داری وصل میشی عامو یه نمه صبر کن...", "#AAAAAA");

                DoubleAnimation bepayin_XWaNTeD = new DoubleAnimation { To = 0.9, Duration = TimeSpan.FromMilliseconds(300), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleXProperty, bepayin_XWaNTeD);
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleYProperty, bepayin_XWaNTeD);

                DoubleAnimation charkhidan_XWaNTeD = new DoubleAnimation { By = 720, Duration = TimeSpan.FromMilliseconds(800), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut } };
                charkh_XWaNTeD.BeginAnimation(RotateTransform.AngleProperty, charkhidan_XWaNTeD);

                await Task.Delay(600);

                RoshanKardanMotor_XWaNTeD();

                Moje_XWaNTeD.Opacity = 0.5;
                DoubleAnimation moge_bozorg_XWaNTeD = new DoubleAnimation { From = 1, To = 3.5, Duration = TimeSpan.FromMilliseconds(500), EasingFunction = new QuarticEase { EasingMode = EasingMode.EaseOut } };
                DoubleAnimation moge_mahv_XWaNTeD = new DoubleAnimation { From = 0.5, To = 0, Duration = TimeSpan.FromMilliseconds(500) };
                ShockwaveScale.BeginAnimation(ScaleTransform.ScaleXProperty, moge_bozorg_XWaNTeD);
                ShockwaveScale.BeginAnimation(ScaleTransform.ScaleYProperty, moge_bozorg_XWaNTeD);
                Moje_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, moge_mahv_XWaNTeD);

                DoubleAnimation bebala_XWaNTeD = new DoubleAnimation { To = 1.05, Duration = TimeSpan.FromMilliseconds(300), EasingFunction = new BackEase { Amplitude = 0.5, EasingMode = EasingMode.EaseOut } };
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleXProperty, bebala_XWaNTeD);
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleYProperty, bebala_XWaNTeD);

                DoubleAnimation glow_roshan_XWaNTeD = new DoubleAnimation { To = 1, Duration = TimeSpan.FromSeconds(0.4) };
                DoubleAnimation glow_bozorg_XWaNTeD = new DoubleAnimation { To = 1.2, Duration = TimeSpan.FromSeconds(0.4) };
                GlowMarkaz_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, glow_roshan_XWaNTeD);
                CenterGlowScale.BeginAnimation(ScaleTransform.ScaleXProperty, glow_bozorg_XWaNTeD);
                CenterGlowScale.BeginAnimation(ScaleTransform.ScaleYProperty, glow_bozorg_XWaNTeD);

                string matn_vasl_XWaNTeD = halate_bazi_XWaNTeD ? "پینگت فضایی شد داش، برو بترکون :)" : "هی خوشتیپ، وصل شد برو عشق کن :)";
                string matn_server_XWaNTeD = halate_bazi_XWaNTeD ? "خاورمیانه (VIP)" : "اروپا (بدون فیلتر)";

                _ = EivazKardanMatn_XWaNTeD(MatnVazeiat_XWaNTeD, matn_vasl_XWaNTeD, "#B026FF");
                _ = EivazKardanMatn_XWaNTeD(MatnServer_XWaNTeD, matn_server_XWaNTeD, "#FFFFFF");

                IconServer_XWaNTeD.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A2BE2"));
                if (shekl_halat_XWaNTeD != null) shekl_halat_XWaNTeD.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B026FF"));
            }
            else
            {
                KhamooshKardanMotor_XWaNTeD();

                DoubleAnimation bepayin_XWaNTeD = new DoubleAnimation { To = 0.95, Duration = TimeSpan.FromMilliseconds(200) };
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleXProperty, bepayin_XWaNTeD);
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleYProperty, bepayin_XWaNTeD);

                DoubleAnimation bargasht_XWaNTeD = new DoubleAnimation { By = -360, Duration = TimeSpan.FromMilliseconds(500), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
                charkh_XWaNTeD.BeginAnimation(RotateTransform.AngleProperty, bargasht_XWaNTeD);

                await Task.Delay(200);

                DoubleAnimation popUp = new DoubleAnimation { To = 1, Duration = TimeSpan.FromMilliseconds(300), EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } };
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleXProperty, popUp);
                BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleYProperty, popUp);

                DoubleAnimation glow_mahv_XWaNTeD = new DoubleAnimation { To = 0, Duration = TimeSpan.FromSeconds(0.4) };
                DoubleAnimation glow_koochik_XWaNTeD = new DoubleAnimation { To = 0.8, Duration = TimeSpan.FromSeconds(0.4) };
                GlowMarkaz_XWaNTeD.BeginAnimation(UIElement.OpacityProperty, glow_mahv_XWaNTeD);
                CenterGlowScale.BeginAnimation(ScaleTransform.ScaleXProperty, glow_koochik_XWaNTeD);
                CenterGlowScale.BeginAnimation(ScaleTransform.ScaleYProperty, glow_koochik_XWaNTeD);

                _ = EivazKardanMatn_XWaNTeD(MatnVazeiat_XWaNTeD, "قطعه حاجی، دکمه رو بزن بریم!", "#777777");
                _ = EivazKardanMatn_XWaNTeD(MatnServer_XWaNTeD, "فعلا تو گاراژیم", "#EEEEEE");

                IconServer_XWaNTeD.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555555"));
                if (shekl_halat_XWaNTeD != null) shekl_halat_XWaNTeD.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#555555"));
            }

            await Task.Delay(300);
            dare_mikhore_XWaNTeD = false;
        }

        private void PlayButtonBounceAnimation()
        {
            DoubleAnimation scaleDown = new DoubleAnimation { To = 0.85, Duration = TimeSpan.FromMilliseconds(120), AutoReverse = true };
            BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleXProperty, scaleDown);
            BozorgiDokme_XWaNTeD.BeginAnimation(ScaleTransform.ScaleYProperty, scaleDown);

            DoubleAnimation logoPulse = new DoubleAnimation { To = vasl_hastim_XWaNTeD ? 1.0 : 0.5, Duration = TimeSpan.FromSeconds(0.4) };
            Image? logo = (Image)DokmeAsli_XWaNTeD.Template.FindName("LogoDokme_XWaNTeD", DokmeAsli_XWaNTeD);
            if (logo != null) logo.BeginAnimation(UIElement.OpacityProperty, logoPulse);
        }
    }
}