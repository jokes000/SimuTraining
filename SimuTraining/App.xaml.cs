﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.IO;
using Microsoft.Win32;

using SimuTraining.windows;
using SimuTraining.util;
using System.Reflection;

namespace SimuTraining
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //SplashScreen sp = new SplashScreen("res/img/welcome.png");
            //sp.Show(true, true);
            //sp.Close(new TimeSpan(0, 0, 3));
            //Thread.Sleep(3000);

            //if (confirmAuthorization())
            //{
            //    Window login = new LoginWindow();
            //    login.Show();
            //}
            //else
            //{
            //    Window auth = new AuthWindow();
            //    auth.Show();

            MessageBox.Show(AuthUtil.generateAuthrizationCode("1"));
            MessageBox.Show(AuthUtil.generateAuthrizationCode("2"));
            MessageBox.Show(AuthUtil.generateAuthrizationCode("3"));
            MessageBox.Show(AuthUtil.generateAuthrizationCode("4"));
            MessageBox.Show(AuthUtil.generateAuthrizationCode("5"));
            MessageBox.Show(AuthUtil.generateAuthrizationCode("6"));
            MessageBox.Show(AuthUtil.generateAuthrizationCode("7"));

            Window index = new IndexWindow(BreadCrumb.getRoot());
            index.Show();

            

            base.OnStartup(e);
        }

        private Boolean confirmAuthorization()
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey SimuTraining = key.OpenSubKey("SOFTWARE\\SimuTraining");
            if (SimuTraining != null && SimuTraining.GetValue("authcode") != null)
            {
                String id = AuthUtil.getMachineID();
                return AuthUtil.authorize(id, SimuTraining.GetValue("authcode").ToString());
            }
            else
            {
                return false;
            }
        }

    }
}
