//------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace KinectPhotobooth
{
    using Microsoft.Kinect.Wpf.Controls;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App
    /// </summary>
    public partial class App : Application
    {
        internal KinectRegion KinectRegion { get; set; }
    }
}
