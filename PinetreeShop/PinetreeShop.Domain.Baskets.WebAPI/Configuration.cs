﻿using PinetreeUtilities.Configuration;

namespace PinetreeShop.Domain.Baskets.WebAPI
{
    public class Configuration : ConfigurationBase
    {
        private static Configuration _instance;
        public static Configuration Instance
        {
            get
            {
                if (_instance == null) _instance = new Configuration();
                return _instance;
            }
        }

        public string ClientUrl { get; protected set; }

        private Configuration() : base(new ConfigurationDictionary())
        {
        }
    }
}