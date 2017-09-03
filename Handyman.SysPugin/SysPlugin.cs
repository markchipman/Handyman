﻿using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Handyman.Framework.Entities;

namespace Handyman.SysPugin {
    public class SysPlugin : IMaster {
        public SysPlugin() {
            _alias = "sys";
            _hotKey = Shortcut.None;
        }

        public string Name => "System Plugin";
        public string Description => "Execute system commands.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.SysPlugin/README.MD";

        private Shortcut _hotKey;
        private string _alias;


        Shortcut IMaster.HotKey {
            get => _hotKey;
            set => _hotKey = value;
        }

        string IMaster.Alias {
            get => _alias;
            set => _alias = value;
        }

        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        public void Execute(string[] args, Action<string, DisplayData> display = null) {
            var cmdArgs = "shutdown ";
            if (args[0] == "shutdown") {
                if (args.Length == 1) {
                    cmdArgs += "/s";
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/s /t " + (mins * 60);
                } 
            } else if (args[0] == "restart") {
                cmdArgs += "/r";
                if (args.Length == 1) {
                    cmdArgs += "/r";
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/r /t " + ( mins * 60 );
                }
            } else if (args[0] == "hibernate") {
                cmdArgs += "/h";
                if (args.Length == 1) {
                    cmdArgs += "/h";
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/h /t " + ( mins * 60 );
                }
            } else if (args[0] == "abort") {
                cmdArgs += "/a";
            } else if (args[0] == "logoff") {
                cmdArgs += "/l";
                if (args.Length == 1) {
                    cmdArgs += "/l";
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/l /t " + ( mins * 60 );
                }
            }

            var startInfo = new ProcessStartInfo {
                FileName = @"cmd.exe",
                Arguments = cmdArgs,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = new Process { StartInfo = startInfo };
            process.Start();
        }

        public void Initialize() {
            Suggestions = new List<string> { "sys shutdown", "sys restart", "sys hibernate", "sys logoff", "sys abort"};
            //throw new NotImplementedException();
        }
    }
}
