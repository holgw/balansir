using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BalansirApp.ViewModels.Common
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public virtual string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        protected bool InformPropertyChanged<T>(T oldValue, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(oldValue, value))
                return false;

            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = this.PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Метод, сигнализирующий об обновлении всех полей модели,
        /// помеченных атрибутом [FormField]
        /// </summary>
        protected void NotifyAllFiledsChanged()
        {
            var changed = this.PropertyChanged;
            if (changed == null)
                return;

            var props = this.GetType()
                .GetProperties()
                .Where(prop => prop.IsDefined(typeof(FormFieldAttribute), false))
                .ToArray();

            foreach (var prop in props)
            {
                string propName = prop.Name;
                changed.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}
