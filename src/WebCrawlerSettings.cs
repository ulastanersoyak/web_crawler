﻿// Ignore Spelling: webcrawler
//9.5 out of 10
using System;
using System.IO;
using Newtonsoft.Json;
using webcrawler.Logs;
namespace WebCrawler.Settings
{
    public class WebCrawlerSettings
    {
        private readonly string settingsPath = AppDomain.CurrentDomain.BaseDirectory + "settings.json";
        readonly Logger logger = new Logger();
        private const int DefaultMaxDepth = 3;
        private const int DefaultMaxUrl = 100;
        private const int DefaultDelay = 1000;
        public int MaxDepth { get; set; }
        public int MaxUrl { get; set; }
        public int Delay { get; set; }
        public WebCrawlerSettings()
        {
            SetDefaults();
        }
        public WebCrawlerSettings(int maxDepth, int maxUrl, int delay)
        {
            MaxDepth = maxDepth > 0 ? maxDepth : DefaultMaxDepth;
            MaxUrl = maxUrl > 0 ? maxUrl : DefaultMaxUrl;
            Delay = delay > 0 ? delay : DefaultDelay;
            ValidateSettings();
        }
        public void SetDefaults()
        {
            MaxDepth = DefaultMaxDepth;
            MaxUrl = DefaultMaxUrl;
            Delay = DefaultDelay;
            ValidateSettings();
        }
        public void SaveSettings()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(settingsPath, json);
                logger.Info("Settings saved successfully");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        public void LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                try
                {
                    string json = File.ReadAllText(settingsPath);
                    WebCrawlerSettings loadedSettings = JsonConvert.DeserializeObject<WebCrawlerSettings>(json);
                    if (loadedSettings != null)
                    {
                        this.MaxDepth = loadedSettings.MaxDepth;
                        this.MaxUrl = loadedSettings.MaxUrl;
                        this.Delay = loadedSettings.Delay;
                    }
                    else
                    {
                        logger.Warning("Result of de-serialization of settings.json was null.");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            else
            {
                SetDefaults();
            }
        }
        private void ValidateSettings()
        {
            if (MaxDepth <= 0)
            {
                MaxDepth = DefaultMaxDepth;
                logger.Warning("MaxDepth cannot be 0 or negative. Using default value.");
            }
            if (MaxUrl <= 0)
            {
                MaxUrl = DefaultMaxUrl;
                logger.Warning("MaxUrl cannot be 0 or negative. Using default value.");
            }
            if (Delay <= 0)
            {
                Delay = DefaultDelay;
                logger.Warning("Delay cannot be 0 or negative. Using default value.");
            }
        }
    }
}
