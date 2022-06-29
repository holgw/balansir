using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BalansirApp.Utility.Results
{
    static class Extensions
    {
        public static async Task Handle(this Result result)
        {
            if (!result.IsSuccess)
            {
                await result.DisplayAlert();
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        public static async Task DisplayAlert(this Result result)
        {
            await Shell.Current.DisplayAlert("Предупреждение", result.Message, "OK");
        }

        public static Result HandleMethod(Action act)
        {
            var result = new Result();

            try
            {
                act();
            }
            catch (Exception ex)
            {
                result.SetFail(ex.Message);
            }

            return result;
        }
        public static Result<T> HandleMethod<T>(Func<T> func)
        {
            var result = new Result<T>();

            try
            {
                var obj = func();
                result.Object = obj;
            }
            catch (Exception ex)
            {
                result.SetFail(ex.Message);
            }

            return result;
        }
        public static Task<Result> GetResult(Action act)
        {
            return Task.FromResult(HandleMethod(act));
        }
        public static Task<Result<T>> GetResult<T>(Func<T> func)
        {
            return Task.FromResult(HandleMethod(func));
        }
    }
}