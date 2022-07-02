using BalansirApp.Core.Common;
using BalansirApp.Core.Common.DataAccess;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Extensions = BalansirApp.Utility.Results.Extensions;

namespace BalansirApp.ViewModels.Common
{
    public abstract class EntityList_ViewModel<TRecord, TRecordView, TParam> : BaseViewModel
        where TRecord : DbRecord, new()
        where TParam : BaseQueryParam
    {
        protected readonly ISettingsProvider _settings;
        protected readonly IEntityService<TRecordView, TParam> _entityService;

        int totalItemsCount;
        int totalPagesCount;
        int currentPage = 1;
        string pagingTextView;
        bool isPagingBarVisible;

        // PROPS: Public Commands
        public Command LoadItemsCommand { get; }
        public Command GoToNextPageCmd { get; }
        public Command GoToPreviousPageCmd { get; }
        public Command ApplyFilterCmd { get; }

        // PROPS: Public
        public ObservableCollection<TRecordView> Items { get; }
        public abstract new string Title { get; }

        // PROPS: Pages Counter
        public int PageSize => _settings.PageSize;
        public int TotalItemsCount
        {
            get { return totalItemsCount; }
            set { SetProperty(ref totalItemsCount, value); }
        }
        public int TotalPagesCount
        {
            get { return totalPagesCount; }
            set { SetProperty(ref totalPagesCount, value); }
        }
        public int CurrentPage
        {
            get { return currentPage; }
            set { SetProperty(ref currentPage, value); }
        }
        public string PagingTextView
        {
            get { return pagingTextView; }
            set { SetProperty(ref pagingTextView, value); }
        }
        public bool IsPagingBarVisible
        {
            get { return isPagingBarVisible; }
            set { SetProperty(ref isPagingBarVisible, value); }
        }

        public abstract string ItemChanged_EventName { get; }

        // CTOR
        public EntityList_ViewModel(ISettingsProvider settings, IEntityService<TRecordView, TParam> entityService)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _entityService = entityService ?? throw new ArgumentNullException(nameof(entityService));

            this.Items = new ObservableCollection<TRecordView>();
            BindingBase.EnableCollectionSynchronization(this.Items, null, ObservableCollectionCallback);

            this.LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            this.GoToNextPageCmd = new Command(async () => await GoToNextPageAction());
            this.GoToPreviousPageCmd = new Command(async () => await GoToPreviousPageAction());
            this.ApplyFilterCmd = new Command(async () => await ApplyFilterCommand());

            this.Subscribe_ItemChangedEvent();
        }

        // METHODS: Protected
        protected virtual async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var queryParam = this.GetQueryParam();
                var result = Extensions.HandleMethod(() => _entityService.GetEntityListView(queryParam));
                var items = result.Object.Items;

                this.Items.Clear();
                foreach (var view in items)
                {
                    this.Items.Add(view);
                }

                this.TotalPagesCount = result.Object.TotalPagesCount;
                this.RefillPagingTextView();

                this.TotalItemsCount = result.Object.TotalItemsCount;

                await this.AfterLoadItems(queryParam);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        protected virtual async Task AfterLoadItems(TParam queryParam)
        {
        }
        protected virtual async Task ApplyFilterCommand()
        {
            this.CurrentPage = 1;
            await this.ExecuteLoadItemsCommand();
        }

        protected virtual async Task GoToNextPageAction()
        {
            if (this.CurrentPage != this.TotalPagesCount)
            {
                this.CurrentPage++;
                this.RefillPagingTextView();
                await this.ExecuteLoadItemsCommand();
            }
        }
        protected virtual async Task GoToPreviousPageAction()
        {
            if (this.CurrentPage != 1)
            {
                this.CurrentPage--;
                this.RefillPagingTextView();
                await this.ExecuteLoadItemsCommand();
            }
        }
        protected abstract TParam GetQueryParam();
        protected abstract void Subscribe_ItemChangedEvent();

        // METHODS: Private
        void RefillPagingTextView()
        {
            this.IsPagingBarVisible = this.TotalPagesCount > 1;
            this.PagingTextView = $"{this.CurrentPage}/{this.TotalPagesCount}";
        }
        void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
        {
            // `lock` ensures that only one thread access the collection at a time
            lock (collection)
            {
                accessMethod?.Invoke();
            }
        }
    }
}