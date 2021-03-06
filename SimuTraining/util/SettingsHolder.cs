﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SimuTraining.util
{
    public class SettingsHolder
    {
        public string fontSize
        {
            get 
            {
                return ConfigurationManager.AppSettings["fontSize"].ToString();
            }
        }

        public string fontFamily
        {
            get
            {
                return ConfigurationManager.AppSettings["fontFamily"].ToString();
            }
        }

        public void Refresh()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
