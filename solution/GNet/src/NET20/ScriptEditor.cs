﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace GNet
{
    public partial class ScriptEditor : UserControl
    {
        IScriptRunner scriptRunner;

        public ScriptEditor(string content)
        {
            InitializeComponent();

            HighlightingManager.Manager.AddSyntaxModeFileProvider(new ResourceSyntaxModeProvider());
            var h = HighlightingManager.Manager.FindHighlighter("Lua");
            if (h != null)
            {
                editor.SetHighlighting("Lua");
                HighlightingManager.Manager.AddHighlightingStrategy(h);
            }

            editor.Text = content;

//            if (File.Exists(@".\Profiles\Lua\_default.lua"))
//            {
//                using (var fs = File.OpenText(@".\Profiles\Lua\_default.lua"))
//                {
//                    editor.Text = fs.ReadToEnd();
//                }
//            }
//            else
//            {
//                editor.Text =
//@"-- Name:
//-- Description:
//-- Executables:
//
//function OnEvent(event, arg, family)
//    OutputLogMessage(event.."" : ""..arg.."" : ""..family)
//end
//";
//            }

            scriptRunner = new LuaRunner();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            StopScript();
            base.OnControlRemoved(e);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            runButton.Enabled = false;
            stopButton.Enabled = true;

            try
            {
                editor.IsReadOnly = true;
                scriptRunner.Run(editor.Text);
            }
            catch (Exception ex)
            {
                StopScript();
                Console.WriteLine(ex.ToString());
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            StopScript();
        }

        public void StopScript()
        {
            if (scriptRunner != null)
            {
                scriptRunner.Stop();
            }

            runButton.Enabled = true;
            stopButton.Enabled = false;
            editor.IsReadOnly = false;
        }
    }
}
