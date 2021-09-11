﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MoviesTT.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public BaseViewModel(INavigation navigation)
        {
            Navigation = navigation;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;

            backingField = value;
            OnPropertyChanged(propertyName);
        }
    }
}
