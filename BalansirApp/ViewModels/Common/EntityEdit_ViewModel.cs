using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Utility.Results;
using System;
using Xamarin.Forms;
using Extensions = BalansirApp.Utility.Results.Extensions;

namespace BalansirApp.ViewModels.Common
{
    public abstract class EntityEdit_ViewModel<TRecord, TRecordView, TParam> : BaseViewModel
        where TRecord : DbRecord, new()
        where TRecordView : class, new()
        where TParam : BaseQueryParam
    {
        protected readonly IEntityService<TRecordView, TParam> _entityService;

        protected TRecordView _source;

        public abstract new string Title { get; }

        [FormField] public bool IsDeleteButtonVisible { get; private set; }
        [FormField] public bool IsSaveButtonVisible { get; private set; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }

        // CTOR
        public EntityEdit_ViewModel(IEntityService<TRecordView, TParam> entityService)
        {
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));

            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            DeleteCommand = new Command(OnDelete);

            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        // METHODS: Public
        public virtual void Setup(int? id = null, TRecordView entityView = null, bool notifyChanged = true)
        {
            if (id != null)
            {
                this.LoadItemId(id.Value);
                this.IsDeleteButtonVisible = true;
                this.IsSaveButtonVisible = true;
            }
            else
            {
                if (entityView == null)
                    entityView = this.BuildNewEntityView();

                _source = entityView;
                this.IsDeleteButtonVisible = false;
                this.IsSaveButtonVisible = true;
            }

            if (notifyChanged)
                this.NotifyAllFiledsChanged();
        }

        // METHODS: Protected
        protected virtual TRecordView BuildNewEntityView()
        {
            return new TRecordView();
        }
        protected virtual void SaveAction()
        {
            _entityService.SaveEntity(_source);
            this.NotifyParents();
        }
        protected virtual void DeleteAction()
        {
            _entityService.DeleteEntity(_source);
            this.NotifyParents();
        }
        protected abstract void NotifyParents();

        // METHODS: Private
        async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
        async void OnSave()
        {
            var result = Extensions.HandleMethod(() => this.SaveAction());
            await result.Handle();
        }
        async void LoadItemId(int itemId)
        {
            var result = Extensions.HandleMethod(() => _entityService.GetEntityView(itemId));
            if (result.IsSuccess)
            {
                _source = result.Object;
            }
            else
            {
                await result.DisplayAlert();
            }
        }
        async void OnDelete()
        {
            var result = Extensions.HandleMethod(() => this.DeleteAction());
            await result.Handle();
        }
    }
}
