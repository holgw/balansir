using BalansirApp.Core.Acts;
using BalansirApp.Core.Acts.DataAccess;
using BalansirApp.Core.Common;
using BalansirApp.Core.Domains.Acts;
using BalansirApp.ViewModels.Common;
using BalansirApp.ViewModels.Common.Utility;
using System;
using Xamarin.Forms;

namespace BalansirApp.ViewModels.Acts
{
    public class ActEdit_ViewModel : EntityEdit_ViewModel<Act, ActView, ActsQueryParam>
    {
        private Hours _hours;
        private Minutes _minutes;

        public override string Title => _source == null ? "НОВЫЙ АКТ" : "АКТ";

        // PROPS: Input
        [FormField] public string ProductName => _source?.ProductName;
        [FormField] public DateTime Date
        {
            get => _source?.TimeStamp.GetDayDate() ?? DateTime.MinValue;
            set
            {
                if (_source == null)
                    return;

                DateTime oldValue = _source.TimeStamp;
                _source.TimeStamp = _source.TimeStamp.SetDayDate(value);
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField] public int Hours
        {
            get => _source?.TimeStamp.Hour ?? 0;
            set
            {
                if (_source == null)
                    return;

                int oldValue = _hours.Value;
                _source.TimeStamp = _source.TimeStamp.TrySetHour(value);
                _hours.SetValue(value);
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField] public int Minutes
        {
            get => _source?.TimeStamp.Minute ?? 0;
            set
            {
                if (_source == null)
                    return;

                int oldValue = _minutes.Value;
                _source.TimeStamp = _source.TimeStamp.TrySetMinute(value);
                _minutes.SetValue(value);
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField] public string Comment
        {
            get => _source?.Comment;
            set
            {
                if (_source == null)
                    return;

                string oldValue = _source.Comment;
                _source.Comment = value;
                InformPropertyChanged(oldValue, value);
            }
        }
        [FormField] public decimal Delta
        {
            get => _source?.Delta ?? 0;
            set
            {
                if (_source == null)
                    return;

                decimal oldValue = _source.Delta;
                _source.Delta = value;
                InformPropertyChanged(oldValue, value);
            }
        }

        // CTOR
        public ActEdit_ViewModel(IActsService entityService)
            : base(entityService)
        {
        }

        // METHODS: Public
        public override void Setup(int? id = null, ActView entityView = null, bool notifyChanged = true)
        {
            base.Setup(id, entityView, false);

            _hours = new Hours(_source.TimeStamp.Hour);
            _minutes = new Minutes(_source.TimeStamp.Minute);

            this.NotifyAllFiledsChanged();
        }
        public void IncrementHours()
        {
            if (this.Hours == 23)
                this.Hours = 0;
            else
                Hours++;
        }
        public void DecrementHours()
        {
            if (this.Hours == 0)
                this.Hours = 23;
            else
                Hours--;
        }
        public void IncrementMinutes()
        {
            if (this.Minutes == 59)
                this.Minutes = 0;
            else
                Minutes++;
        }
        public void DecrementMinutes()
        {
            if (this.Minutes == 0)
                this.Minutes = 59;
            else
                this.Minutes--;
        }
        public void IncrementDelta()
        {
            Delta++;
        }
        public void DecrementDelta()
        {
            Delta--;
        }

        protected override void NotifyParents()
        {
            MessagingCenter.Send(this, Consts.ActsChanged_EventName);
            MessagingCenter.Send(this, Consts.ProductsChanged_EventName);
        }
    }
}
