using BalansirApp.Core.Common.DataAccess;
using System;
using Xamarin.Forms;

namespace BalansirApp.ViewModels.Common
{
    public class SettingsEdit_ViewModel : BaseViewModel
    {       
        private readonly ISettingsProvider _source;

        private int _historyDaysCount;
        private int _pageSize;

        public override string Title => "Настройки";

        [FormField] public int HistoryDaysCount
        {
            get => _historyDaysCount;
            set
            {
                int oldValue = _historyDaysCount;
                _historyDaysCount = value;
                InformPropertyChanged(oldValue, value);
            }
        }

        [FormField] public int PageSize
        {
            get => _pageSize;
            set
            {
                int oldValue = _pageSize;
                _pageSize = value;
                InformPropertyChanged(oldValue, value);
            }
        }

        // PROPS: Commands
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        // CTOR
        public SettingsEdit_ViewModel(ISettingsProvider source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));

            this.SaveCommand = new Command(OnSave);
            this.CancelCommand = new Command(OnCancel);

            this.HistoryDaysCount = _source.HistoryDaysCount;
            this.PageSize = _source.PageSize;
        }

        // METHODS: Private
        async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        async void OnSave()
        {
            _source.HistoryDaysCount = this.HistoryDaysCount;
            _source.PageSize = this.PageSize;

            await Shell.Current.DisplayAlert("Информация", "Настройки сохранены", "OK");
        }
    }
}
