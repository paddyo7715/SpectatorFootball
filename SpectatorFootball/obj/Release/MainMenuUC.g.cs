﻿#pragma checksum "..\..\MainMenuUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0C425C6D6BDD647C175868D96B7D1E0D4EFDCFD9173E86DBE13541472C3DE94C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SpectatorFootball;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SpectatorFootball {
    
    
    /// <summary>
    /// MainMenuUC
    /// </summary>
    public partial class MainMenuUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\MainMenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mmNew;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\MainMenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mmLoad;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MainMenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mmAdmin;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\MainMenuUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mmExit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SpectatorFootball;component/mainmenuuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainMenuUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mmNew = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\MainMenuUC.xaml"
            this.mmNew.Click += new System.Windows.RoutedEventHandler(this.mmNew_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mmLoad = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\MainMenuUC.xaml"
            this.mmLoad.Click += new System.Windows.RoutedEventHandler(this.mmLoad_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.mmAdmin = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\MainMenuUC.xaml"
            this.mmAdmin.Click += new System.Windows.RoutedEventHandler(this.mmOptions_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.mmExit = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\MainMenuUC.xaml"
            this.mmExit.Click += new System.Windows.RoutedEventHandler(this.mmExit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

