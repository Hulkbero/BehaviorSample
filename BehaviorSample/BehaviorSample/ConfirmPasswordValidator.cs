using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BehaviorSample
{
    public class ConfirmPasswordValidator : Behavior<Entry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(ConfirmPasswordValidator), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public static readonly BindableProperty ConfirmToProperty = BindableProperty.Create("ConfirmTo", typeof(Entry), typeof(ConfirmPasswordValidator), null);

        public Entry ConfirmTo
        {
            get { return (Entry)base.GetValue(ConfirmToProperty); }
            set { base.SetValue(ConfirmToProperty, value); }
        }
        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var password = ConfirmTo.Text;
            var confirmPassword = e.NewTextValue;
            IsValid = password.Equals(confirmPassword);
            ((Entry)sender).BackgroundColor = IsValid ? Color.Default : Color.Red;
        }
    }
}
